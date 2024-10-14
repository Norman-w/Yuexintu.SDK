namespace Yuexintu.SDK.RequestAndResponse.WebSocket;

/// <summary>
/// 人脸参数控制请求载荷
/// </summary>
public class FaceParameterControlRequestPayload
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
		/// 参数部分
		/// </summary>
		public ParametersModel Parameters { get; set; }

		/// <summary>
		/// 参数模型
		/// </summary>
		public class ParametersModel
		{
			/// <summary>
			/// 人脸检测开关
			/// </summary>
			public bool FaceDetection { get; set; }

			/// <summary>
			/// 人脸识别开关
			/// </summary>
			public bool FaceRecognition { get; set; }

			/// <summary>
			/// 人脸抓拍开关
			/// </summary>
			public bool FaceCapture { get; set; }
		}
	}
}