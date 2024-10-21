/*
 
 
 1.14 查询人员信息
   服务器查询人员信息，
   {
   "msgid": "adfdgwrt245356",
   "token"："adf4453456ghhjgsf23546y",
   "data": {
   "uri"："/person/query",
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
   固定为"person/query"
   是
    
   param
   对象
    
   是
    
    
   pid
   字符串
   一般为整数形式的字符串，最大不超过264
   是
    
    
   响应
   {
   "msgid": "adfdgwrt245356",
   "data": {
   "uri": "/person/query",
   "code": 0,
   "msg": "OK",
   "result": {
   "pid": "123456",
   "name": "姓名",
   "work_id": "工号",
   "id_card_no": "身份证号码",
   "ic_card_no": "IC卡号",
   "gender": 1,
   "age": 25,
   "department": "市场部",
   "photo": "sdcard/20210924/14/2741_179.jpg",
   "category": "类别",
   "phone": "136xxxxxxxx"
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
   固定为"person/query"
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
    
   是
    
    
   pid
   字符串
   一般为整数形式的字符串，最大值不超过264。
   是
    
    
   name
   字符串
   姓名
   是
    
    
   work_id
   字符串
   工号
   是
    
    
   id_card_no
   字符串
   身份证号码
   是
    
    
   ic_card_no
   字符串
   IC卡号码
   是
    
    
   gender
   整型
   性别，1-男 2-女
   是
    
    
   age
   整型
   年龄
   是
    
    
   department
   字符串
   部门名称
   是
    
    
   photo
   字符串
   人脸照片路径或ID，可通过"读取文件(抓拍图片)内容"接口获取图片内容。
   是
    
    
   category
   整型
   类别，1:白名单；2:黑名单；3:VIP；4:访客
   是
    
    
   phone
   字符串
   手机号
   是
   

*/


namespace Yuexintu.SDK.RequestAndResponse.WebSocket;

/// <summary>
/// 人员信息请求载荷
/// </summary>
public class PersonInfoRequestPackage : WebSocketRequestPackage
{
	private const string Uri = "/person/query";
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
		public string Uri => PersonInfoRequestPackage.Uri;
		public class ParamModel
		{
			/// <summary>
			/// 人员ID
			/// </summary>
			public string Pid { get; set; }
		}
		public ParamModel Param { get; set; }
	}

	public override string GetUri() => Uri;
}