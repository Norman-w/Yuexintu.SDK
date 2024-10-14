namespace Yuexintu.SDK.RequestAndResponse.WebSocket;

/// <summary>
/// 设备心跳请求载荷
/// </summary>
public class DeviceHeartbeatRequestPayload
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
		/// 时间戳
		/// </summary>
		public long Ts { get; set; }
	}
}