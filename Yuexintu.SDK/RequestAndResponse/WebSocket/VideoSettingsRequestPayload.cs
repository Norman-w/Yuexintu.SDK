/*
 
 
 1.6 录像设置
   
   服务器设置录像参数，
   {
   "msgid": "adfdgwrt245356",
   "token"："adf4453456ghhjgsf23546y",
   "data": {
   "uri"："/storage/record_set",
   "param": {
   ​"normal_record" : 1,
   ​"disconnect_record" : 1,
   ​"alarm_record": 1,
   ​"audio_enable":1,
   ​"file_len":5,
   ​"strategy" : 0,
   ​"face_server_enable": 1,
   ​"pre_record":
   ​ {
   ​​"enable":1
   ​​"duration":10
   ​ }
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
   固定为"storage/record_set"
   是
    
   param
   对象
    
   是
    
    
   normal_record
   布尔
   手动录像
   是
    
    
   disconnect_record
   布尔
   断网录像
   是
    
    
   alarm_record
   布尔
   报警录像
   是
    
    
   audio_enable
   布尔
   是否录制音频
   是
    
    
   file_len
   整形
   录像时长 1-60min
   是
    
    
   strategy
   整形
   0 -1 0是循环覆盖，1是录满停止
   是
    
    
   pre_record
   对象
   预录
   是
    
    
    
   enable
   布尔
   开启预录
   是
    
    
    
   duration
   时长
   0-10s
   是
    
    
   响应
   {
   "msgid": "adfdgwrt245356",
   "data": {
   "uri"：" /storage/record_set",
   "code": 200,
   "msg": "OK"
   ​}
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
   固定为"storage/record_set"
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
/// 录像设置请求载荷
/// </summary>
public class VideoSettingsRequestPayload : WebSocketRequestPayload
{
	private const string Uri = "/storage/record_set";
	/// <summary>
	/// 数据部分
	/// </summary>
	public DataModel Data { get; set; }

	public string Token { get; set; }
	
	public class DataModel
	{
		public string Uri { get; set; } = VideoSettingsRequestPayload.Uri;
		/// <summary>
		/// 手动录像
		/// </summary>
		public bool NormalRecord { get; set; }
		/// <summary>
		/// 断网录像
		/// </summary>
		public bool DisconnectRecord { get; set; }
		/// <summary>
		/// 报警录像
		/// </summary>
		public bool AlarmRecord { get; set; }
		/// <summary>
		/// 是否录制音频
		/// </summary>
		public bool AudioEnable { get; set; }
		/// <summary>
		/// 录像时长 1-60min
		/// </summary>
		public int FileLen { get; set; }
		/// <summary>
		/// 0 -1 0是循环覆盖，1是录满停止
		/// </summary>
		public int Strategy { get; set; }
		/// <summary>
		/// 预录
		/// </summary>
		public PreRecordModel PreRecord { get; set; }
		
		public class PreRecordModel
		{
			/// <summary>
			/// 开启预录
			/// </summary>
			public bool Enable { get; set; }
			/// <summary>
			/// 时长 0-10s
			/// </summary>
			public int Duration { get; set; }
		}
	}

	public override string GetUri() => Uri;
}