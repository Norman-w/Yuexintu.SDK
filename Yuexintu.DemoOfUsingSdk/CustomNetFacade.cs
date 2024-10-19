using Yuexintu.SDK.Service;

namespace Yuexintu.DemoOfUsingSdk;

/// <summary>
/// 自定义的网络请求接收器/网络服务器, 实现INetFacade即可
/// INetFacade的WebSocketSessionCreatedAsync事件在自有服务器接受到WebSocket连接请求时触发即可
/// Sdk的Server中已经关注了这个事件,当事件触发时,会处理整个连接过程以及消息处理
/// </summary>
public class CustomNetFacade : INetFacade
{
	public void Start()
	{
		//假设你有一个开关控制要不要发送事件
	}

	public void Stop()
	{
		//假设你有一个开关控制要不要发送事件
	}

	/// <summary>
	/// 这个事件要在WebSocket连接请求到来时触发
	/// </summary>
	public event WebSocketSessionCreatedEventHandler? WebSocketSessionCreatedAsync;
}