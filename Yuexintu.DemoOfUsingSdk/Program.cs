/*


 * 添加对Yuexintu.SDK的引用
 * 初始化SdkServer并启动
 * 初始化NetFacade对象并启动
 * 注册NetFacade对象的事件
 * 做应用的其他操作并loop


*/

using Yuexintu.DemoOfUsingSdk;
using Yuexintu.SDK.Enum;
using Yuexintu.SDK.RequestAndResponse.WebSocket;
using Yuexintu.SDK.Service;
using Yuexintu.SDK.Service.Api;

Console.WriteLine("Demo of using SDK is starting...");

#region 用法示例1

void 单独使用消息处理器对象消息由外部提供()
{
	#region 初始化
	var netMessageProcessor = new NetMessageProcessor();
	netMessageProcessor.OnWebSocketRequestPackageReceived += 处理摄像机发过来的请求包回调函数;
	#endregion
	
	
	#region 模拟消息处理
	//比如这是从Controller Request中获取的消息
	const string mockUpMessage = "{\"uri\":\"/connect\",\"msgId\":\"1234567890\",\"data\":{\"param\":{\"did\":\"1234567890\",\"sn\":\"1234567890\"}}}";
	//处理这条消息,如果消息正确处理了,则会触发上面注册的事件,回调"处理摄像机发过来的请求包回调函数"
	
	//比如这里触发一个异步等待机制,等待处理完毕
	//var task = Task.Run(() => netMessageProcessor.ProcessMessage(mockUpMessage, response => { }));
	//task.Wait();
	//或者直接调用(不需要回发消息的话适用)
	netMessageProcessor.ProcessMessage(mockUpMessage, response =>
	{
		//这里的response是 处理摄像机发过来的请求包回调函数 返回的值, 我们需要在下面通过消息通道将这个值回发给摄像机
		//比如你从http获取到的消息,那这里就是给http的request的response
		Console.WriteLine($"回发消息: {response}");
		//比如Controller获取到消息的地方要Response.Write(response);
		//执行完毕,这里可以释放等待机
	});
	//task结束后,可以继续执行其他操作
	#endregion
	
	/*NOTE
	若是
	需要处理字符串消息(由任何地方获得),调用 netMessageProcessor.ProcessMessage(字符串消息, 回发消息回调函数);

	这种用法可以在任何地方调用,不需要初始化NetFacade对象
	比如用在测试用例,使用gRPC模拟,压力测试等
	*/
}

#endregion

#region 用法示例2

void 使用已经存在的含WebSocket功能的Http服务器对象和Sdk服务器()
{
	/*

	 这种用法NetMessageProcessor对象不需要初始化,因为已经在Server内部初始化了
	 只需要原有的服务器实现INetFacade接口即可
	 比如已经有了一个HTTP服务器,只需要实现一个INetFacade的接口,然后传入Server对象即可
	 Server对象会自动初始化NetMessageProcessor,
	 且可以自动注册NetFacade的事件,并在事件中调用NetMessageProcessor的处理方法

    */
	var netFacade = new CustomNetFacade();
	//注意这里的调用方式,传入一个继承自INetFacade的对象
	var server = new Server(netFacade);
	
	// 这个事件已修改,只有在sdk包内可使用internal instance的地方才可以使用.实际使用Server时候,使用业务层的两个事件(OnDeviceConnected,OnFaceCaptured)即可
	// server.OnWebSocketRequestPackageReceived += 处理摄像机发过来的请求包回调函数;
	server.OnDeviceConnected += (sender, e) =>
	{
		//这是业务层的.
		Console.WriteLine($"设备连接事件: {e.Sn}");
	};
	server.OnFaceCaptured += (sender, e) =>
	{
		//这是业务层的.
		// var imageSize = e.Image.Length;
		// Console.WriteLine($"人脸抓拍事件: 图片大小: {imageSize},设备SN: {e.Sn}");
		Console.WriteLine($"人脸抓拍事件: 设备SN: {e.Sn}, 人员ID: {e.KnownPersonId}, 是否已知: {e.IsKnownPerson}", ConsoleColor.Green);
	};
	netFacade.Start();
}

#endregion

#region 用法示例3

void 使用内置Http和Websocket服务器和内置消息处理器()
{
	/*


	 这种用法直接初始化一个Server对象即可
	 监听WebSocket和处理消息都是内置的


    */

	var server = new Server(51648);

	server.OnDeviceConnected += (sender, e) =>
	{
		//这是业务层的.
		Console.WriteLine($"设备连接事件: {e.Sn}");
	};
	server.OnFaceCaptured += (sender, e) =>
	{
		//这是业务层的.
		// var imageSize = e.Image.Length;
		// Console.WriteLine($"人脸抓拍事件: 图片大小: {imageSize},设备SN: {e.Sn}");
		Console.WriteLine($"人脸抓拍事件: 设备SN: {e.Sn}, 人员ID: {e.KnownPersonId}, 是否已知: {e.IsKnownPerson}", ConsoleColor.Green);
	};
	
	server.Start();
}

#endregion

// 使用已经存在的含WebSocket功能的Http服务器对象和Sdk服务器();
使用内置Http和Websocket服务器和内置消息处理器();

Console.WriteLine("Demo of using SDK has started...");

var startTime = DateTime.Now;

//应用程序不会主动终止,使用Ctrl+C终止
while (true)
{
	//模拟应用程序的其他操作,输出已运行时间
	Console.WriteLine($"Demo of using SDK is running...{(uint)(DateTime.Now - startTime).TotalSeconds} seconds");
	Thread.Sleep(10000);
}

WebSocketResponsePackage 处理摄像机发过来的请求包回调函数(WebSocketRequestPackage package)
{
	#region 日志输出接收到的WebSocket请求Uri和Body

	Console.ForegroundColor = ConsoleColor.Cyan;
	Console.WriteLine($"接收到WebSocket请求: {package.GetUri()} - {package.Body}");
	Console.ResetColor();

	#endregion

	#region 处理摄像机发过来的请求

	switch (package)
	{
		case DeviceConnectionRequestPackage deviceConnectionRequestPayload:
		{
			Console.WriteLine($"摄像机连接请求, SN: {deviceConnectionRequestPayload.Data.Param.Sn}");
			if (string.IsNullOrEmpty(deviceConnectionRequestPayload.Data.Param.Did)
			    && string.IsNullOrEmpty(deviceConnectionRequestPayload.Data.Param.Sn)
			    )
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("摄像机连接请求中的Did 和 SN 都为空,无法连接");
				Console.ResetColor();

				#region 发送421错误返回给摄像机

				var response = new DeviceConnectionResponsePackage()
				{
					MsgId = deviceConnectionRequestPayload.MsgId,
					Data = new DeviceConnectionResponsePackage.DataModel()
					{
						#region 拒绝

						//uri 不需要设置
						Msg = "Did 和 SN 都为空,无法连接",
						Code = ErrorCode.DeviceNotRegistered,
						Result = new DeviceConnectionResponsePackage.DataModel.ResultModel()
						{
							Token = string.Empty,
							Expire = 0,
							Interval = 0
						}

						#endregion

						#region 同意,生成Token并下发
						//
						// //可以正常下发Token,摄像机收到以后不会再次请求连接,但是再次连接后 Did没有下发
						//
						// // Uri = "/connect",
						// Msg = "OK",
						// Code = ErrorCode.Success,
						// Result = new DeviceConnectionResponsePackage.DataModel.ResultModel()
						// {
						// 	Token = "1234567890",
						// 	Expire = 3600,
						// 	Interval = 60,
						// }

						#endregion
					}
				};

				Console.ForegroundColor = ConsoleColor.Magenta;
				Console.WriteLine($"返回给摄像机的消息: {response.Data.Code} - {response.Data.Msg}");
				Console.ResetColor();

				return response;

				#endregion
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Green;
				var did = deviceConnectionRequestPayload.Data.Param.Did;
				var sn = deviceConnectionRequestPayload.Data.Param.Sn;
				Console.WriteLine($"摄像机连接请求中的Did为: {did} , SN为: {sn}");
				if(!string.IsNullOrEmpty(sn) || !string.IsNullOrEmpty(did))
				{
					Console.WriteLine("具备有效的SN或者Did,可以连接");
				}
				Console.ResetColor();
				var response = new DeviceConnectionResponsePackage()
				{
					MsgId = deviceConnectionRequestPayload.MsgId,
					Data = new DeviceConnectionResponsePackage.DataModel()
					{
						Msg = "OK",
						Code = ErrorCode.Success,
						Result = new DeviceConnectionResponsePackage.DataModel.ResultModel()
						{
							Token = "1234567890",
							Expire = 3600,
							Interval = 60
						}
					}
				};
				
				Console.ForegroundColor = ConsoleColor.Magenta;
				Console.WriteLine($"返回给摄像机的消息: {response.Data.Code} - {response.Data.Msg}");
				Console.ResetColor();
				return response;
			}

			break;
		}
		case DeviceHeartbeatRequestPackage deviceHeartbeatRequestPayload:
			Console.WriteLine($"摄像机心跳请求, Did: {deviceHeartbeatRequestPayload.Data.Param.Did}");
			break;
		case FaceParameterControlRequestPackage faceParameterControlRequestPayload:
			Console.WriteLine($"摄像机参数控制请求, Did: {faceParameterControlRequestPayload.Data.Did}");
			break;
		case RebootDeviceRequestPackage rebootDeviceRequestPayload:
			Console.WriteLine($"摄像机重启请求, Token: {rebootDeviceRequestPayload.Token}");
			break;
		default:
			Console.WriteLine($"未知请求: {package.GetUri()}");
			break;
	}

	#endregion
	
	
	//当没有返回值时,返回一个空的WebSocketResponsePackage

	return WebSocketResponsePackage.Empty;
}