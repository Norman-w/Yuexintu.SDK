using Yuexintu.SDK.Event;
using Yuexintu.SDK.RequestAndResponse.WebSocket;

namespace Yuexintu.SDK.Service;

public class Server
{
	private readonly NetMessageProcessor _messageProcessor = new();
	private readonly INetFacade _netFacade;
	internal static Server? Instance;

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
		//监听到设备连接请求时,触发事件
		_messageProcessor.OnWebSocketRequestPackageReceived += package =>
		{
			if (package is DeviceConnectionRequestPackage requestPackage)
			{
				OnDeviceConnected?.Invoke(this, new DeviceConnectedEventArgs
				{
					Sn = requestPackage.Data.Param.Sn
				});
			}
			//返回null则不向客户端发送消息,也就是说该回调函数内只是旁路监听消息.
			return null;
		};
		
		Instance = this;
	}

	public void Start()
	{
		_netFacade.Start();
	}

	public void Stop()
	{
		_netFacade.Stop();
	}

	/// <summary>
	/// 当WebSocket会话被创建时触发,通常你不需要订阅这个事件
	/// 在业务层,你只需要订阅OnDeviceConnected和OnFaceCaptured事件即可基本完成业务逻辑
	/// </summary>
	private event WebSocketSessionCreatedEventHandler? WebSocketSessionCreatedAsync
	{
		add => _netFacade.WebSocketSessionCreatedAsync += value;
		remove => _netFacade.WebSocketSessionCreatedAsync -= value;
	}

	/// <summary>
	/// 当WebSocket请求包被接收到时触发,在这里你可以处理全部请求包,属于数据层的事件
	/// 如果你只关心摄像机连接和捕捉到人脸的事件,进需要订阅OnDeviceConnected和OnFaceCaptured事件
	/// </summary>
	public event NetMessageProcessor.WebSocketRequestPackageReceivedEventHandler? OnWebSocketRequestPackageReceived
	{
		add => _messageProcessor.OnWebSocketRequestPackageReceived += value;
		remove => _messageProcessor.OnWebSocketRequestPackageReceived -= value;
	}

	/// <summary>
	/// 当HTTP请求被接收到时触发,在这里你可以处理全部请求包,属于数据层的事件
	/// 如果你只关心摄像机连接和捕捉到人脸的事件,进需要订阅OnDeviceConnected和OnFaceCaptured事件
	/// </summary>
	public event HttpRequestReceivedEventHandler? OnHttpRequestReceived;

	/// <summary>
	/// 当设备连接到服务端时触发,是从摄像机通过WebSocket传送过来的
	/// </summary>
	public event DeviceConnectedEventHandler? OnDeviceConnected;
	/// <summary>
	/// 当有人脸被捕捉到时触发(是通过http从摄像机上传过来的)
	/// </summary>
	public event FaceCapturedEventHandler? OnFaceCaptured;
	
	/// <summary>
	/// 所有的http接收到的请求都会调用这个方法,然后触发事件,让Server类对象的用户可以订阅这个事件
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	internal void ReportHttpRequestReceived(HttpRequestPackage request)
	{
		var args = new HttpRequestReceivedEventArgs
		{
			Request = request,
			Sn = request.Sn,
		};
		OnHttpRequestReceived?.Invoke(this, args);
	}
	
	/// <summary>
	/// 内部通过ApiController中调用这个方法,触发事件,让Server类对象的用户可以订阅这个事件
	/// </summary>
	/// <param name="sn"></param>
	/// <param name="pid"></param>
	/// <param name="name"></param>
	/// <param name="faceData"></param>
	internal void ReportFaceCaptured(string sn, string pid, string name, string faceData)
	{
		OnFaceCaptured?.Invoke(this, new FaceCapturedEventArgs
		{
			// Sn = request.
			//Image是byte[]类型,而request.Data.FaceData,是string类型,需要转换
			// Image = Convert.FromBase64String(faceData),
			FaceImageBase64String = faceData,
			KnownPersonId = string.IsNullOrEmpty(pid) ? null : pid,
		});
	}
}