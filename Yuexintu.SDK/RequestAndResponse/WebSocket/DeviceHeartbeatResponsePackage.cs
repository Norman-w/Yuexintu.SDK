namespace Yuexintu.SDK.RequestAndResponse.WebSocket;

/// <summary>
/// 设备心跳响应载荷
/// </summary>
public class DeviceHeartbeatResponsePackage
{
	/// <summary>
	/// 错误码
	/// </summary>
	public int Code { get; set; }

	/// <summary>
	/// 执行结果描述
	/// </summary>
	public string Msg { get; set; }
}