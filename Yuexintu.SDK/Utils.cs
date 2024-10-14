using System.Security.Cryptography;
using System.Text;

namespace Yuexintu.SDK;

public static class Utils
{
	public static string GenerateSignature(Dictionary<string, string> parameters, string secret)
	{
		// 1. 按字母升序排序，并移除值为空的字段
		var sortedParams = parameters
			.Where(p => !string.IsNullOrEmpty(p.Value))
			.OrderBy(p => p.Key)
			.ToList();

		// 2. 拼接成一个字符串
		var stringBuilder = new StringBuilder();
		foreach (var param in sortedParams)
		{
			stringBuilder.Append(param.Key).Append(param.Value);
		}
		stringBuilder.Append(secret);

		// 3. 计算MD5值
		using (var md5 = MD5.Create())
		{
			var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(stringBuilder.ToString()));
			var sb = new StringBuilder();
			foreach (var b in hash)
			{
				sb.Append(b.ToString("X2"));
			}
			return sb.ToString();
		}
	}
}