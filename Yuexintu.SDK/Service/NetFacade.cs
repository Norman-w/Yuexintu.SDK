using System.ComponentModel;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Yuexintu.SDK.Service.Api;

namespace Yuexintu.SDK.Service;

internal class NetFacade : INetFacade
{
	#region Public

	public NetFacade(int port)
	{
		_port = port;
		_backgroundWorker.DoWork += NetFacadeBackgroundWorker_DoWork;
	}

	public void Start()
	{
		_backgroundWorker.RunWorkerAsync();
	}

	public void Stop()
	{
		if (_backgroundWorker.IsBusy)
		{
			_backgroundWorker.CancelAsync();
		}
		else
		{
			_backgroundWorker.Dispose();
		}
	}

	/// <summary>
	/// 异步会话创建事件,当新的会话创建时触发,并等待事件处理完成
	/// </summary>
	public event WebSocketSessionCreatedEventHandler? WebSocketSessionCreatedAsync;

	#endregion

	#region Private

	private readonly int _port;

	private readonly BackgroundWorker _backgroundWorker = new();

	private void NetFacadeBackgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
	{
		var builder = InitWebApplicationBuilder(_port);
		using var app = InitWebApplication(builder);
		
		app.MapGet("/",
			() =>
				"当前服务为人脸识别SDK的HTTP API服务,请使用HTTP API进行访问,或者使用WebSocket进行连接")
			.RequireCors("AllowAll");
		

		
		ConfigSwagger(app);

		ConfigWebsocket(app);

		#region 启动服务器

		//这里添加正常的其他逻辑.因为本项目是一个控制台应用程序,其他的业务方面的服务应当先启动,然后再启动grpc服务
		app.Run();

		#endregion
	}

	private static WebApplicationBuilder InitWebApplicationBuilder(int port)
	{
		#region 创建和配置Builder

		#region 创建Builder

		//创建WebApplication的Builder,  WebApplication 是 using Microsoft.AspNetCore.Builder;
		var builder = WebApplication.CreateBuilder();

		#region Api controller和swagger

		builder.Services.AddControllers();
		builder.Services.AddControllers()
			.AddApplicationPart(typeof(ApiController).Assembly)
			.AddControllersAsServices();
		builder.Services.AddEndpointsApiExplorer();
		// builder.Services.AddSwaggerGen();
		builder.Services.AddSwaggerGen(
			c =>
			{
				c.SwaggerDoc(
					"v1",
					new OpenApiInfo
					{
						Title = "Yuexintu人脸识别SDK HTTP API",
						Version = "v1"
					}
				);
			}
		);

		#endregion

		#endregion

		#region 添加下面的UseKestrel(微软的一种web服务引擎)需要用到using Microsoft.AspNetCore.Hosting;

		builder.WebHost.UseKestrel(options =>
		{
			options.ListenAnyIP(port,
				listenOptions => { listenOptions.Protocols = HttpProtocols.Http1; });
			// options.ListenAnyIP(App.Setting.GrpcWebPort, listenOptions =>
			// {
			// Grpc Web需要使用Http1
			// listenOptions.Protocols = HttpProtocols.Http1;
			// });
		});

		#endregion

		#region 为grpc web添加跨域支持

		builder.Services.AddCors(o => o.AddPolicy("AllowAll", corsPolicyBuilder =>
		{
			corsPolicyBuilder.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader();
		}));

		#endregion

		#endregion

		return builder;
	}

	private static WebApplication InitWebApplication(WebApplicationBuilder builder)
	{
		WebApplication? app = null;
		try
		{
			#region 使用builder创建和配置app

			app = builder.Build();
			//支持跨域请求
			app.UseCors("AllowAll");

			#endregion

			return app;
		}
		catch
		{
			((IDisposable?)app)?.Dispose();
			throw;
		}
	}

	private static void ConfigSwagger(WebApplication app)
	{
		//TODO 需要将此硬编码改为从设置或环境变量等获取
		//部署在https://norman.wang/face-capture-camera,则需要设置BasePath
		app.UsePathBase("/face-capture-camera");
		#region swagger

		app.UseSwagger(options =>
		{
			options.PreSerializeFilters.Add((swagger, httpReq) =>
			{
				if (!httpReq.Headers.ContainsKey("X-Request-Uri")) return;
				var index = httpReq.Headers["X-Request-Uri"].ToString()
					.IndexOf("/swagger/", StringComparison.Ordinal);
				if (index <= 0) return;
				var serverUrl = $"{httpReq.Headers["X-Request-Uri"].ToString()[..index]}/";
				swagger.Servers = new List<OpenApiServer> { new() { Url = serverUrl } };
			});
		});

		app.UseSwaggerUI();

		app.UseHttpsRedirection();

		app.UseAuthorization();

		app.MapControllers();

		//这种方式比较原始.但是可以用来测试
		// app.MapPost("/api/v1/adapter/lenfocus/face/capture", async context =>
		// {
		// 	await context.Response.WriteAsync($"Hello World! {context.Request.Path}");
		// });

		#endregion
	}

	private void ConfigWebsocket(WebApplication app)
	{
		#region 添加websocket支持

		app.UseWebSockets(
			//支持SubProtocol
			new WebSocketOptions
			{
				KeepAliveInterval = TimeSpan.FromSeconds(120),
			});
		// 添加WebSocket中间件
		app.Map("/face-capture-camera/", async (context) =>
		{
			if (context.WebSockets.IsWebSocketRequest)
			{
				if (context.Request.Path == "/face-capture-camera/")
				{
					// var subProtocol = context.Request.Headers["Sec-WebSocket-Protocol"];
					// if (subProtocol.Count > 0)
					// {
					//     context.WebSockets.WebSocketRequestedProtocols.Add(subProtocol);
					// }
				}

				try
				{
					var webSocket =
						await context.WebSockets.AcceptWebSocketAsync();
					var session = new SessionCreatedEventArgs(Guid.NewGuid().ToString(), webSocket);
					//触发会话创建事件,并等待他的完整生命周期
					if (WebSocketSessionCreatedAsync != null) await WebSocketSessionCreatedAsync(session);
					else Console.WriteLine("未处理的会话创建事件");
				}
				catch (Exception err)
				{
					Console.WriteLine("OnClientConnected异常:" + err.Message);
					// throw;
				}
			}
			else
			{
				//设置字符集,防止乱码
				context.Response.ContentType = "text/plain; charset=utf-8";
				//输出提示,只能使用ws访问
				await context.Response.WriteAsync("只能使用ws访问, Only ws is supported");
				// context.Response.StatusCode = 400;
			}
		});

		#endregion
	}

	#endregion
}

public class SessionCreatedEventArgs : EventArgs
{
	/// <summary>
	/// 当新的连接创建时,触发的事件参数
	/// </summary>
	/// <param name="sessionId"></param>
	/// <param name="connection"></param>
	public SessionCreatedEventArgs(string sessionId, object connection)
	{
		SessionId = sessionId;
		Connection = connection;
		switch (connection)
		{
			case HttpContext httpContext:
				ConnectionType = ConnectionTypeEnum.Http;
				var clientTypeHeader = httpContext.Request.Headers["ClientType"];
				var clientTypeHeaderValues = clientTypeHeader.ToList();
				if (clientTypeHeader.Count != 1)
				{
					throw new Exception("ClientType Header的值不是唯一的");
				}

				var clientTypeHeaderValue = clientTypeHeaderValues[0];
				ClientType = clientTypeHeaderValue == null
					? ClientTypeEnum.Unknown
					: (ClientTypeEnum)int.Parse(clientTypeHeaderValue);
				break;
			// case Grpc.AspNetCore.Server.ServerCallContext _:
			// ConnectionType = ConnectionTypeEnum.Grpc;
			// break;
			case WebSocket webSocket:
				ConnectionType = ConnectionTypeEnum.WebSocket;
				ClientType = webSocket.SubProtocol == null
					? ClientTypeEnum.Unknown
					: (ClientTypeEnum)int.Parse(webSocket.SubProtocol);
				break;
			default:
				ConnectionType = ConnectionTypeEnum.Unknown;
				break;
		}
	}

	/// <summary>
	/// 会话ID,连接的时候生成的guid(或者是其他的唯一标识,不一定是哪一端生成)
	/// </summary>
	public string SessionId { get; }

	/// <summary>
	/// 连接类型枚举
	/// </summary>
	public ConnectionTypeEnum ConnectionType { get; set; }

	/// <summary>
	/// 连接对象
	/// </summary>
	public object Connection { get; set; }

	/// <summary>
	/// 客户端类型
	/// </summary>
	public ClientTypeEnum ClientType { get; set; }

	public override string ToString()
	{
		return $"Id:{SessionId}, 连接类型:{ConnectionType}, 客户端类型:{ClientType}, 连接对象:{Connection}";
	}
}