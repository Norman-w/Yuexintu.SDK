/*
 
 
 1.10 查询抓拍图像的文件列表
   服务器查询抓拍文件列表，
   {
   "msgid": "adfdgwrt245356",
   "token"："adf4453456ghhjgsf23546y",
   "data": {
   "uri"："/ivp/face/get_cap_history",
   "param": {
   "start_time": "2021-10-03 10:30:00",
   "end_time": "2021-10-03 16:00:00",
   "page_num": 1,
   "page_size": 32,
   "name": "张三",
   "workId":"10091",
   "idCard":"xxxxxxxxxxxxxxxxxx",
   "phone":"136xxxxxxxx"
   }
    
   }
   }
   参数说明：
   参数
   类型
   说明
   是否必须
   msgid
   字符串
   消息ID，由设备生成，每条消息对应一个唯一的ID
   是
   token
   字符串
   会话token，设备connect服务器时，由服务器返回
   是
   data
   对象
    
   是
    
   uri
   字符串
   固定为"ivp/face/get_cap_history"
   是
    
   param
   对象
    
   是
    
    
   start_time
   字符串
   起始时间，格式为：yyyy-MM-dd HH:mm:ss
   是
    
    
   end_time
   字符串
   结束时间，格式为：yyyy-MM-dd HH:mm:ss
   是
    
    
   page_num
   整型
   页面
   是
    
    
   page_size
   整型
   每页记录条数，最大不超过32
   是
    
    
   name
   字符串
   指定姓名
   否
    
    
   workId
   字符串
   指定工号
   否
    
    
   idCard
   字符串
   指定身份证号
   否
    
    
   phone
   字符串
   指定手机号
   否
    
    
   响应
   {
   "msgid": "adfdgwrt245356",
   "data": {
   "uri": "/ivp/face/get_cap_history",
   "code": 0,
   "msg": "OK",
   "result": {
   "face_total":916,
   "face_list": [
   {
   "name":"张三",
   "similarity": 80,
   "gender": 2,
   "age": 27,
   "time":"2022-09-24 14:27:41",
   "jpeg_url_face":"sdcard/20210924/14/2741_179.jpg",
   "jpeg_url_body":"sdcard/20210924/14/2741_179_body.jpg",
   "jpeg_url_frame":"sdcard/20220924/14/2741_179_bg.jpg",
   },
   ...
   ]
   }
   }
   }
   参数说明：
   参数
   类型
   说明
   是否必须
   msgid
   字符串
   消息ID，与命令中的参数一致。
   是
   data
   对象
    
   是
    
   uri
   字符串
   固定为"ivp/face/get_cap_history"
   否
    
   code
   整型
   错误码，详见错误码定义。
   是
    
   msg
   字符串
   执行结果描述。
   否
    
   result
   对象
    
   否
    
    
   face_total
   整型
   该时间段包含的抓拍记录数量
   是
    
    
   face_list
   数组
    
   是
    
    
    
   time
   字符串
   抓拍的时间点，格式为yyyy-MM-dd HH:mm:ss
   是
    
    
    
   jpeg_url_face
   字符串
   抓拍的人脸图片路径
   否
    
    
    
   jpeg_url_body
   字符串
   抓拍的人体全身图片路径
   否
    
    
    
   jpeg_url_frame
   字符串
   抓拍记录对应的全景图片路径
   是
    
    
    
   name
   字符串
   姓名
   否
    
    
    
   similarity
   整型
   相似度
   否
    
    
    
   gender
   整型
   性别，1-男 2-女
   否
    
    
    
   age
   整型
   年龄
   否
    


*/


namespace Yuexintu.SDK.RequestAndResponse.WebSocket;

/// <summary>
/// 抓拍图像文件列表请求载荷
/// </summary>
public class CaptureImageFileListRequestPayload : WebSocketRequestPayload
{
	private const string Uri = "/ivp/face/get_cap_history";
	/// <summary>
	/// 消息ID,由服务端生成,每条消息对应一个唯一的ID
	/// TODO 待确认
	/// </summary>
	public string Token { get; set; }
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
		/// URI
		/// </summary>
		public static string Uri => CaptureImageFileListRequestPayload.Uri;
		
		/// <summary>
		///  参数
		/// </summary>
		public ParamModel Param { get; set; }
		
		/// <summary>
		/// 参数模型
		/// </summary>
		public class ParamModel
		{
			/// <summary>
			/// 起始时间
			/// </summary>
			public string StartTime { get; set; }
			
			/// <summary>
			/// 结束时间
			/// </summary>
			public string EndTime { get; set; }
			
			/// <summary>
			/// 页码
			/// </summary>
			public int PageNum { get; set; }
			
			/// <summary>
			/// 每页记录条数
			/// </summary>
			public int PageSize { get; set; }
			
			/// <summary>
			/// 姓名
			/// </summary>
			public string Name { get; set; }
			
			/// <summary>
			/// 工号
			/// </summary>
			public string WorkId { get; set; }
			
			/// <summary>
			/// 身份证号
			/// </summary>
			public string IdCard { get; set; }
			
			/// <summary>
			/// 手机号
			/// </summary>
			public string Phone { get; set; }
		}
	}

	public override string GetUri() => Uri;
}