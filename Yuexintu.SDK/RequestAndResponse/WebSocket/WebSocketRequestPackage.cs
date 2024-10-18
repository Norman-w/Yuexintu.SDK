namespace Yuexintu.SDK.RequestAndResponse.WebSocket;

public abstract class WebSocketRequestPackage : IWebSocketRequestPackage
{
	/// <summary>
	/// 获取URI
	/// </summary>
	/// <returns></returns>
	public abstract string GetUri();
	/// <summary>
	/// 消息ID
	/// </summary>
	public string MsgId { get; set; }
}