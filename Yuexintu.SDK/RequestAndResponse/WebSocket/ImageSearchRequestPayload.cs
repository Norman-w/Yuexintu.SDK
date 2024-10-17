namespace Yuexintu.SDK.RequestAndResponse.WebSocket;

/// <summary>
/// 以图搜图请求载荷
/// </summary>
public class ImageSearchRequestPayload : WebSocketRequestPayload
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
		/// 图像数据
		/// </summary>
		public string ImageData { get; set; }
	}
}