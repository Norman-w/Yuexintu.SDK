/*
 
 
 方向: 从服务器响应到摄像机
   说明: 该文件定义了设备信息上报响应模型，包含错误编码和响应信息。


*/


namespace Yuexintu.SDK.RequestAndResponse.Http;

/// <summary>
/// 设备信息上报响应模型
/// </summary>
public class DeviceReportInfoResponse
{
	/// <summary>
	/// 错误编码
	/// </summary>
	public int Code { get; set; }
	/// <summary>
	/// 响应信息
	/// </summary>
	public string Msg { get; set; }
}