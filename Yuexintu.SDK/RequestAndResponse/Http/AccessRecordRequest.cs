/*
 
 
 文档中并没有提及到这个接口,但是在实际测试中摄像机捕捉到了这个请求.
 请求路径为 api/v1/access/record
 
 请求的内容为:
         //ValueKind = Object : "{"sn":"WBU1249Q1000201","data":[{"rid":1,"did":0,"pid":9223372036854775808,"type":1,"status":1,"accessTime":"2024-10-22 09:21:04","faceImg":null,"temperature":36,"score":86}]}"
         
 该类为根据如上请求内容生成的请求类
 
 随后在读取put请求时,读取到:
 {
     "sn": "WBU1249Q1000201",
     "data": [
       {
         "rid": 1,
         "did": 0,
         "pid": 9223372036854775808,
         "type": 1,
         "status": 1,
         "accessTime": "2024-10-22 09:49:04",
         "faceImg": null,
         "temperature": 36,
         "score": 86
       }
     ]
   }
   
   但解析报错,因为pid的值超出了long的范围,无法转换.改为ulong后,可以正常解析.
 */
namespace Yuexintu.SDK.RequestAndResponse.Http;

/// <summary>
/// 摄像机-->服务器
/// 访问记录请求,当摄像机检测到人脸时,会发送这个请求
/// TODO 待确认是不是只有通过验证的人脸才会发送这个请求
/// </summary>
public class AccessRecordRequest
{
	/// <summary>
	/// 发送请求的设备序列号(哪个设备检测到了人脸)
	/// </summary>
	public string Sn { get; set; }
	/// <summary>
	/// 访问记录数据
	/// </summary>
	public List<AccessRecordData> Data { get; set; }

	public class AccessRecordData
	{
		/// <summary>
		/// 访问记录ID
		/// </summary>
		public long Rid { get; set; }
		/// <summary>
		/// 设备ID
		/// </summary>
		public long Did { get; set; }
		/// <summary>
		/// 人员ID,注意这里不能用long,否则超出范围无法转换将抛出异常
		/// </summary>
		public ulong Pid { get; set; }
		/// <summary>
		/// TODO 访问类型具体定义不详
		/// </summary>
		public int Type { get; set; }
		/// <summary>
		/// TODO 访问状态具体定义不详
		/// </summary>
		public int Status { get; set; }
		/// <summary>
		/// 访问时间,格式为 yyyy-MM-dd HH:mm:ss,本地时间,不带时区
		/// </summary>
		public string AccessTime { get; set; }
		/// <summary>
		/// 人脸图片(可能为空)
		/// </summary>
		public string? FaceImg { get; set; }
		/// <summary>
		/// 体温 (摄氏度)
		/// </summary>
		public float Temperature { get; set; }
		/// <summary>
		/// 人脸识别分数
		/// </summary>
		public float Score { get; set; }
	}
}