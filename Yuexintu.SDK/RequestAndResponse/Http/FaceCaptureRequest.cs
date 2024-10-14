/*
 
 方向: 从摄像机请求到服务器
   说明: 该文件定义了人脸抓拍请求模型，包含设备 ID、抓拍时间、年龄、性别、人脸 ID、姓名和人脸数据等信息。

*/
namespace Yuexintu.SDK.RequestAndResponse.Http;

/// <summary>
/// 人脸抓拍请求模型
/// </summary>
public class FaceCaptureRequest
{
	/// <summary>
	/// 设备ID
	/// </summary>
	public string Did { get; set; }
	/// <summary>
	/// 抓拍时间，UNIX时间戳，单位：秒
	/// </summary>
	public long Time { get; set; }
	/// <summary>
	/// 年龄
	/// </summary>
	public int Age { get; set; }
	/// <summary>
	/// 性别，1-男，2-女
	/// </summary>
	public int Gender { get; set; }
	/// <summary>
	/// 人脸ID
	/// </summary>
	public string Uuid { get; set; }
	/// <summary>
	/// 姓名
	/// </summary>
	public string Name { get; set; }
	/// <summary>
	/// 人脸数据
	/// </summary>
	public FaceDataModel Data { get; set; }

	/// <summary>
	/// 人脸数据类
	/// </summary>
	public class FaceDataModel
	{
		/// <summary>
		/// 人脸图，base64编码
		/// </summary>
		public string FaceData { get; set; }
		/// <summary>
		/// 全身照，base64编码
		/// </summary>
		public string BodyData { get; set; }
		/// <summary>
		/// 全景图，base64编码
		/// </summary>
		public string BgData { get; set; }
	}
}