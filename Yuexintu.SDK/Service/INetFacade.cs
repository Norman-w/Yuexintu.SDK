namespace Yuexintu.SDK.Service;

public interface INetFacade
{
	void Start();
	void Stop();

	/// <summary>
	/// 异步会话创建事件,当新的会话创建时触发,并等待事件处理完成
	/// </summary>
	event WebSocketSessionCreatedEventHandler? WebSocketSessionCreatedAsync;
}