using Microsoft.AspNetCore.Mvc;
using Yuexintu.SDK.Enum;
using Yuexintu.SDK.RequestAndResponse.Http;

namespace Yuexintu.SDK.Service.Api;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{
	/*
	 
	 
	 摄像机到服务器
	 2.1 上传人脸抓拍图像
	   
	   POST /api/v1/adapter/lenfocus/face/capture
	   参数
	   {
	   "did"："1234512",
	   "time"： 1666781577,
	   "age"： 95, /*比对机专用* /
	   "gender"： 1, /*比对机专用* /
	   "uuid"： “1234566”, /*比对机专用* /
	   "name"： “1234566”, /*比对机专用* /
	   "data"：{
	   "face_data"："data:image/jpeg;base64,UklGRuAb…AABXRAAA==",
	   "body_data"："data:image/jpeg;base64,UklGRuAb…AABXRAAA==",
	   "bg_data"："data:image/jpeg;base64,UklGRuAb…AABXRAAA=="
	   }
	   }
	   参数说明：
	   参数
	   类型
	   说明
	   是否必须
	   did
	   字符串
	   设备id
	   是
	   time
	   整数
	   抓拍时间，UNIX时间戳，单位：秒
	   是
	   name
	   字符串
	   姓名
	   比对机
	   age
	   整数
	   年龄
	   比对机
	   gender
	   整数
	   1-男，2-女
	   比对机
	   uuid
	   字符串
	   人脸id
	   比对机
	   data
	   对象
	    
	   否
	    
	   face_data
	   字符串
	   人脸图,base64编码
	   否
	    
	   body_data
	   字符串
	   全身照,base64编码
	   否
	    
	   bg_data
	   字符串
	   全景图,base64编码
	   否
	    
	    
	    
	   响应
	   {
	   "code":  错误码，
	   "msg": "返回消息"
	   }
	   参数说明：
	   参数
	   类型
	   说明
	   是否必须
	   code
	   整数
	   错误码，详见错误码定义。
	   是
	   msg
	   字符串
	   执行结果描述。
	   否
	   
    
    
    */
	/// <summary>
	/// 摄像机到服务器,上传人脸抓拍图像
	/// </summary>
	/// <returns></returns>
	[HttpPost("/api/v1/adapter/lenfocus/face/capture")]
	public IActionResult UploadFaceCaptureImage(FaceCaptureRequest request)
	{
		var response = new FaceCaptureResponse();
		Console.WriteLine($"摄像机抓拍到人脸信息, Did: {request.Did}, Pid(UUID): {request.Uuid}, Name: {request.Name}");
		response.Code = ErrorCode.Success.Value;
		response.Msg = "Ok";
		return Ok(response);
	}
}