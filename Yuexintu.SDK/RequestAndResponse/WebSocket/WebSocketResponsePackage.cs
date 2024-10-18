namespace Yuexintu.SDK.RequestAndResponse.WebSocket;

public abstract class WebSocketResponsePackage
{
	public static WebSocketResponsePackage Empty { get; } = new EmptyResponsePackage();
}

public class EmptyResponsePackage : WebSocketResponsePackage
{
}