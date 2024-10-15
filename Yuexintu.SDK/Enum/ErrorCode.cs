/*
 
 
 该类使用静态类实现枚举类似功能以承载更丰富的错误码信息

   200：成功
   
   401：没有访问权限，需要进行身份认证
   
   404：访问的路径不存在
   
   420：参数异常
   
   421：设备未注册
   
   422：无效的AppId
   
   423：签名校验错误
   
   424：事件类型异常
   
   425：客户端版本过低
   
   426：请求的资源不存在

*/
namespace Yuexintu.SDK.Enum;

public struct ErrorCode
{
	#region 构造函数

	public ErrorCode(int value, string description)
	{
		Value = value;
		Description = description;
	}

	#endregion

	#region 字段定义

	public int Value;
	public string Description;

	#endregion

	#region 重载运算符
	
	private bool Equals(ErrorCode other)
	{
		return Value == other.Value && Description == other.Description;
	}

	public override bool Equals(object? obj)
	{
		return obj is ErrorCode other && Equals(other);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Value, Description);
	}

	public static bool operator ==(ErrorCode a, ErrorCode b)
	{
		return a.Value == b.Value;
	}

	public static bool operator !=(ErrorCode a, ErrorCode b)
	{
		return !(a == b);
	}

	#endregion

	#region 静态实例/值

	public static readonly ErrorCode Success = new ErrorCode(200, "成功");
	public static readonly ErrorCode Unauthorized = new ErrorCode(401, "没有访问权限，需要进行身份认证");
	public static readonly ErrorCode NotFound = new ErrorCode(404, "访问的路径不存在");
	public static readonly ErrorCode ParameterError = new ErrorCode(420, "参数异常");
	public static readonly ErrorCode DeviceNotRegistered = new ErrorCode(421, "设备未注册");
	public static readonly ErrorCode InvalidAppId = new ErrorCode(422, "无效的AppId");
	public static readonly ErrorCode SignatureError = new ErrorCode(423, "签名校验错误");
	public static readonly ErrorCode EventTypeError = new ErrorCode(424, "事件类型异常");
	public static readonly ErrorCode ClientVersionTooLow = new ErrorCode(425, "客户端版本过低");
	public static readonly ErrorCode ResourceNotFound = new ErrorCode(426, "请求的资源不存在");

	#endregion
}