namespace Yuexintu.SDK.RequestAndResponse.WebSocket;

public abstract class WebSocketResponsePackage
{
	public string MsgId { get; set; }
	public static WebSocketResponsePackage Empty { get; } = new EmptyResponsePackage();
}

public class EmptyResponsePackage : WebSocketResponsePackage
{
}