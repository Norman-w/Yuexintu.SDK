using Yuexintu.SDK.Service;

namespace Yuexintu.SDK.Event;

public class HttpRequestReceivedEventArgs
{
	public HttpRequestPackage Request { get; set; }
	public string Sn { get; set; }
}