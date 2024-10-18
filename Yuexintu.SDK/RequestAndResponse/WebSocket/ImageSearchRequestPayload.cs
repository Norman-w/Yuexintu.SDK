/*
 
 
 1.16 以图搜图
   服务器以图搜图，
   {
   "msgid": "adfdgwrt245356",
   "token"："adf4453456ghhjgsf23546y",
   "data": {
   "uri"："/person/search",
   "param": {
   "photo": "base64",
   "threshold": 80,
   "start_time": "2021-10-03 10:30:00",
   "end_time": "2021-10-03 16:00:00"
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
   固定为"person/search"
   是
    
   param
   对象
    
   是
    
    
   photo
   字符串
   图片的base64编码
   否
    
    
   threshold
   整型
   比对阈值，有效值为0 - 100
   是
    
    
   start_time
   字符串
   起始时间，格式为：yyyy-MM-dd HH:mm:ss
   是
    
    
   end_time
   字符串
   结束时间，格式为：yyyy-MM-dd HH:mm:ss
   是
    
    
   响应
   {
   "msgid": "adfdgwrt245356",
   "data": {
   "uri": "/person/search",
   "code": 0,
   "msg": "OK",
   "result": {
   "total": 16,
   "face_list": [
   {
   "time":"2022-09-24 14:27:41",
   "score": 90,
   "jpeg_url_face":"sdcard/20210924/14/2741_179.jpg",
   },
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
   固定为"person/search"
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
    
    
   total
   整型
    
   是
    
    
   face_list
   数组
    
   否
    
    
    
   time
   字符串
   图片时间
   是
    
    
    
   score
   整型
   相似度分数
   是
    
    
    
   jpeg_url_face
   字符串
   抓拍的人脸图片路径
   是


*/


namespace Yuexintu.SDK.RequestAndResponse.WebSocket;

/// <summary>
/// 以图搜图请求载荷
/// </summary>
public class ImageSearchRequestPayload : WebSocketRequestPayload
{
	private const string Uri = "/person/search";
	/// <summary>
	/// 数据部分
	/// </summary>
	public DataModel Data { get; set; }

	/// <summary>
	/// 数据模型
	/// </summary>
	public class DataModel
	{
		public class ParamModel
		{
			/// <summary>
			/// 图片的base64编码
			/// </summary>
			public string Photo { get; set; }

			/// <summary>
			/// 比对阈值，有效值为0 - 100
			/// </summary>
			public int Threshold { get; set; }

			/// <summary>
			/// 起始时间，格式为：yyyy-MM-dd HH:mm:ss
			/// </summary>
			public string StartTime { get; set; }

			/// <summary>
			/// 结束时间，格式为：yyyy-MM-dd HH:mm:ss
			/// </summary>
			public string EndTime { get; set; }
		}
		public ParamModel Param { get; set; }
		public string Uri => ImageSearchRequestPayload.Uri;
	}

	public override string GetUri() => Uri;
}