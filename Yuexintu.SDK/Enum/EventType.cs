namespace Yuexintu.SDK.Enum;

/// <summary>
/// 事件类型枚举
/// </summary>
public enum EventType
{
	/// <summary>
	/// 移动侦测报警
	/// </summary>
	MotionDetection = 1,
	/// <summary>
	/// 区域入侵报警
	/// </summary>
	AreaIntrusion = 2,
	/// <summary>
	/// 视频遮挡报警
	/// </summary>
	VideoOcclusion = 3,
	/// <summary>
	/// 婴儿啼哭报警
	/// </summary>
	BabyCrying = 4,
	/// <summary>
	/// 人员聚集检测报警
	/// </summary>
	CrowdDetection = 5,
	/// <summary>
	/// 人员徘徊检测报警
	/// </summary>
	LoiteringDetection = 6,
	/// <summary>
	/// 快速移动报警
	/// </summary>
	FastMovement = 7,
	/// <summary>
	/// 火焰报警
	/// </summary>
	FireAlarm = 8,
	/// <summary>
	/// 音量陡升报警
	/// </summary>
	SuddenVolumeIncrease = 9,
	/// <summary>
	/// 环境噪声陡降报警
	/// </summary>
	SuddenNoiseDecrease = 10,
	/// <summary>
	/// 人形检测实时抓拍
	/// </summary>
	RealTimeCapture = 11,
	/// <summary>
	/// 关注人员(黑白名单)检测
	/// </summary>
	BlackWhiteListDetection = 12,
	/// <summary>
	/// 离岗检测报警
	/// </summary>
	AbsenceDetection = 13,
	/// <summary>
	/// 体温检测报警
	/// </summary>
	TemperatureDetection = 14
}