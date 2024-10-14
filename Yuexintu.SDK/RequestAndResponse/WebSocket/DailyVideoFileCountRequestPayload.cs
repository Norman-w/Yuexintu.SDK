namespace Yuexintu.SDK.RequestAndResponse.WebSocket;

/// <summary>
/// 每日视频文件数量请求载荷
/// </summary>
public class DailyVideoFileCountRequestPayload
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
		/// 月份
		/// </summary>
		public string Month { get; set; }
	}
}