using Yuexintu.SDK.Service.Api;

namespace Yuexintu.SDK.RequestAndResponse.Http;

public class QueryOtaVersionResponse
{
	/// <summary>
	/// 错误编码
	/// </summary>
	public int Code { get; set; }
	/// <summary>
	/// 响应信息
	/// </summary>
	public string Msg { get; set; }

	public QueryOtaVersionData Data { get; set; }
	
	public class QueryOtaVersionData
	{
		public int VerCode { get; set; }
		public string VerName { get; set; }
		public string Description { get; set; }
		public string ReleaseDate { get; set; }
		public List<ComponentModel> ComponentList { get; set; }
    
		public class ComponentModel
		{
			public string Name { get; set; }
			public int Type { get; set; }
			public string Md5 { get; set; }
			public int Size { get; set; }
			public string Url { get; set; }
			public string Sha256 { get; set; }
		}
	}
}