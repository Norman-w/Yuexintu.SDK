using System.Net;
using System.Text;

namespace Yuexintu.Simulator;

public static class Utils
{
	#region 生成随机SN

	public static string GenerateRandomSn()
	{
		//A~Z的2位加上10位数字以及a~z的2位
		return $"{GetRandomString(2, RandomCharType.UpperLetter)}{GetRandomString(10, RandomCharType.Number)}{GetRandomString(2, RandomCharType.LowerLetter)}";
	}
	
	private static string GetRandomString(int length, RandomCharType type)
	{
		var sb = new StringBuilder();
		var random = new Random();
		for (var i = 0; i < length; i++)
		{
			switch (type)
			{
				case RandomCharType.Number:
					sb.Append(random.Next(0, 10));
					break;
				case RandomCharType.UpperLetter:
					sb.Append((char)random.Next(65, 91));
					break;
				case RandomCharType.LowerLetter:
					sb.Append((char)random.Next(97, 123));
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, null);
			}
		}
		return sb.ToString();
	}
	
	private enum RandomCharType
	{
		Number,
		UpperLetter,
		LowerLetter
	}

	#endregion

	#region 生成随机人名

	public static string GenerateRandomPersonName()
	{
		var familyNames = new[]
		{
			"赵", "钱", "孙", "李", "周", "吴", "郑", "王", "冯", "陈", "褚", "卫", "蒋", "沈", "韩", "杨", "朱", "秦", "尤", "许",
			"何", "吕", "施", "张", "孔", "曹", "严", "华", "金", "魏", "陶", "姜", "戚", "谢", "邹", "喻", "柏", "水", "窦", "章",
			"云", "苏", "潘", "葛", "奚", "范", "彭", "郎", "鲁", "韦", "昌", "马", "苗", "凤", "花", "方", "俞", "任", "袁", "柳",
			"酆", "鲍", "史", "唐", "费", "廉", "岑", "薛", "雷", "贺", "倪", "汤", "滕", "殷", "罗", "毕", "郝", "邬", "安", "常",
			"乐", "于", "时", "傅", "皮", "卞", "齐", "康", "伍", "余", "元", "卜", "顾", "孟", "平", "黄", "和", "穆", "萧", "尹",
			"欧阳", "慕容"
		};
		var givenNames = new[]
		{
			"子璇", "淼", "国栋", "夫子", "瑞堂", "甜", "敏", "尚", "国贤", "贺祥", "天佑", "文博", "文斌", "昊轩", "易轩", "博涛", "瑾春",
			"嘉熙", "子辰", "泽宇", "鑫磊", "明轩", "越", "博", "瑾春", "子辰", "泽宇", "鑫磊", "明轩", "越", "博", "昊焱", "致远",
			"俊驰", "雨泽", "烨磊", "晟睿", "文昊", "修洁", "黎昕", "远航", "旭尧", "鸿涛", "伟祺", "荣轩", "越彬", "浩宇", "瑾瑜",
			"明", "健", "耀", "煜", "烨", "熠", "潇", "绍", "泽", "涵", "亦", "弘", "博", "烨", "霖", "潇", "熠", "烨", "煜", "煊",
			"甜甜", "娜娜", "婷婷", "倩倩", "媛媛", "婷", "露", "甜", "美", "妍", "婷", "婷", "玉", "凤", "萍", "玲", "丽", "燕", "萍",
		};
		var random = new Random();
		return $"{familyNames[random.Next(0, familyNames.Length)]}{givenNames[random.Next(0, givenNames.Length)]}";
	}

	#endregion

	public static string GetLocalIp()
	{
		var host = Dns.GetHostEntry(Dns.GetHostName());
		foreach (var ip in host.AddressList)
		{
			if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
			{
				return ip.ToString();
			}
		}
		throw new Exception("No network adapters with an IPv4 address in the system!");
	}

	public static string GenerateRandomFaceData()
	{
		return Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
	}

	public static string GenerateRandomBodyData()
	{
		return Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
	}

	public static string GenerateRandomBgData()
	{
		return Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
	}
}