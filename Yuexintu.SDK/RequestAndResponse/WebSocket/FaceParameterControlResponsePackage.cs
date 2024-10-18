namespace Yuexintu.SDK.RequestAndResponse.WebSocket;

/// <summary>
/// 人脸参数控制响应载荷
/// </summary>
public class FaceParameterControlResponsePackage
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