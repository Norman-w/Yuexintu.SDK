/*



 摄像机模拟器
 单独的一个摄像机模拟器.
 如果模拟摄像机群,定时重启宕机摄像机,则需要批量实例和 TODO 尚未开发的测试群控器



*/

using System.ComponentModel;
using System.Net.WebSockets;
using System.Text;
using Newtonsoft.Json;
using Yuexintu.SDK.Enum;
using Yuexintu.SDK.RequestAndResponse.Http;
using Yuexintu.SDK.RequestAndResponse.WebSocket;

namespace Yuexintu.Simulator;

internal static class Program
{
	private static PersonInformationArchive? _personInformationArchive;
	private static FaceCapCameraSystemConfig? _faceCapCameraSystemConfig;

	public static void Main(string[] args)
	{
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine("Hello World!");
		Console.ResetColor();
		Console.WriteLine("摄像机硬件启动中,加载硬件信息中...");
//随机生成硬件信息(SN码,型号,版本号等)
		var sn = Utils.GenerateRandomSn();
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine($"摄像机硬件信息加载完成,SN: {sn}");
		Console.ResetColor();

		Console.WriteLine("摄像机系统启动中...");
		Console.WriteLine("加载摄像机系统设置中...");
//随机读取预先准备好的多个系统镜像,包括 HTTP,WebSocket服务器地址信息,心跳间隔,Token有效时间等设置.
// var faceCapCameraSystemConfig = FaceCapCameraSystemConfig.MockFaceCapCameraSystemConfig();
		_faceCapCameraSystemConfig = new FaceCapCameraSystemConfig
		{
			HttpServerUrl = "http://localhost:5011",
			WebSocketServerUrl = "ws://localhost:5011/ws",
		};
		Console.WriteLine($"摄像机系统配置信息: {_faceCapCameraSystemConfig}");

//加载摄像机闪存,读取人员库(不是人脸库)
		Console.WriteLine("加载摄像机人员库中...");
		_personInformationArchive = new PersonInformationArchive();
		var totalPersonAppearanceCount =
			_personInformationArchive.PersonInfoList.Sum(x => x.Value.AppearanceInformationList.Count);
		Console.WriteLine(
			$"摄像机人员库加载完成,共加载 {_personInformationArchive.PersonInfoList.Count} 条人员信息,及 {totalPersonAppearanceCount} 条人员特征信息");


		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine("摄像机系统启动完成");
		Console.ResetColor();


		var backgroundWorker = new BackgroundWorker();

		backgroundWorker.DoWork += OnBackgroundWorkerOnDoWork;

		backgroundWorker.RunWorkerAsync();

		//TODO 这里再开一个线程,用于接收服务器的推送消息,并处理

		while (true)
		{
			var cmd = Console.ReadLine();
			if (cmd == "exit")
			{
				break;
			}
		}

		return;


		async void OnBackgroundWorkerOnDoWork(object? sender, DoWorkEventArgs doWorkEventArgs)
		{
			//定义初始连接入口锚点
			Connect:
			var webSocket = new ClientWebSocket();
			while (true)
			{
				//进入loop

				//通过WebSocket连接服务器
				if (await ConnectWebSocketServer(webSocket, _faceCapCameraSystemConfig, sn)) continue;
				break;
			}

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("摄像机已完成报备,开始正常工作");
			Console.ResetColor();


			Console.WriteLine("初始化人脸监控器中...");
			//初始化FaceMonitor
			var faceMonitor = new FaceMonitor(_personInformationArchive);

			faceMonitor.FaceDetected += OnFaceMonitorOnFaceDetected;
			faceMonitor.FaceUnknownCaptured += OnFaceMonitorOnFaceUnknownCaptured;
			Console.WriteLine("人脸监控器初始化完成,准备启动");
			Console.ForegroundColor = ConsoleColor.Green;
			faceMonitor.Start();
			Console.WriteLine("人脸监控器已启动");
			Console.ResetColor();

			//持久化进程,每10秒发送一次心跳给服务器
			LoopHeartBeat(sn, webSocket);
			//如果应用程序已经退出,则不需要重连,返回
			if (backgroundWorker.CancellationPending)
			{
				goto Connect;
			}
		}

		void LoopHeartBeat(string s, ClientWebSocket clientWebSocket)
		{
			while (true)
			{
				Thread.Sleep(10000);
				var heartBeatRequestPackage = new DeviceHeartbeatRequestPackage
				{
					MsgId = Guid.NewGuid().ToString(),
					Data = new DeviceHeartbeatRequestPackage.DataModel
					{
						Param = new DeviceHeartbeatRequestPackage.DataModel.ParamModel
						{
							Sn = s, Did = Guid.NewGuid().ToString(), Ip = Utils.GetLocalIp(),
						}
					}
				};
				var heartBeatRequestJson = JsonConvert.SerializeObject(heartBeatRequestPackage);
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine($"摄像机发送心跳请求,Json: {heartBeatRequestJson}");
				Console.ResetColor();
				try
				{
					clientWebSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(heartBeatRequestJson)),
						WebSocketMessageType.Text, true, CancellationToken.None);
				}
				catch (Exception e)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine($"摄像机发送心跳请求失败,错误信息: {e.Message}");
					Console.ResetColor();
					//心跳失败则重新连接
					return;
				}
			}
		}
	}

	private static async Task<bool> ConnectWebSocketServer(ClientWebSocket clientWebSocket,
		FaceCapCameraSystemConfig? faceCapCameraSystemConfig, string s)
	{
		Console.WriteLine("摄像机连接WebSocket服务器中...");
		//初始化WebSocket连接
		try
		{
			if (faceCapCameraSystemConfig?.WebSocketServerUrl != null)
				await clientWebSocket.ConnectAsync(new Uri(faceCapCameraSystemConfig.WebSocketServerUrl),
					CancellationToken.None);
			else throw new Exception("WebSocketServerUrl is null");
		}
		catch (Exception e)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine($"摄像机连接WebSocket服务器失败,错误信息: {e.Message}");
			Console.ResetColor();
			Thread.Sleep(2000);
			return true;
		}

		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine("摄像机连接WebSocket服务器成功");
		Console.ResetColor();
		//获取Token
		Console.WriteLine("摄像机获取Token中...");
		var thisNetworkIp = Utils.GetLocalIp();
		//发送连接请求
		var deviceConnectionRequestPackage = new DeviceConnectionRequestPackage
		{
			MsgId = Guid.NewGuid().ToString(),
			Data = new DeviceConnectionRequestPackage.DataModel
			{
				Param = new DeviceConnectionRequestPackage.DataModel.ParamModel
				{
					Sn = s,
					Ip = thisNetworkIp,
					Did = Guid.NewGuid().ToString(),
					Ts = DateTimeOffset.Now.ToUnixTimeSeconds(),
					Sign = "TODO"
				},
			}
		};
		//发送
		var json = JsonConvert.SerializeObject(deviceConnectionRequestPackage);
		var jsonBytes = Encoding.UTF8.GetBytes(json);
		await clientWebSocket.SendAsync(new ArraySegment<byte>(jsonBytes), WebSocketMessageType.Text, true,
			CancellationToken.None);
		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.WriteLine($"摄像机发送连接请求,SN: {s}, IP: {thisNetworkIp}, Json: {json}");
		Console.ResetColor();
		//等待服务器返回Token,等待时长2秒,如果超时则重新执行while的入口
		var cancellationTokenSource = new CancellationTokenSource();
		cancellationTokenSource.CancelAfter(2000);

		var buffer = new byte[1024];
		WebSocketReceiveResult receiveResult;
		try
		{
			receiveResult =
				await clientWebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationTokenSource.Token);
		}
		catch (Exception e)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine($"摄像机获取Token超时,错误信息: {e.Message}");
			Console.ResetColor();
			Thread.Sleep(2000);
			return true;
		}

		var responseJson = Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);
		var deviceConnectionResponsePackage =
			JsonConvert.DeserializeObject<DeviceConnectionResponsePackage>(responseJson);
		if (deviceConnectionResponsePackage?.Data == null)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("摄像机获取Token失败,返回数据为空");
			Console.ResetColor();
			Thread.Sleep(2000);
			return true;
		}

		if (deviceConnectionResponsePackage.Data.Code != ErrorCode.Success)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(
				$"摄像机获取Token失败,错误码: {deviceConnectionResponsePackage.Data.Code},错误信息: {deviceConnectionResponsePackage.Data.Msg}");
			Console.ResetColor();
			Thread.Sleep(2000);
			return true;
		}

		Console.ForegroundColor = ConsoleColor.Magenta;
		Console.WriteLine($"摄像机获取Token成功,Token: {deviceConnectionResponsePackage.Data.Result.Token}");
		Console.ResetColor();
		faceCapCameraSystemConfig.TokenAwardFromServer =
			new FaceCapCameraSystemConfig.TokenModel(faceCapCameraSystemConfig.HttpServerUrl)
			{
				Token = deviceConnectionResponsePackage.Data.Result.Token,
				Expire = deviceConnectionResponsePackage.Data.Result.Expire,
				Interval = deviceConnectionResponsePackage.Data.Result.Interval
			};
		return false;
	}

	private static void OnFaceMonitorOnFaceUnknownCaptured(object sender, FaceMonitor.FaceUnknownCapturedEventArgs e)
	{
		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.WriteLine("摄像机检测到陌生人脸");
		Console.ResetColor();
	}

	private static async void OnFaceMonitorOnFaceDetected(object onFaceMonitorOnFaceDetectedSender,
		FaceMonitor.FaceDetectedEventArgs faceDetectedEventArgs)
	{
		//根据uuid,也就是pid查询这个人的一些信息填充request
		var pid = faceDetectedEventArgs.Pid;
		var person = _personInformationArchive?.PersonInfoList[pid];
		if (person != null)
		{
			var age = person.Age ?? 0;
			var gender = (int)person.Gender;
			var name = person.Name ?? "";
			var time = DateTime.Now.Ticks;
			var randomFaceImg = Utils.GenerateRandomFaceData();
			var randomBodyImg = Utils.GenerateRandomBodyData();
			var randomBgImg = Utils.GenerateRandomBgData();
			//通过HTTP发送人脸识别请求(报告识别到人脸)
			var faceRecognitionRequestPackage = new FaceCaptureRequest
			{
				Pid = pid,
				Age = (int)age,
				Name = name,
				Gender = gender,
				Time = time,
				Did = "TODO",
				Data = new FaceCaptureRequest.FaceDataModel
				{
					FaceData = randomFaceImg, 
					BodyData = randomBodyImg, 
					BgData = randomBgImg
				}
			};
			/*



 设备可通过服务器提供的HTTP接口查询和提交信息。生产环境与测试环境均使用HTTPS连接方式，在传输层进行数据加密。正常情况下，调用HTTP接口前应先调用connect接口获取token，并在header中携带token，形式如下：
   Content-Type: application/json
   Authorization: Bearer <token>
   如果没有传递token或者token无效，接口将返回401错误。


*/


			var faceRecognitionRequestJson = JsonConvert.SerializeObject(faceRecognitionRequestPackage);
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine($"摄像机发送人脸识别请求,Json: {faceRecognitionRequestJson}");
			Console.ResetColor();
			var faceRecognitionRequestJsonBytes = Encoding.UTF8.GetBytes(faceRecognitionRequestJson);
			var header = new Dictionary<string, string>
			{
				{ "Content-Type", "application/json" },
			};
			/*


			 2.1 上传人脸抓拍图像

			   POST /api/v1/adapter/lenfocus/face/capture
			   参数
			   {
			   "did"："1234512",
			   "time"： 1666781577,
			   "age"： 95, /*比对机专用* /
			   "gender"： 1, /*比对机专用* /
			   "uuid"： “1234566”, /*比对机专用* /
			   "name"： “1234566”, /*比对机专用* /
			   "data"：{
			   "face_data"："data:image/jpeg;base64,UklGRuAb…AABXRAAA==",
			   "body_data"："data:image/jpeg;base64,UklGRuAb…AABXRAAA==",
			   "bg_data"："data:image/jpeg;base64,UklGRuAb…AABXRAAA=="
			   }
			   }
			   参数说明：
			   参数
			   类型
			   说明
			   是否必须
			   did
			   字符串
			   设备id
			   是
			   time
			   整数
			   抓拍时间，UNIX时间戳，单位：秒
			   是
			   name
			   字符串
			   姓名
			   比对机
			   age
			   整数
			   年龄
			   比对机
			   gender
			   整数
			   1-男，2-女
			   比对机
			   uuid
			   字符串
			   人脸id
			   比对机
			   data
			   对象

			   否

			   face_data
			   字符串
			   人脸图,base64编码
			   否

			   body_data
			   字符串
			   全身照,base64编码
			   否

			   bg_data
			   字符串
			   全景图,base64编码
			   否



			   响应
			   {
			   "code":  错误码，
			   "msg": "返回消息"
			   }
			   参数说明：
			   参数
			   类型
			   说明
			   是否必须
			   code
			   整数
			   错误码，详见错误码定义。
			   是
			   msg
			   字符串
			   执行结果描述。
			   否




            */
			var response = string.Empty;
			try
			{
				if (_faceCapCameraSystemConfig?.HttpServerUrl != null)
					response = await HttpHelper.PostAsync(
						_faceCapCameraSystemConfig.HttpServerUrl,
						"/api/v1/adapter/lenfocus/face/capture",
						faceRecognitionRequestJsonBytes, header,
						$"Bearer {_faceCapCameraSystemConfig.TokenAwardFromServer.Token}"
					);
				else throw new Exception("HttpServerUrl is null");
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"摄像机发送人脸识别请求失败,错误信息: {e.Message}");
				Console.ResetColor();
			}

			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine($"摄像机发送人脸识别请求成功,返回: {response}");
			Console.ResetColor();
			//解析到请求结果对象
			var responseObj = JsonConvert.DeserializeObject<FaceCaptureResponse>(response);
			if (responseObj == null)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("摄像机发送人脸识别请求失败,返回数据为空");
				Console.ResetColor();
			}
			else if (responseObj.Code != ErrorCode.Success.Value)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"摄像机发送人脸识别请求失败,错误码: {responseObj.Code},错误信息: {responseObj.Msg}");
				Console.ResetColor();
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("摄像机发送人脸识别请求成功");
				Console.ResetColor();
			}
		}
		else
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine($"摄像机检测到有效人脸特征但未找到对应人员信息,UUID: {pid}");
			Console.ResetColor();
		}
	}
}

//输出连接信息
//连接
//输出连接结果
//随机时间产生人脸识别结果,并发送给服务器,保存结果
//随机发送失败但保存到"设备"本地存储记录成功(等待到时候重新发送)
//按设置时间定期加载失败结果,重新发送
//按间隔时间设置心跳
//按间隔时间自动更新Token
//随机时间宕机,重启,或根据args判断是否永久运行(不主动宕机,运行到启动处断电)
//随时接受新的服务端推送的配置信息,并应用