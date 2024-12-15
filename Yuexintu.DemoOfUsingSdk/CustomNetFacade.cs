using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Yuexintu.SDK.Service;
using Yuexintu.SDK.Service.Api;

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

		#region 已存在的Http服务器对象

		var builder = WebApplication.CreateBuilder();
		builder.WebHost.UseUrls("http://*:51648");
		builder.Services.AddControllers();
		builder.Services.AddControllers()
			.AddApplicationPart(typeof(ApiController).Assembly)
			.AddControllersAsServices();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo { Title = "Yuexintu人脸识别SDK HTTP API", Version = "v1" });
		});
		builder.Services.AddCors(options =>
		{
			options.AddPolicy("AllowAll", corsPolicyBuilder =>
			{
				corsPolicyBuilder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader();
			});
		});
		builder.WebHost.UseKestrel(options =>
		{
			options.ListenAnyIP(51648, listenOptions => { listenOptions.Protocols = HttpProtocols.Http1; });
		});

		var app = builder.Build();
		app.UseSwagger();
		app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Yuexintu人脸识别SDK HTTP API v1"));
		app.UseCors("AllowAll");
		app.UseHttpsRedirection();
		app.UseAuthorization();
		app.MapControllers();

		app.MapGet("/", () => "这是Yuexintu人脸识别SDK的HTTP API服务");
		app.Run();

		#endregion
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