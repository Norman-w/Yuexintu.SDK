using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yuexintu.SDK.Enum;
using Yuexintu.SDK.RequestAndResponse.Http;

namespace Yuexintu.SDK.Service.Api;

[ApiController]
[Route("/[controller]")]
public class ApiController : ControllerBase
{
	/// <summary>
	/// ✅ 已调通正常可使用接口.人脸出现在摄像机时就会调用此接口
	/// 摄像机-->服务端
	/// 当摄像机捕捉到人脸时,调用此接口上报人脸信息(可能同一个人多次上报)
	/// TODO 不确定是不是当服务器宕机以后摄像机重新连接时是否会使用该接口重复上报
	/// </summary>
	/// <returns></returns>
	[HttpPost("v1/adapter/lenfocus/face/capture")]
	public IActionResult UploadFaceCaptureImage()
	{
		var body = Request.Body;
		var reader = new StreamReader(body);
		var json = reader.ReadToEndAsync().Result;
		var request = JsonConvert.DeserializeObject<FaceCaptureRequest>(json);
		if (request == null)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("接口: /v1/adapter/lenfocus/face/capture, 请求数据为空");
			Console.ResetColor();
			return BadRequest();
		}
		Server.Instance?.ReportHttpRequestReceived(request);

		Console.ForegroundColor = ConsoleColor.Cyan;
		//这个应该是之前的错误的文档,后面实际接收到的请求中,有Pid没有Uuid
		// Console.WriteLine($"摄像机抓拍到人脸信息, Did: {request.Did}, Pid(UUID): {request.Uuid}, Name: {request.Name}");
		Console.WriteLine($"摄像机抓拍到人脸信息, Did: {request.Did}, Pid: {request.Pid}, Name: {request.Name}");
		Console.ResetColor();

		var response = new FaceCaptureResponse
		{
			Code = ErrorCode.Success.Value,
			Msg = "Ok"
		};

		Console.ForegroundColor = ConsoleColor.Magenta;
		Console.WriteLine($"返回给摄像机的消息: {response.Code} - {response.Msg}");
		Console.ResetColor();


		#region 传递给回调函数
		
		var pid = request.Pid;
		var name = request.Name;
		var faceDateBase64 = request.Data.FaceData;
		var sn = request.Sn;

		Server.Instance?.ReportFaceCaptured(sn, pid, name, faceDateBase64);
				

		#endregion

		return Ok(response);
	}

	/// <summary>
	/// 上报设备信息(设备上电时调用)
	/// </summary>
	/// <param name="request">请求参数</param>
	/// <returns>响应结果</returns>
	[HttpPost("v1/device/report/info")]
	public IActionResult DeviceReportInfo([FromBody] DeviceReportInfoRequest request)
	{
		var response = new DeviceReportInfoResponse
		{
			Code = ErrorCode.Success.Value,
			Msg = "Ok"
		};
		Server.Instance?.ReportHttpRequestReceived(request);
		return Ok(response);
	}

	/// <summary>
	/// 查询在线升级版本信息
	/// </summary>
	/// <param name="request">请求参数</param>
	/// <returns>响应结果</returns>
	[HttpPost("v1/ota/query")]
	public IActionResult QueryOtaVersion([FromBody] QueryOtaVersionRequest request)
	{
		var response = new QueryOtaVersionResponse
		{
			Code = ErrorCode.Success.Value,
			Msg = "Ok",
			Data = new QueryOtaVersionResponse.QueryOtaVersionData
			{
				VerCode = 453523,
				VerName = "v1.0",
				Description = "更新了一些问题",
				ReleaseDate = "2021-01-01 08:00:00",
				ComponentList = new List<QueryOtaVersionResponse.QueryOtaVersionData.ComponentModel>
				{
					new()
					{
						Name = "Firmware",
						Type = 1,
						Size = 1024,
						Md5 = "ee1101953363bf8a57334d60251ca53e",
						Sha256 = "5e497a333048ae62e5701ff667edd6ba7...",
						Url = "https://mi-test530.lenfocus.com/.../09b5c0e.bin"
					}
				}
			}
		};
		Server.Instance?.ReportHttpRequestReceived(request);
		return Ok(response);
	}

	/// <summary>
	/// ⏳ 已经可以正常接受到消息,但待确认是否因为没有通过人脸获取到pid导致,另外告警消息的结构和现有定义是否一致尚未确认
	/// 上报告警信息
	/// </summary>
	/// <returns>响应结果</returns>
	[HttpPost("v1/device/report/event")]
	public IActionResult ReportEvent()
	{
		//从请求中读取数据
		var body = Request.Body;
		var reader = new StreamReader(body);
		var json = reader.ReadToEndAsync().Result;
		var request = JsonConvert.DeserializeObject<EventReportRequest>(json);
		if (request == null)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("接口: /v1/device/report/event, 请求数据为空");
			Console.ResetColor();
			return BadRequest();
		}
		Server.Instance?.ReportHttpRequestReceived(request);
		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.WriteLine($"收到告警信息: Type: {request.Type}, 正文: {json}");
		Console.ResetColor();
		var response = new EventReportResponse
		{
			Code = ErrorCode.Success.Value,
			Msg = "Ok"
		};
		return Ok(response);
	}

	/// <summary>
	/// 上报车辆抓拍信息
	/// </summary>
	/// <param name="request">请求参数</param>
	/// <returns>响应结果</returns>
	[HttpPost("v1/vehicle/capture")]
	public IActionResult ReportVehicleCapture([FromBody] VehicleCaptureRequest request)
	{
		var response = new VehicleCaptureResponse
		{
			Code = ErrorCode.Success.Value,
			Msg = "Ok"
		};
		Server.Instance?.ReportHttpRequestReceived(request);
		return Ok(response);
	}

	/// <summary>
	/// ✅ 已调通正常可使用接口.摄像机检测到人脸时除了调用上报人脸信息(图片)的接口外还会调用此接口,可简单确认人脸是否在库中
	/// 有人员访问时上报访问记录
	/// </summary>
	/// <returns></returns>
	[HttpPut("v1/access/record")]
	public async Task<IActionResult> AccessRecord()
	{
		AccessRecordRequest? request;
		//从put请求中读取数据
		using (var reader = new StreamReader(Request.Body))
		{
			// var json = reader.ReadToEnd();
			// Synchronous operations are disallowed. Call ReadAsync or set AllowSynchronousIO to true instead.
			// request = JsonConvert.DeserializeObject<AccessRecordRequest>(json);
			var json = await reader.ReadToEndAsync();
			try
			{
				request = JsonConvert.DeserializeObject<AccessRecordRequest>(json);
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("在接口: /v1/access/record, 反序列化请求数据时出错");
				Console.WriteLine(e);
				Console.ResetColor();
				return BadRequest();
			}
		}

		if (request == null)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("接口: /v1/access/record, 请求数据为空");
			Console.ResetColor();
			return BadRequest();
		}
		
		Server.Instance?.ReportHttpRequestReceived(request);

		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.WriteLine($"收到访问记录: SN: {request.Sn},共计{request.Data.Count}条记录");
		Console.ResetColor();
		foreach (var json in request.Data.Select(JsonConvert.SerializeObject))
		{
			Console.WriteLine(json);
		}

		var response = new AccessRecordResponse
		{
			Code = ErrorCode.Success.Value,
			Msg = "Ok"
		};
		Console.ForegroundColor = ConsoleColor.Magenta;
		Console.WriteLine($"返回给摄像机的消息: {response.Code} - {response.Msg}");
		Console.ResetColor();
		return Ok(response);
	}
	
	/// <summary>
	/// ✅ 已调通正常可使用接口.摄像机抓拍到人脸后会调用该接口上传图片内容,body可以直接作为前端img标签的src使用.
	/// </summary>
	/// <returns></returns>
	[HttpPost("v1/access/upload/base64img")]
	public IActionResult AccessUploadBase64Image()
	{
		//从请求中读取数据
		var body = Request.Body;
		var reader = new StreamReader(body);
		var dataImageBase64 = reader.ReadToEndAsync().Result;

		if (Debugger.IsAttached)
		{
			//用户文件夹下创建AccessUploadBase64Image文件夹
			var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AccessUploadBase64Image");
			if (!Directory.Exists(folder))
			{
				Directory.CreateDirectory(folder);
			}
			//保存图片
			var fileName = Path.Combine(folder, $"{DateTime.Now:yyyy-MM-dd HH-mm-ss}.jpg");
			//格式是data:image/jpeg;base64,开头,所以要规范化保存到Image
			var imageBase64 = dataImageBase64.Replace("data:image/jpeg;base64,", "");
			var imageBytes = Convert.FromBase64String(imageBase64);
			System.IO.File.WriteAllBytes(fileName, imageBytes);
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine($"保存图片到: {fileName}");
			Console.ResetColor();
		}
		//这里调用不了这个方法 因为发过来的就不是request 直接就是base64字符串,所以无法解析到request
		// Server.Instance?.ReportHttpRequestReceived(request);
		
		var response = new Base64ImageResponse();
		return Ok(response);
	}
	
	/// <summary>
	/// 获取服务器时间
	/// </summary>
	/// <returns></returns>
	[HttpGet("v1/server/time")]
	public IActionResult GetServerTime()
	{
		var response = new ServerTimeResponse
		{
			Code = ErrorCode.Success.Value,
			Msg = "Ok",
			Data = new ServerTimeResponse.ServerTimeData
			{
				Time = DateTime.Now
			}
		};
		return Ok(response);
	}
}

public class Base64ImageResponse
{
}