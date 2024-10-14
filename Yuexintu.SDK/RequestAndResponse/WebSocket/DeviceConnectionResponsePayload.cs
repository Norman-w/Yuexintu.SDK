namespace Yuexintu.SDK.RequestAndResponse.WebSocket;

/// <summary>
/// 设备连接响应载荷
/// </summary>
public class DeviceConnectionResponsePayload
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
		/// 请求的URI
		/// </summary>
		public string Uri { get; set; }

		/// <summary>
		/// 参数部分
		/// </summary>
		public ParamModel Param { get; set; }

		/// <summary>
		/// 参数模型
		/// </summary>
		public class ParamModel
		{
			/// <summary>
			/// 设备ID
			/// </summary>
			public string Did { get; set; }

			/// <summary>
			/// 序列号
			/// </summary>
			public string Sn { get; set; }

			/// <summary>
			/// IP地址
			/// </summary>
			public string Ip { get; set; }

			/// <summary>
			/// 签名
			/// </summary>
			public string Sign { get; set; }

			/// <summary>
			/// 时间戳
			/// </summary>
			public long Ts { get; set; }
		}
	}
}