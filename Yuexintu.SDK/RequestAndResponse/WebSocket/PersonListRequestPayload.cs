namespace Yuexintu.SDK.RequestAndResponse.WebSocket;

/// <summary>
/// 人员列表请求载荷
/// </summary>
public class PersonListRequestPayload : WebSocketRequestPayload
{
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
	}
}