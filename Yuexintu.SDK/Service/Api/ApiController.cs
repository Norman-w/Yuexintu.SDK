using Microsoft.AspNetCore.Mvc;
using Yuexintu.SDK.Enum;
using Yuexintu.SDK.RequestAndResponse.Http;

namespace Yuexintu.SDK.Service.Api;

[ApiController]
[Route("api/[controller]")]
public class ApiController : ControllerBase
{
    /// <summary>
    /// 上传人脸抓拍图像
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <returns>响应结果</returns>
    [HttpPost("v1/adapter/lenfocus/face/capture")]
    public IActionResult UploadFaceCaptureImage([FromBody] FaceCaptureRequest request)
    {
        var response = new FaceCaptureResponse
        {
            Code = ErrorCode.Success.Value,
            Msg = "Ok"
        };
        Console.WriteLine($"摄像机抓拍到人脸信息, Did: {request.Did}, Pid(UUID): {request.Uuid}, Name: {request.Name}");
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
        return Ok(response);
    }

    /// <summary>
    /// 上报告警信息
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <returns>响应结果</returns>
    [HttpPost("v1/device/report/event")]
    public IActionResult ReportEvent([FromBody] EventReportRequest request)
    {
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
        return Ok(response);
    }
}