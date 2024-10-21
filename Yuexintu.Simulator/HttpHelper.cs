using System.Net.Http.Headers;

namespace Yuexintu.Simulator;

internal static class HttpHelper
{
	public static async Task<string> PostAsync(string baseUrl, string uri, byte[] data, Dictionary<string, string> header, string authorization)
	{
		using var httpClient = new HttpClient();
		var content = new ByteArrayContent(data);
		foreach (var (key, value) in header)
		{
			content.Headers.Add(key, value);
		}
		//这样加这个header不行:Misused header name, 'Authorization'. Make sure request headers are used with HttpRequestMessage, response headers with HttpResponseMessage, and content headers with HttpContent objects.
		// content.Headers.Add("Authorization", authorization);
		//需要这样加:
		httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization);
		var response = await httpClient.PostAsync($"{baseUrl}{uri}", content);
		return await response.Content.ReadAsStringAsync();
	}
}