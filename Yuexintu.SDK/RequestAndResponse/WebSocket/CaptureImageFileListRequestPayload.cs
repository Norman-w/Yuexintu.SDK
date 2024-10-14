namespace Yuexintu.SDK.RequestAndResponse.WebSocket;

/// <summary>
/// 抓拍图像文件列表请求载荷
/// </summary>
public class CaptureImageFileListRequestPayload
{
	/// <summary>
	/// 消息ID
	/// </summary>
	public string Msgid { get; set; }

	/// <summary>
	/// 数据部分
	/// </summary>
	public DataModel Data { get; set; }

	/// <summary>
	/// 数据模型
	/// </summary>
	public class DataModel
	{
		/// <summary>
		/// 设备ID
		/// </summary>
		public string Did { get; set; }

		/// <summary>
		/// 日期
		/// </summary>
		public string Date { get; set; }
	}
}