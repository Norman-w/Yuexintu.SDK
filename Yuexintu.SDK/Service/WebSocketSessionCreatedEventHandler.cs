namespace Yuexintu.SDK.Service;

/// <summary>
/// WebSocket会话创建事件处理器,需要一个异步方法进行处理
/// Session创建后,异步函数应当始终保持连接,直到Session关闭,如果函数返回,Session将被关闭
/// </summary>
public delegate Task WebSocketSessionCreatedEventHandler(SessionCreatedEventArgs session);