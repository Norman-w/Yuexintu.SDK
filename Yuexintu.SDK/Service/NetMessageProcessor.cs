using System.Reflection;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Yuexintu.SDK.RequestAndResponse.WebSocket;

namespace Yuexintu.SDK.Service;

/// <summary>
/// TODO 需要考虑把这个类命名为WebSocketMessageProcessor,因为这个类的主要作用就是把WebSocket发来的string消息转换为对象,并调用事件.
/// 从网络过来的消息的处理器.
/// 如果SDK内部实现了消息接收器(HTTP/WebSocket服务),那么这个类就是消息处理器.
/// 处理HTTP请求,WebSocket消息等,将消息转换为SDK内部的消息对象,并调用注册的事件.
/// </summary>
public class NetMessageProcessor
{
	private Dictionary<string, Type>? _knownRequestPayloadTypes;

	private Dictionary<string, Type> KnownRequestPayloadTypes =>
		_knownRequestPayloadTypes ??= GetAllRequestPackageTypesDic();

	private static Dictionary<string, Type> GetAllRequestPackageTypesDic()
	{
		var dic = new Dictionary<string, Type>();
		//通过反射获取所有的继承自IWebSocketRequestPayload的类,并注册到消息处理器中
		var types = Assembly.GetExecutingAssembly().GetTypes()
			.Where(type => type is { IsClass: true, IsAbstract: false } &&
			               typeof(WebSocketRequestPackage).IsAssignableFrom(type));
		foreach (var type in types)
		{
			if (Activator.CreateInstance(type) is not WebSocketRequestPackage instance)
			{
				continue;
			}

			dic.Add(instance.GetUri(), type);
		}

		return dic;
	}

	public delegate WebSocketResponsePackage? WebSocketRequestPackageReceivedEventHandler(
		WebSocketRequestPackage iWebSocketRequestPackage);

	//TODO 需要重命名
	public event WebSocketRequestPackageReceivedEventHandler? OnWebSocketRequestPackageReceived;


	/// <summary>
	/// 处理从http/websocket收到的文本消息,如果消息被正确解析,将会通过OnWebSocketRequestPackageReceived事件传递出去.
	/// </summary>
	/// <param name="message"></param>
	/// <param name="returnMessageAction">当需要给客户端返回消息时,调用这个委托</param>
	public void ProcessMessage(string message, Action<string> returnMessageAction)
	{
		if (string.IsNullOrEmpty(message))
		{
			Console.WriteLine("无效的消息,消息为空");
			return;
		}

		/*
		 消息结构类似于:
		 摄像机连接请求
		 {
		     "msgid": "1729215134",
		     "data": {
		       "uri": "/connect",
		       "param": {
		         "sn": "WBU1249Q1000201",
		         "sign": "fgkmVcQY2EEy9YhTgjYqa90lzcC/GtfMkFFNpKIRuYZlHA289iHUe7Gw0H8VJWD0CUvE3LepsDMRgxWUa5mX1fd5xiZifRpP0oyqbC9ELR6/vQd3A8DSn1C9dzzg0Ll1w6jcT1XBr5o4JoqOyFDGLcIezbOwjfeWxiRA9Orqk4w=",
		         "ts": 1729215134
		       }
		     }
		   }
        */
		WebSocketRequestPackageSimple? websocketRequestPackage = null;
		try
		{
			websocketRequestPackage = new WebSocketRequestPackageSimple(message);
		}
		catch (Exception e)
		{
			//原路发送回去消息,告诉客户端消息无效
			Console.WriteLine($"无效的消息,无法解析: {message}, 错误信息: {e.Message}");
			returnMessageAction($"无法解析的消息: {message}");
		}

		if (string.IsNullOrWhiteSpace(websocketRequestPackage?.MsgId)
		   )
			// || Guid.TryParse(websocketRequestPackage.MsgId, out var _) == false)
		{
			Console.WriteLine($"无效的消息,没有有效的MsgId:{message}");
			return;
		}

		if (KnownRequestPayloadTypes.TryGetValue(websocketRequestPackage.Data.Uri, out var type) == false)
		{
			Console.WriteLine($"未知的请求类型, uri: {websocketRequestPackage.Data.Uri}, body: {message}");
			return;
		}

		//创建一个实例
		if (Activator.CreateInstance(type) is not WebSocketRequestPackage instance)
		{
			Console.WriteLine($"无法创建实例, uri: {websocketRequestPackage.Data.Uri}, body: {message}");
			return;
		}

		//使用json填充
		JsonConvert.PopulateObject(message, instance);
		instance.SetBody(message);
		//触发事件
		var responsePackage = OnWebSocketRequestPackageReceived?.Invoke(instance);
		if (responsePackage == null)
		{
			//回调函数中返回null,则不向客户端发送消息
			return;
		}

		//json 配置,使用小驼峰
		var settings = new JsonSerializerSettings
		{
			ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
		};
		var responseJson = JsonConvert.SerializeObject(responsePackage, settings);
		returnMessageAction(responseJson);
	}
}