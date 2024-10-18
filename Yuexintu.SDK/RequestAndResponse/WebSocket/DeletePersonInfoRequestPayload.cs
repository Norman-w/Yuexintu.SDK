/*
 
 
 
 1.15 删除人员信息
   服务器删除人员信息，
   {
   "msgid": "adfdgwrt245356",
   "token"："adf4453456ghhjgsf23546y",
   "data": {
   "uri"："/person/delete",
   "param": {
   "pid": "123456"
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
   固定为"person/delete"
   是
    
   param
   对象
    
   是
    
    
   pid
   字符串
   一般为整数形式的字符串，最大不超过264
   否
    
    
   响应
   {
   "msgid": "adfdgwrt245356",
   "data": {
   "uri": "/person/delete",
   "code": 0,
   "msg": "OK",
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
   固定为"person/delete"
   否
    
   code
   整型
   错误码，详见错误码定义。
   是
    
   msg
   字符串
   执行结果描述。
   否
   



*/

namespace Yuexintu.SDK.RequestAndResponse.WebSocket;

/// <summary>
/// 删除人员信息请求载荷
/// </summary>
public class DeletePersonInfoRequestPayload : WebSocketRequestPayload
{
	public const string Uri = "/ivp/face/delete";
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
		public static string Uri => DeletePersonInfoRequestPayload.Uri;

		/// <summary>
		/// 参数
		/// </summary>
		public ParamModel Param { get; set; }

		/// <summary>
		/// 参数模型
		/// </summary>
		public class ParamModel
		{
			/// <summary>
			/// 人员ID
			/// </summary>
			public string Pid { get; set; }
		}
	}

	public override string GetUri() => Uri;
}