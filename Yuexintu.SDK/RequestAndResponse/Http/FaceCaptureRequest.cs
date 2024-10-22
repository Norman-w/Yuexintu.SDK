// /*
//  
//  方向: 从摄像机请求到服务器
//    说明: 该文件定义了人脸抓拍请求模型，包含设备 ID、抓拍时间、年龄、性别、人脸 ID、姓名和人脸数据等信息。
//
// */
// namespace Yuexintu.SDK.RequestAndResponse.Http;
//
// /// <summary>
// /// 人脸抓拍请求模型
// /// </summary>
// public class FaceCaptureRequest
// {
// 	/// <summary>
// 	/// 设备ID
// 	/// </summary>
// 	public string Did { get; set; }
// 	/// <summary>
// 	/// 抓拍时间，UNIX时间戳，单位：秒
// 	/// </summary>
// 	public long Time { get; set; }
// 	/// <summary>
// 	/// 年龄
// 	/// </summary>
// 	public int Age { get; set; }
// 	/// <summary>
// 	/// 性别，1-男，2-女
// 	/// </summary>
// 	public int Gender { get; set; }
// 	/// <summary>
// 	/// 人脸ID
// 	/// </summary>
// 	public string Uuid { get; set; }
// 	/// <summary>
// 	/// 姓名
// 	/// </summary>
// 	public string Name { get; set; }
// 	/// <summary>
// 	/// 人脸数据
// 	/// </summary>
// 	public FaceDataModel Data { get; set; }
//
// 	/// <summary>
// 	/// 人脸数据类
// 	/// </summary>
// 	public class FaceDataModel
// 	{
// 		/// <summary>
// 		/// 人脸图，base64编码
// 		/// </summary>
// 		public string FaceData { get; set; }
// 		/// <summary>
// 		/// 全身照，base64编码
// 		/// </summary>
// 		public string BodyData { get; set; }
// 		/// <summary>
// 		/// 全景图，base64编码
// 		/// </summary>
// 		public string BgData { get; set; }
// 	}
// }


//以上是之前根据文档创建的本本.实际应用中,收到的消息请求与文档描述不一致.故上述代码无法正常运行.


/*

实际从设备接受到的内容:
{
     "did": "",
     "type": 11,
     "time": 1729564815,
     "sn": "WBU1249Q1000201",
     "age": 0,
     "gender": 0,
     "pid": "",
     "name": "",
     "id_card": "",
     "work_id": "",
     "phone": "",
     "department": "",
     "otherInfo": "",
     "category": 0,
     "score": 83,
     "helmet": 0,
     "smoke": 0,
     "handed": 0,
     "feature": [
       -0.033698,
       
       ...
       
       
       0.040698
     ],
     "data": {
       "id": 0,
       "face_data": "/9j/4AAQSkZJRgABAQAAAQABAAD
       
       ...
       
       neqEf/9k\u003d",
       "jpeg_url_face": "sdcard/face_record/22/40/20241022104038000859.jpg",
       "bg_data": "/9j/4AAQSkZJRgABAQAAAQABAAD
       
       ...
       
       JS0lMAooooA//2Q\u003d\u003d",
       "jpeg_url_frame": "sdcard/face_record/22/40/20241022104038000859_bg.jpg"
     }
   }


*/


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
  /// 人员ID
  /// </summary>
  public string Pid { get; set; }
  /// <summary>
  /// 姓名
  /// </summary>
  public string Name { get; set; }
  /// <summary>
  /// 身份证号
  /// </summary>
  public string IdCard { get; set; }
  /// <summary>
  /// 工号
  /// </summary>
  public string WorkId { get; set; }
  /// <summary>
  /// 电话
  /// </summary>
  public string Phone { get; set; }
  /// <summary>
  /// 部门
  /// </summary>
  public string Department { get; set; }
  /// <summary>
  /// 其他信息
  /// </summary>
  public string OtherInfo { get; set; }
  /// <summary>
  /// 类别 TODO 具体含义不详
  /// </summary>
  public int Category { get; set; }
  /// <summary>
  /// 分数(识别后的相似度)
  /// </summary>
  public int Score { get; set; }
  /// <summary>
  /// 是否戴安全帽 TODO 具体含义不详
  /// </summary>
  public int Helmet { get; set; }
  /// <summary>
  /// 是否吸烟 TODO 具体含义不详
  /// </summary>
  public int Smoke { get; set; }
  /// <summary>
  /// 手势 TODO 具体含义不详
  /// </summary>
  public int Handed { get; set; }
  /// <summary>
  /// 特征
  /// </summary>
  public List<double> Feature { get; set; }
  public FaceDataModel Data { get; set; }
  public class FaceDataModel
  {
    /// <summary>
    /// ID
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// 人脸数据 TODO 是否能直接解析成图片?
    /// </summary>
    public string FaceData { get; set; }
    /// <summary>
    /// 人脸图片地址
    /// </summary>
    public string JpegUrlFace { get; set; }
    /// <summary>
    /// 背景数据 TODO 是否能直接解析成图片?
    /// </summary>
    public string BgData { get; set; }
    /// <summary>
    /// 全景图地址
    /// </summary>
    public string JpegUrlFrame { get; set; }

    /// <summary>
    /// 全身照，base64编码 TODO 不确定是否还有这个字段
    /// </summary>
    public string BodyData { get; set; }
    
    /// <summary>
    /// 全身照地址 TODO 不确定是否还有这个字段
    /// </summary>
    public string JpegUrlBody { get; set; }
  }
}