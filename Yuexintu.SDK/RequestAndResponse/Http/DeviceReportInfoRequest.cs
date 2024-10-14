/*
 
 
 方向: 从摄像机请求到服务器
   说明: 该文件定义了设备信息上报请求模型，包含设备 ID、固件版本、软件版本、系统版本、硬件版本和设备名称等信息。


*/
namespace Yuexintu.SDK.RequestAndResponse.Http;

/// <summary>
/// 设备信息上报请求模型
/// </summary>
public class DeviceReportInfoRequest
{
	/// <summary>
	/// 设备ID
	/// </summary>
	public string Did { get; set; }
	/// <summary>
	/// 固件版本
	/// </summary>
	public string FwVer { get; set; }
	/// <summary>
	/// 软件版本
	/// </summary>
	public string SwVer { get; set; }
	/// <summary>
	/// 系统版本
	/// </summary>
	public string OsVer { get; set; }
	/// <summary>
	/// 硬件版本
	/// </summary>
	public string HwVer { get; set; }
	/// <summary>
	/// 设备名称
	/// </summary>
	public string DevName { get; set; }
}