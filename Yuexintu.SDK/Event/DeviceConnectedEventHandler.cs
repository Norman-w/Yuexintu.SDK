namespace Yuexintu.SDK.Event;
/// <summary>
/// 设备连接到服务端的事件参数,业务层可订阅该事件获取设备序列号,只使用部分属性,实际使用时可根据需求添加属性(从SDK层传递过来)
/// </summary>
public class DeviceConnectedEventArgs : EventArgs
{
	/// <summary>
	/// 设备序列号
	/// </summary>
	public string Sn { get; set; }
}

/// <summary>
/// 捕捉到人脸事件参数,业务层可订阅该事件获取人脸图片,只使用部分属性,实际使用时可根据需求添加属性(从SDK层传递过来)
/// </summary>
public class FaceCapturedEventArgs : EventArgs
{
	/// <summary>
	/// 设备序列号
	/// </summary>
	public string Sn { get; set; }
	/// <summary>
	/// 人脸图片
	/// </summary>
	public string FaceImageBase64String { get; set; }
	/// <summary>
	/// 已知人员ID
	/// </summary>
	public string? KnownPersonId { get; set; }
	/// <summary>
	/// 该人员是否已知
	/// </summary>
	public bool IsKnownPerson => !string.IsNullOrEmpty(KnownPersonId);
}
