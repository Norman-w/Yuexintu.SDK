/*


 1.5 人脸参数控制

   服务器控制人脸参数，
   {
   "msgid": "adfdgwrt245356",
   "token"："adf4453456ghhjgsf23546y",
   "data": {
   "uri"："/ivp/person_param_set",
   "param": {
   "enable" : true,
   "show_face" : true,
   "show_human" : true,
   "push_stranger" : true,
   "push_face" : true,
   "push_bg" : true,
   "play_audio" :false,
   "play_audio_stranger":true,
   "play_audio_white":true,
   "play_audio_black":true,
   "play_audio_vip":true,
   "play_audio_friend":true,
   "threshold" : 50,
   "reg_threshold":85,
   "mini_face": 50,
   "mini_human":50,
   "quality_face":50,
   "quality_human":50,
   "server_enable":true,
   "alarm_out":{
   "interval":10
   ​​"alarm_push":true
   }
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
   固定为"ivp/facecap_param_set"
   是

   param
   对象

   是


   enable
   布尔
   1打开 0关闭
   是


   show_face
   布尔
   显示人脸框
   是


   show_human
   布尔
   显示人形框
   是


   push_stranger
   布尔
   推送陌生人
   是


   push_face
   布尔
   推送人脸图
   是


   push_bg
   布尔
   推送背景图
   是


   play_audio
   布尔
   语音播报
   是


   play_audio_stranger
   布尔
   陌生人语音播报
   否


   play_audio_white
   布尔
   白名单语音播报
   否


   play_audio_black
   布尔
   黑名单语音播报
   否
   i

   play_audio_vip
   布尔
   VIP语音播报
   否


   play_audio_friend
   布尔
   访客语音播报
   否


   threshold
   整型
   检测阈值 0-100
   是


   reg_threshold
   整型
   识别阈值 0-100
   是


   mini_face
   整型
   人脸大小 30-500
   是


   mini_human
   整型
   人体大小 60-1000
   是


   quality_face
   整型
   人脸质量 0-100
   是


   quality_human
   整型
   人体质量 0 -100
   是


   server_enable
   布尔
   使能人脸服务器



   alarm_out
   对象
   人脸报警 10-60s
   是



   interval
   整型
   报警间隔
   是



   alarm_push
   布尔
   服务器推送
   是


   响应
   {
   "msgid": "adfdgwrt245356",
   "data": {
   "uri"：" /ivp/facecap_param_set",
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
   固定为"ivp/facecap_param_set"
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
/// 人脸参数控制请求载荷
/// </summary>
public class FaceParameterControlRequestPackage : WebSocketRequestPackage
{
	private const string Uri = "/ivp/facecap_param_set";

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
		/// 设备ID
		/// </summary>
		public string Did { get; set; }

		/// <summary>
		/// 参数部分
		/// </summary>
		public ParamModel Param { get; set; }

		/// <summary>
		/// 参数模型
		/// </summary>
		/// <summary>
        /// 参数模型
        /// </summary>
        public class ParamModel
        {
            /// <summary>
            /// 1打开 0关闭
            /// </summary>
            public bool Enable { get; set; }

            /// <summary>
            /// 显示人脸框
            /// </summary>
            public bool ShowFace { get; set; }

            /// <summary>
            /// 显示人形框
            /// </summary>
            public bool ShowHuman { get; set; }

            /// <summary>
            /// 推送陌生人
            /// </summary>
            public bool PushStranger { get; set; }

            /// <summary>
            /// 推送人脸图
            /// </summary>
            public bool PushFace { get; set; }

            /// <summary>
            /// 推送背景图
            /// </summary>
            public bool PushBg { get; set; }

            /// <summary>
            /// 语音播报
            /// </summary>
            public bool PlayAudio { get; set; }

            /// <summary>
            /// 陌生人语音播报
            /// </summary>
            public bool PlayAudioStranger { get; set; }

            /// <summary>
            /// 白名单语音播报
            /// </summary>
            public bool PlayAudioWhite { get; set; }

            /// <summary>
            /// 黑名单语音播报
            /// </summary>
            public bool PlayAudioBlack { get; set; }

            /// <summary>
            /// VIP语音播报
            /// </summary>
            public bool PlayAudioVip { get; set; }

            /// <summary>
            /// 访客语音播报
            /// </summary>
            public bool PlayAudioFriend { get; set; }

            /// <summary>
            /// 检测阈值 0-100
            /// </summary>
            public int Threshold { get; set; }

            /// <summary>
            /// 识别阈值 0-100
            /// </summary>
            public int RegThreshold { get; set; }

            /// <summary>
            /// 人脸大小 30-500
            /// </summary>
            public int MiniFace { get; set; }

            /// <summary>
            /// 人体大小 60-1000
            /// </summary>
            public int MiniHuman { get; set; }

            /// <summary>
            /// 人脸质量 0-100
            /// </summary>
            public int QualityFace { get; set; }

            /// <summary>
            /// 人体质量 0-100
            /// </summary>
            public int QualityHuman { get; set; }

            /// <summary>
            /// 使能人脸服务器
            /// </summary>
            public bool ServerEnable { get; set; }

            /// <summary>
            /// 人脸报警
            /// </summary>
            public AlarmOutModel AlarmOut { get; set; }

            public class AlarmOutModel
            {
                /// <summary>
                /// 报警间隔
                /// </summary>
                public int Interval { get; set; }

                /// <summary>
                /// 服务器推送
                /// </summary>
                public bool AlarmPush { get; set; }
            }
        }
	}

	public override string GetUri() => Uri;
}