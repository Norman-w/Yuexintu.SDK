namespace Yuexintu.SDK.RequestAndResponse.WebSocket;

/// <summary>
/// 删除人员信息请求载荷
/// </summary>
public class DeletePersonInfoRequestPayload : WebSocketRequestPayload
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

		/// <summary>
		/// 人脸ID
		/// </summary>
		public string Uuid { get; set; }
	}
}