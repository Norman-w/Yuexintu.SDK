namespace Yuexintu.SDK.RequestAndResponse.Http;

public class ServerTimeResponse
{
	public int Code { get; set; }
	public string? Msg { get; set; }
	public ServerTimeData? Data { get; set; }
	
	public class ServerTimeData
	{
		public DateTime Time { get; set; }
	}
}