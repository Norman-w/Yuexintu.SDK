/*
 
 方向: 从摄像机请求到服务器
   说明: 该文件定义了车辆抓拍请求模型，包含设备 ID、序列号、抓拍时间和车辆数据等信息。

*/

using Yuexintu.SDK.Enum;
using Yuexintu.SDK.Model;
using Yuexintu.SDK.Service;

namespace Yuexintu.SDK.RequestAndResponse.Http;

/// <summary>
/// 车辆抓拍请求模型
/// </summary>
public class VehicleCaptureRequest : HttpRequestPackage
{
	/// <summary>
	/// 设备ID
	/// </summary>
	public string Did { get; set; }
	/// <summary>
	/// 设备序列号
	/// </summary>
	public string Sn { get; set; }
	/// <summary>
	/// 抓拍时间，UNIX时间戳，单位：毫秒
	/// </summary>
	public long Time { get; set; }
	/// <summary>
	/// 车辆数据
	/// </summary>
	public VehicleData Data { get; set; }

	/// <summary>
	/// 车辆数据类
	/// </summary>
	public class VehicleData
	{
		/// <summary>
		/// 全景图片，base64编码后的数据
		/// </summary>
		public string FullImage { get; set; }
		/// <summary>
		/// 车辆在全景图片中的位置
		/// </summary>
		public Rect VehicleLoc { get; set; }
		/// <summary>
		/// 车身图片，base64编码后的数据
		/// </summary>
		public string VehiclePic { get; set; }
		/// <summary>
		/// 车辆类型
		/// </summary>
		public VehicleType Type { get; set; }
		/// <summary>
		/// 车辆品牌
		/// </summary>
		public string Brand { get; set; }
		/// <summary>
		/// 车牌信息
		/// </summary>
		public PlateModel Plate { get; set; }

		/// <summary>
		/// 车牌信息类
		/// </summary>
		public class PlateModel
		{
			/// <summary>
			/// 车牌号码
			/// </summary>
			public string Id { get; set; }
			/// <summary>
			/// 车牌在全景图片中的位置
			/// </summary>
			public Rect PlateLoc { get; set; }
			/// <summary>
			/// 车牌图片，base64编码后的数据
			/// </summary>
			public string PlatePic { get; set; }
			/// <summary>
			/// 车牌类型
			/// </summary>
			public PlateType PlateType { get; set; }
		}
	}
}