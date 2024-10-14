/*
 
 方向: 从摄像机请求到服务器
   说明: 该文件定义了事件上报请求模型，包含设备 ID、事件类型、事件发生时间和事件数据等信息。

*/
namespace Yuexintu.SDK.RequestAndResponse;

/// <summary>
/// 事件上报请求模型
/// </summary>
public class EventReportRequest
{
	/// <summary>
	/// 设备ID
	/// </summary>
	public string Did { get; set; }
	/// <summary>
	/// 事件类型
	/// </summary>
	public EventType Type { get; set; }
	/// <summary>
	/// 事件发生时间，UNIX时间戳，单位：毫秒
	/// </summary>
	public long Time { get; set; }
	/// <summary>
	/// 事件数据
	/// </summary>
	public EventData Data { get; set; }

	/// <summary>
	/// 事件数据类
	/// </summary>
	public class EventData
	{
		/// <summary>
		/// 人脸图，base64编码
		/// </summary>
		public string FaceData { get; set; }
		/// <summary>
		/// 全身照，base64编码
		/// </summary>
		public string BodyData { get; set; }
		/// <summary>
		/// 全景图，base64编码
		/// </summary>
		public string BgData { get; set; }
		/// <summary>
		/// 人脸JPEG URL
		/// </summary>
		public string JpegUrlFace { get; set; }
		/// <summary>
		/// 全身JPEG URL
		/// </summary>
		public string JpegUrlBody { get; set; }
		/// <summary>
		/// 全景JPEG URL
		/// </summary>
		public string JpegUrlFrame { get; set; }
	}
}