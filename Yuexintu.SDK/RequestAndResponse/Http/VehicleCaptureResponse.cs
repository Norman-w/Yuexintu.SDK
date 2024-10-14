/*
 
 
   方向: 从服务器响应到摄像机
   说明: 该文件定义了车辆抓拍响应模型，包含错误编码和响应信息。


*/
namespace Yuexintu.SDK.RequestAndResponse.Http;

/// <summary>
/// 车辆抓拍响应模型
/// </summary>
public class VehicleCaptureResponse
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