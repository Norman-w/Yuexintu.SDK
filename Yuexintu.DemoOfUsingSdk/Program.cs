/*
 
 
 * 添加对Yuexintu.SDK的引用
 * 初始化SdkServer并启动
 * 初始化NetFacade对象并启动
 * 注册NetFacade对象的事件
 * 做应用的其他操作并loop


*/

using Yuexintu.DemoOfUsingSdk;
using Yuexintu.SDK.Service;

Console.WriteLine("Demo of using SDK is starting...");
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
			Console.WriteLine($"接收到客户端消息: {message}");
		};
		client.ClientDisconnected += (c) =>
		{
			Console.WriteLine($"报告者客户端断开连接: {c}");
		};
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