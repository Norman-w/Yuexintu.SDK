namespace Yuexintu.SDK.Service;

public class Server
{
	private readonly NetMessageProcessor _messageProcessor = new();
	private readonly INetFacade _netFacade;

	public Server(INetFacade existingNetFacade)
	{
		_netFacade = existingNetFacade;
		Init();
	}

	public Server(int port)
	{
		_netFacade = new NetFacade(port);
		Init();
	}

	private void Init()
	{
		_netFacade.WebSocketSessionCreatedAsync += async session =>
		{
			var client = FaceCapCamaraClient.FromSession(session);
			client.OnRequestMessageReceived += (sender, message) =>
			{
				_messageProcessor.ProcessMessage(message,
					response =>
					{
						if (string.IsNullOrEmpty(response))
						{
							return;
						}

						sender.Send(response);
					}
				);
			};
			await client.StartWorking();
		};
	}

	public void Start()
	{
		_netFacade.Start();
	}

	public void Stop()
	{
		_netFacade.Stop();
	}

	private event WebSocketSessionCreatedEventHandler? WebSocketSessionCreatedAsync
	{
		add => _netFacade.WebSocketSessionCreatedAsync += value;
		remove => _netFacade.WebSocketSessionCreatedAsync -= value;
	}

	public event NetMessageProcessor.WebSocketRequestPackageReceivedEventHandler? OnWebSocketRequestPackageReceived
	{
		add => _messageProcessor.OnWebSocketRequestPackageReceived += value;
		remove => _messageProcessor.OnWebSocketRequestPackageReceived -= value;
	}
}