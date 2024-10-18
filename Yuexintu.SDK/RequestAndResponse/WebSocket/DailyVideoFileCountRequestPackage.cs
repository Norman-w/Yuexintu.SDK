/*
 
 
 
 1.8 按月获取每天的视频文件数量
   
   服务器显示获取的视频文件信息，
   {
   "msgid": "adfdgwrt245356",
   "token"："adf4453456ghhjgsf23546y",
   "data": {
   "uri"："/storage/get_record_list",
   "param": {
   "start_time": "2021-10-03 10:30:00",
   "end_time": "2021-10-03 16:00:00",
   "page_num": 0,
   "page_size": 100
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
   固定为"storage/get_record_list"
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
   页面，从0开始
   是
    
    
   page_size
   整型
   每页记录条数，最大不超过500
   是
    
    
   响应
   {
   "msgid": "adfdgwrt245356",
   "data": {
   "uri": "/storage/get_record_list",
   "code": 0,
   "msg": "OK",
   "result": {
   "total_page":1,
   "total_file":68,
   "page_size":100,
   "recordlist": [
   {
   "filename":"/progs/rec/00/20211003/N02180546.mp4",
   "start_time":"2021-10-03 10:05:46",
   "end_time":"2021-10-03 10:15:48",
   },
   ...
   ]
   }
    
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
   固定为"storage/get_record_list"
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
    
    
   total_page
   整型
   该时间段包含的视频总页数
   是
    
    
   total_file
   整型
   该时间段包含的视频数量
   是
    
    
   page_size
   整型
   每页记录条数，最大不超过500
   是
    
    
   recordlist
   对象
    
   否
    
    
    
   filename
   字符串
   视频文件的名称或路径
   否
    
    
    
   start_time
   字符串
   该文件开始录像的时间，格式为yyyy-MM-dd HH:mm:ss
   否
    
    
    
   end_time
   字符串
   停止录像的时间
   否



*/

namespace Yuexintu.SDK.RequestAndResponse.WebSocket;

/// <summary>
/// 每日视频文件数量请求载荷
/// </summary>
public class DailyVideoFileCountRequestPackage : WebSocketRequestPackage
{
	private const string Uri = "/storage/get_record_list";
	public string Token { get; set; }
	/// <summary>
	/// 数据部分
	/// </summary>
	public DataModel Data { get; set; } = new DataModel();

	/// <summary>
	/// 数据模型
	/// </summary>
	public class DataModel
	{
		public string Uri => DailyVideoFileCountRequestPackage.Uri;
		public ParamModel Param { get; set; }
		public class ParamModel
		{
			/// <summary>
			/// 起始时间，格式为：yyyy-MM-dd HH:mm:ss
			/// </summary>
			public string StartTime { get; set; }
			/// <summary>
			/// 结束时间，格式为：yyyy-MM-dd HH:mm:ss
			/// </summary>
			public string EndTime { get; set; }
			/// <summary>
			/// 页面，从0开始
			/// </summary>
			public int PageNum { get; set; }
			/// <summary>
			/// 每页记录条数，最大不超过500
			/// </summary>
			public int PageSize { get; set; }
		}
	}

	public override string GetUri() => Uri;
}