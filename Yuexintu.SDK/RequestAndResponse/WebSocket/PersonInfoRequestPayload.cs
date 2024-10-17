namespace Yuexintu.SDK.RequestAndResponse.WebSocket;

/// <summary>
/// 人员信息请求载荷
/// </summary>
public class PersonInfoRequestPayload : WebSocketRequestPayload
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
		/// 人员信息
		/// </summary>
		public PersonInfoModel PersonInfo { get; set; }

		/// <summary>
		/// 人员信息模型
		/// </summary>
		public class PersonInfoModel
		{
			/// <summary>
			/// 姓名
			/// </summary>
			public string Name { get; set; }

			/// <summary>
			/// 年龄
			/// </summary>
			public int Age { get; set; }

			/// <summary>
			/// 性别
			/// </summary>
			public int Gender { get; set; }

			/// <summary>
			/// 人脸ID
			/// </summary>
			public string Uuid { get; set; }

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
				/// 人脸图
				/// </summary>
				public string FaceData { get; set; }

				/// <summary>
				/// 全身照
				/// </summary>
				public string BodyData { get; set; }

				/// <summary>
				/// 全景图
				/// </summary>
				public string BgData { get; set; }
			}
		}
	}
}