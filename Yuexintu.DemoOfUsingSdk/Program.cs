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

Console.WriteLine("Demo of using SDK is starting...");
var netMessageProcessor = new NetMessageProcessor();
netMessageProcessor.OnWebSocketRequestPackageReceived += payload =>
{
	Console.ForegroundColor = ConsoleColor.Cyan;
	Console.WriteLine($"接收到WebSocket请求: {payload.GetUri()}");
	Console.ResetColor();

	#region 处理摄像机发过来的请求

	if (payload is DeviceConnectionRequestPackage deviceConnectionRequestPayload)
	{
		Console.WriteLine($"摄像机连接请求, SN: {deviceConnectionRequestPayload.Data.Param.Sn}");
		if (string.IsNullOrEmpty(deviceConnectionRequestPayload.Data.Param.Did))
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("摄像机连接请求中的Did为空,无法连接");
			Console.ResetColor();

			#region 发送421错误返回给摄像机

			var response = new DeviceConnectionResponsePackage()
			{
				MsgId = deviceConnectionRequestPayload.MsgId,
				Data = new DeviceConnectionResponsePackage.DataModel()
				{
					#region 拒绝

					// //uri 不需要设置
					// Msg = "Did为空,无法连接",
					// Code = ErrorCode.DeviceNotRegistered,
					// Result = new DeviceConnectionResponsePackage.DataModel.ResultModel()
					// {
					// 	Token = string.Empty,
					// 	Expire = 0,
					// 	Interval = 0
					// }

					#endregion

					#region 同意,生成Token并下发
					//可以正常下发Token,摄像机收到以后不会再次请求连接,但是再次连接后 Did没有下发

					// Uri = "/connect",
					Msg = "OK",
					Code = ErrorCode.Success,
					Result = new DeviceConnectionResponsePackage.DataModel.ResultModel()
					{
						Token = "1234567890",
						Expire = 3600,
						Interval = 60,
					}

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
			Console.WriteLine($"摄像机连接请求中的Did为: {deviceConnectionRequestPayload.Data.Param.Did} , 可以分配服务端通讯Token进行后续通讯");
			Console.ResetColor();
		}
	}

	#endregion

	return WebSocketResponsePackage.Empty;
};
var netFacade = new NetFacade();
netFacade.Start();
netFacade.SessionCreatedAsync += async session =>
{
	Console.WriteLine($"已创建新会话: {session.ClientType}");
	// switch (session.ClientType)
	// {
	// 	case ClientTypeEnum.FaceCapCamera:
	// 	case ClientTypeEnum.Unknown:
	// 	case ClientTypeEnum.Reporter:
	// 	case ClientTypeEnum.Receiver:
	// 	case ClientTypeEnum.Writer:
	// 	default:
	// 		throw new ArgumentOutOfRangeException();
	// }
	var client = FaceCapCamaraClient.FromSession(session);
	client.OnRequestReceived += (sender, message) =>
	{
		// Console.WriteLine($"接收到客户端消息: {message}");
		netMessageProcessor.ProcessMessage(message, response =>
		{
			client.Send(response);
		});
	};
	client.ClientDisconnected += (c) => { Console.WriteLine($"报告者客户端断开连接: {c}"); };
	await client.StartWorking();
};
Console.WriteLine("Demo of using SDK has started...");

var startTime = DateTime.Now;

//应用程序不会主动终止,使用Ctrl+C终止
while (true)
{
	//模拟应用程序的其他操作,输出已运行时间
	Console.WriteLine($"Demo of using SDK is running...{(DateTime.Now - startTime).TotalSeconds} seconds");
	Thread.Sleep(5000);
}