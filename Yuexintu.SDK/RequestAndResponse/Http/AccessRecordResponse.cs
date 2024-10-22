namespace Yuexintu.SDK.RequestAndResponse.Http;

/// <summary>
/// 当摄像机检测到人脸时,会发送请求到服务器上报访问记录
/// 随后摄像机会收到服务器的响应
/// TODO 待确认是不是服务器响应状态码为200后摄像机就不再重发
/// </summary>
public class AccessRecordResponse
{
	/// <summary>
	/// 响应码,当为200时表示成功,使用ErrorCode.Success.Value
	/// </summary>
	public int Code { get; set; }
	/// <summary>
	/// 响应消息,成功时为"OK"
	/// TODO: 不确定是不是必须的
	/// </summary>
	public string? Msg { get; set; }
}