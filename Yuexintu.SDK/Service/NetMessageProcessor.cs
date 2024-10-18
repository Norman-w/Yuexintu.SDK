using Yuexintu.SDK.RequestAndResponse.WebSocket;

namespace Yuexintu.SDK.Service;

/// <summary>
/// 从网络过来的消息的处理器.
/// 如果SDK内部实现了消息接收器(HTTP/WebSocket服务),那么这个类就是消息处理器.
/// 处理HTTP请求,WebSocket消息等,将消息转换为SDK内部的消息对象,并调用注册的事件.
/// </summary>
public class NetMessageProcessor
{
	public NetMessageProcessor()
	{
	}
	
	public delegate void WebSocketRequestReceivedEventHandler(WebSocketRequestPayload webSocketRequestPayload);

	public void ProcessMessage(string message)
	{
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
		var websocketRequestPackage = new WebSocketRequestPackage(message);
		
	}
}