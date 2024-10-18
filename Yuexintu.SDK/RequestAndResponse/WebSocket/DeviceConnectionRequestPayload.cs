/*
 
 
 
 1.1 连接服务器
   
   设备连接服务器
   {
   "msgid": "adfdgwrt245356",
   "data": {
   "uri"："/connect",
   "param": {
   "did"："1234512",
   "sn"："Y3C122AP1234512",
   "ip"："192.168.100.168",
   "sign": "567yuisfghg435646hgf56768",
   "ts": 1635066908
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
   data
   对象
    
   是
    
   uri
   字符串
   固定为"/connect"
   是
    
   param
   对象
    
   是
    
    
   did
   字符串
   一般为整数形式的字符串，最大不超过264。
   否
    
    
   sn
   字符串
   设备系列号
   是
    
    
   ip
   字符串
   设备的局域网IP地址
   是
    
    
   sign
   字符串
   设备端签名,用以校验设备的合法性，详见下文说明
   是
    
    
   ts
   整数
   UNIX时间戳，单位：秒
   是
    
    
   签名字段(sign)计算方法：Base64 { RSA [ MD5(设备SN + 设备安全码 + 时间戳) ] }。
   即先将15位设备序列号、8位安全码和当前的时间戳拼接组成字符串，并计算该字符串的MD5值，再用设备私钥进行签名，最后转换成Base64编码字符串。
   设备ID字段(did)默认为空，在用户添加设备时由平台分配并下发至设备保存。设备恢复出厂设置后，设备上保存的did参数应当清除。如果did为空，此接口将返回421错误。
   响应
   {
   ​"msgid": "adfdgwrt245356",
   ​"data": {
   "uri"："/connect",
   "code": 200,
   "msg": "OK",
   "result": {
          ​"token": "adf4453456ghhjgsf23546y",
          ​"expire": 3600,
   "interval"：30
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
   固定为"/connect"
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
    
    
   token
   字符串
   由服务器生成，不超过32字节。该字段将用于其它WebSocket和HTTP接口身份认证。
   是
    
    
   expire
   整数
   表示该token的有效时间(秒)，设备需要在有效期内更新token，否则就需要重新连接。token的有效期一般为30分钟到2小时之间。
   是
    
    
   interval
   整数
   设备心跳频率，即设备向平台发送心跳命令的最大间隔时间，单位：秒。
   是
    
    
   通常情况下，设备上电启动后，应当首先调用该接口向平台获取有效的token，然后才能调用其它接口。如果返回421错误，说明该设备尚未向平台注册，或者设备被恢复了出厂设置。此时设备应等待平台下发初始化参数后再调用该接口连接平台。
   



*/

namespace Yuexintu.SDK.RequestAndResponse.WebSocket;

/// <summary>
/// 设备连接请求载荷
/// </summary>
public class DeviceConnectionRequestPayload : WebSocketRequestPayload
{
    private const string Uri = "/connect";
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
        /// 请求的URI
        /// </summary>
        public string Uri => DeviceConnectionRequestPayload.Uri;

        /// <summary>
        /// 参数部分
        /// </summary>
        public ParamModel Param { get; set; }

        /// <summary>
        /// 参数模型
        /// </summary>
        public class ParamModel
        {
            /// <summary>
            /// 设备ID
            /// </summary>
            public string Did { get; set; }

            /// <summary>
            /// 序列号
            /// </summary>
            public string Sn { get; set; }

            /// <summary>
            /// IP地址
            /// </summary>
            public string Ip { get; set; }

            /// <summary>
            /// 签名
            /// </summary>
            public string Sign { get; set; }

            /// <summary>
            /// 时间戳
            /// </summary>
            public long Ts { get; set; }
        }
    }

    public override string GetUri() => Uri;
}