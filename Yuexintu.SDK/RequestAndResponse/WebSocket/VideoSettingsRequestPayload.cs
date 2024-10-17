namespace Yuexintu.SDK.RequestAndResponse.WebSocket;

/// <summary>
/// 录像设置请求载荷
/// </summary>
public class VideoSettingsRequestPayload : WebSocketRequestPayload
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
		/// 设置部分
		/// </summary>
		public SettingsModel Settings { get; set; }

		/// <summary>
		/// 设置模型
		/// </summary>
		public class SettingsModel
		{
			/// <summary>
			/// 分辨率
			/// </summary>
			public string Resolution { get; set; }

			/// <summary>
			/// 帧率
			/// </summary>
			public int FrameRate { get; set; }

			/// <summary>
			/// 比特率
			/// </summary>
			public int BitRate { get; set; }
		}
	}
}