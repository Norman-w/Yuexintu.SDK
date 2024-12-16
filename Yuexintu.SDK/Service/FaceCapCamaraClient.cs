/*
 
 
 
 当日志来源处客户端连接到本服务器后,产生的客户端实例,比如通过gRpc/ws/命名管道/内部调用等方式报送日志的客户端.



*/

using System.Net.WebSockets;
using System.Text;

namespace Yuexintu.SDK.Service;

public class FaceCapCameraClient : IClient
{
	public event ClientDisconnectedEventHandler? ClientDisconnected;

	
	public readonly string Id;
	private readonly WebSocket _webSocket;

	public static FaceCapCameraClient FromSession(SessionCreatedEventArgs session)
	{
		return new FaceCapCameraClient(session);
	}


	public FaceCapCameraClient(SessionCreatedEventArgs sessionCreatedEventArgs)
	{
		Id = sessionCreatedEventArgs.SessionId;
		_webSocket = sessionCreatedEventArgs.Connection as WebSocket;
	}

	/// <summary>
	/// 启动运行(WebSocket接收消息的回调函数)
	/// </summary>
	/// <returns></returns>
	public async Task StartWorking()
	{
		try
		{
			var buffer = new byte[1024 * 4];
			while (_webSocket.State == WebSocketState.Open)
			{
				var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
				if (result.MessageType == WebSocketMessageType.Close)
				{
					await OnClientDisconnected();
				}
				else if (result.MessageType == WebSocketMessageType.Text)
				{
					var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
					// Console.WriteLine($"接收到客户端消息: {message}");
					OnWebSocketMessageReceived(message);
				}
			}
		}
		catch (Exception e)
		{
			Console.WriteLine("读取客户端消息失败,错误信息:" + e.Message);
		}
		finally
		{
			if (_webSocket.State != WebSocketState.Closed)
			{
				_webSocket.Abort();
				_webSocket.Dispose();
			}
		}
	}
	private async Task OnClientDisconnected()
	{
		ClientDisconnected?.Invoke(this);
		await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client disconnected", CancellationToken.None);
	}
	
	/// <summary>
	/// 从网络收到消息时的回调函数, message是logRecord4Net的json字符串,不是业务模型
	/// </summary>
	/// <param name="message"></param>
	private void OnWebSocketMessageReceived(string message)
	{
		// Console.WriteLine($"Received message {message}");
		OnRequestMessageReceived?.Invoke(this, message);
		// try
		// {
		// 	var logRecord4Net = JsonConvert.DeserializeObject<LogRecord4Net>(message);
		// 	if (logRecord4Net == null)
		// 	{
		// 		Console.WriteLine($"{nameof(OnWebSocketMessageReceived)}: 无法解析日志记录, 请检查日志格式, message: {message}");
		// 		return;
		// 	}
		// 	try
		// 	{
		// 		// var log = Log.Model.Log.FromLogRecord4Net(logRecord4Net);
		// 		// LogReceived?.Invoke(this, log);
		// 	}
		// 	catch (Exception e)
		// 	{
		// 		Console.WriteLine($"处理日志失败:{e}");
		// 	}
		// }
		// catch (Exception e)
		// {
		// 	Console.WriteLine($"解析日志失败:{e}");
		// }
	}
	public delegate void RequestMessageReceivedEventHandler(FaceCapCameraClient sender, string message);
	public event RequestMessageReceivedEventHandler? OnRequestMessageReceived;

	public void Send(string message)
	{
		var buffer = Encoding.UTF8.GetBytes(message);
		_webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
	}
}