using System.Text;

namespace Yuexintu.Simulator;

public class FaceCapCamaraSystemConfig
{
	#region 摄像机页面中设置

	public string HttpServerUrl { get; set; }
	public string WebSocketServerUrl { get; set; }

	#endregion

	#region 服务端发送命令控制的参数

	public class TokenModel
	{
		/// <summary>
		/// 最后一次给服务器发送心跳的时间
		/// </summary>
		private DateTime _lastHeartBeatTime = DateTime.Now;
		/// <summary>
		/// 服务器最后一次颁发这个Token的时间,也就是这个Token的有效期开始时间
		/// </summary>
		private DateTime _lastTokenAwardTime = DateTime.Now;
		/// <summary>
		/// 服务器的地址
		/// </summary>
		private string _serverUrl;
		public TokenModel(string serverUrl)
		{
			_serverUrl = serverUrl;
			_lastTokenAwardTime = DateTime.Now;
			_lastHeartBeatTime = DateTime.Now;
		}
		/// <summary>
		/// Token字符串
		/// </summary>
		public string Token { get; set; }
		/// <summary>
		/// Token的有效期时间,单位是秒
		/// </summary>
		public int Expire { get; set; }
		/// <summary>
		/// 需要给服务器发送心跳的时间间隔,单位是秒
		/// </summary>
		public int Interval { get; set; }
	}

	/// <summary>
	/// 服务器授权给摄像机的Token,key是服务器的地址,value是Token
	/// </summary>
	public TokenModel TokenAwardFromServer { get; set; }

	#endregion

	public override string ToString()
	{
		var sb = new StringBuilder();
		sb.AppendLine($"HttpServerUrl: {HttpServerUrl}");
		sb.AppendLine($"WebSocketServerUrl: {WebSocketServerUrl}");
		return sb.ToString();
	}

	private static List<FaceCapCamaraSystemConfig> MockFaceCapCamaraSystemConfigs()
	{
		#region 服务器地址列表

		var httpServerUrls = new List<string>
		{
			"http://localhost:5010",
			"http://localhost:5011",
			"http://localhost:5012",
			"http://localhost:5013",
		};

		var webSocketServerUrls = new List<string>
		{
			"ws://localhost:5011",
			"ws://localhost:5012",
			"ws://localhost:5011/ws",
			"ws://localhost:5012/ws",
			"wss://localhost:5011/ws",
			"wss://localhost:5012/ws",
		};

		#endregion

		var random = new Random();
		return new List<FaceCapCamaraSystemConfig>
		{
			new()
			{
				HttpServerUrl = httpServerUrls[random.Next(httpServerUrls.Count)],
				WebSocketServerUrl = webSocketServerUrls[random.Next(webSocketServerUrls.Count)],
			}
		};
	}
	
	public static FaceCapCamaraSystemConfig MockFaceCapCamaraSystemConfig()
	{
		var random = new Random();
		var configs = MockFaceCapCamaraSystemConfigs();
		return configs[random.Next(configs.Count)];
	}
}