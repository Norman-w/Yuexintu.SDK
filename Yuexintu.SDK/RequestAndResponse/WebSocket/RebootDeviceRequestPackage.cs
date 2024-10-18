/*
 
 
 
 1.9 重启设备
   服务器重启设备，
   {
   "msgid": "adfdgwrt245356",
   "token"："adf4453456ghhjgsf23546y",
   "data": {
   "uri"："/system/maintain/reboot",
   "param": {
   "delay_ms": 1000
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
   固定为"system/maintain/reboot"
   是
    
   param
   对象
    
   是
    
    
   delay_ms
   整型
   重启延时，单位：ms
   是
    
    
   响应
   {
   "msgid": "adfdgwrt245356",
   "data": {
   "uri": "/system/maintain/reboot",
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
   固定为"system/maintain/reboot"
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
/// 重启设备请求载荷
/// </summary>
public class RebootDeviceRequestPackage : WebSocketRequestPackage
{
	private const string Uri = "/system/maintain/reboot";
	/// <summary>
	/// 数据部分
	/// </summary>
	public DataModel Data { get; set; }
	public string Token { get; set; }

	/// <summary>
	/// 数据模型
	/// </summary>
	public class DataModel
	{
		public string Uri => RebootDeviceRequestPackage.Uri;
		public ParamModel Param { get; set; }
		public class ParamModel
		{
			/// <summary>
			/// 重启延时，单位：ms
			/// </summary>
			public int DelayMs { get; set; }
		}
	}

	public override string GetUri() => Uri;
}