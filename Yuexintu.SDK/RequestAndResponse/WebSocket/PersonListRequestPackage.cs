/*
 
 
 
 1.13 查询人员列表
   服务器查询人员列表，
   {
   "msgid": "adfdgwrt245356",
   "token"："adf4453456ghhjgsf23546y",
   "data": {
   "uri"："/person/list",
   "param": {
   "page_num":1,
   "page_size":32,
   "category":1,
   "name":"张三",
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
   固定为"person/list"
   是
    
   param
   对象
    
   是
    
    
   name
   字符串
   姓名
   是
    
    
   workId
   字符串
   工号
   是
    
    
   idCard
   字符串
   身份证号
   是
    
    
   phone
   整型
   手机号
   是
    
    
   category
   整型
   类别，1:白名单；2:黑名单；3:VIP；4:访客
   是
    
    
   page_num
   整型
   页面
   是
    
    
   page_size
   整型
   每页记录条数，最大不超过32
   是
    
    
   响应
   {
   "msgid": "adfdgwrt245356",
   "data": {
   "uri": "/person/list",
   "code": 0,
   "msg": "OK",
   "result": {
   "total": 916,
   "list": [
   {
   "pid":"123456",
   "name": "张三",
   "work_id": "123457089",
   "id_card_no": "",
   "ic_card_no": "",
   "department": "测试部",
   "photo": "data/facelib/19017.jpg",
   "phone": "",
   "gender": 2,
   "age": 27,
   "category": 1
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
   固定为"person/list"
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
   结果总数量
   是
    
    
   list
   数组
    
   否
    
    
    
   pid
   字符串
   一般为整数形式的字符串，最大不超过264
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
   身份证号
   是
    
    
    
   ic_card_no
   字符串
   IC卡号
   是
    
    
    
   department
   字符串
   部门
   是
    
    
    
   photo
   字符串
   图片路径
   是
    
    
    
   phone
   字符串
   手机号
   是
    
    
    
   gender
   整型
   性别，1-男 2-女
   是
    
    
    
   age
   整型
   年龄
   是
    
    
    
   category
   整型
   类别，1:白名单；2:黑名单；3:VIP；4:访客
   是
    
   


*/

using Newtonsoft.Json;

namespace Yuexintu.SDK.RequestAndResponse.WebSocket;

/// <summary>
/// 人员列表请求载荷
/// </summary>
public class PersonListRequestPackage : WebSocketRequestPackage
{
	private const string Uri = "/person/list";
	
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
		public string Uri => PersonListRequestPackage.Uri;
		public ParamModel Param { get; set; }
		public class ParamModel
		{
			/// <summary>
			/// 页面
			/// </summary>
			[JsonProperty("page_num")]
			public int PageNum { get; set; }
			/// <summary>
			/// 每页记录条数，最大不超过32
			/// </summary>
			[JsonProperty("page_size")]
			public int PageSize { get; set; }
			/// <summary>
			/// 类别，1:白名单；2:黑名单；3:VIP；4:访客
			/// </summary>
			public int Category { get; set; }
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
		/// <summary>
		/// 设备ID
		/// </summary>
		public string Did { get; set; }
	}

	public override string GetUri() => Uri;
}