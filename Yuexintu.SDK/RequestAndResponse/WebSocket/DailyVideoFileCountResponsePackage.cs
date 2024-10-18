namespace Yuexintu.SDK.RequestAndResponse.WebSocket;

/// <summary>
/// 每日视频文件数量响应载荷
/// </summary>
public class DailyVideoFileCountResponsePackage
{
	/// <summary>
	/// 错误码
	/// </summary>
	public int Code { get; set; }

	/// <summary>
	/// 执行结果描述
	/// </summary>
	public string Msg { get; set; }
}