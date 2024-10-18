using Yuexintu.SDK.Enum;

namespace Yuexintu.SDK.RequestAndResponse.WebSocket;

/// <summary>
/// 设备连接响应载荷
/// </summary>
public class DeviceConnectionResponsePayload
{
	/// <summary>
	/// 消息ID
	/// </summary>
	public string MsgId { get; set; }

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
		public string Uri => "/connect";

		public ErrorCode Code { get; set; }
		
		public string Msg { get; set; }
		
		public ResultModel Result { get; set; }
		
		public class ResultModel
		{
			public string Token { get; set; }
			
			public int Expire { get; set; }
			
			public int Interval { get; set; }
		}
	}
}