namespace Yuexintu.SDK.RequestAndResponse.WebSocket;

/// <summary>
/// 录像设置响应载荷
/// </summary>
public class VideoSettingsResponsePayload
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