/*
 
 
 
 1.3 设备心跳
   
   设备上报心跳到服务器
   {
   "msgid ": "adfdgwrt245356",
   "token"："adf4453456ghhjgsf23546y",
   "data": {
   "uri"："/heartbeat",
   "param": {
   "did"："1234512",
   "sn"："Y3C122AP1234512",
   "ip"："192.168.100.168"
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
   固定为"/heartbeat"
   是
    
   param
   对象
    
   是
    
    
   did
   字符串
    
   是
    
    
   sn
   字符串
   设备系列号
   是
    
    
   ip
   字符串
   设备的局域网IP地址
   是
    
    
   响应
   {
   "msgid": "adfdgwrt245356",
   "data": {
   "uri"："/heartbeat",
   "code": 200,
   "msg": "OK",
   "result": {
          ​"expire": 3599
   }
   ​}
   }
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
   固定为"/heartbeat"
   否
    
   code
   整数
   错误码，详见错误码定义。
   是
    
   msg
   字符串
   执行结果描述。
   否
    
   result
   对象
   执行成功后返回的数据，失败时该字段为空。
   否
    
    
   expire
   整数
   表示该token的有效时间(秒)。
   是
    
    
   为确保设备时刻在线，设备需要在心跳间隔时间(由connect命令获得的interval参数)内循环发送心跳命令。expire 表示该token的剩余有效时间。
    


*/

namespace Yuexintu.SDK.RequestAndResponse.WebSocket;

/// <summary>
/// 设备心跳请求载荷
/// </summary>
public class DeviceHeartbeatRequestPackage : WebSocketRequestPackage
{
	private const string Uri = "/heartbeat";
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
			/// 设备ID
			/// </summary>
			public string Did { get; set; }
			/// <summary>
			/// 设备系列号
			/// </summary>
			public string Sn { get; set; }
			/// <summary>
			/// 设备的局域网IP地址
			/// </summary>
			public string Ip { get; set; }
		}
		public string Uri => DeviceHeartbeatRequestPackage.Uri;
		public ParamModel Param { get; set; }
	}

	public override string GetUri() => Uri;
}