/*



 人员信息档案册
 注意::: 一个人可以有多个人脸数据,比如说 化妆的和不化妆的,所以这里的人脸数据是一个数组



*/


using Newtonsoft.Json;
using Yuexintu.SDK.Model;

namespace Yuexintu.Simulator;

public class PersonInformationArchive
{
	public PersonInformationArchive()
	{
		LoadPersonInfoFromFile();
		if (PersonInfoList == null || PersonInfoList.Count == 0)
		{
			PersonInfoList = new Dictionary<string, PersonInfo>();
			Console.ForegroundColor = ConsoleColor.Yellow;
			var minCount = 10;
			var maxCount = 20;
			Console.WriteLine($"人员信息档案册为空,正在生成随机人员信息...数量:{minCount}~{maxCount}");
			MockupPersonInfo(minCount, maxCount);
			Console.WriteLine($"生成随机人员信息完成,数量:{PersonInfoList.Count}");
			Console.ResetColor();
		}
		Console.WriteLine($"人员信息档案册加载完成,数量:{PersonInfoList.Count}");
	}
	public Dictionary<string, PersonInfo> PersonInfoList { get; set; }

	/// <summary>
	/// 外貌信息
	/// </summary>
	public class AppearanceInformation
	{
		/// <summary>
		/// 人脸图
		/// </summary>
		public string? FaceData { get; set; }

		/// <summary>
		/// 全身图
		/// </summary>
		public string? BodyData { get; set; }

		/// <summary>
		/// 全景图
		/// </summary>
		public string? BgData { get; set; }
	}

	public class PersonInfo : Person
	{
		/// <summary>
		/// 这个人的所有外貌信息(可能有多组)
		/// </summary>
		public List<AppearanceInformation> AppearanceInformationList { get; set; } = new();
	}

	#region 存入档案册(新增人员)

	public void AddPerson(Person person)
	{
		PersonInfoList[person.Pid] = new PersonInfo
		{
			Pid = person.Pid,
			Name = person.Name,
			Age = person.Age, Category = person.Category, Department = person.Department, Gender = person.Gender,
			Phone = person.Phone, Photo = person.Photo, WorkId = person.WorkId, IcCardNo = person.WorkId,
			IdCardNo = person.IcCardNo
		};
		//默认刚添加的人.人脸档案是空的
	}

	#endregion

	#region 修改档案册中的人员信息,增加他的人脸信息
	
	public AppearanceInformation? AddAppearanceInformationToPerson(string pid, string faceData, string bodyData, string bgData)
	{
		if (!PersonInfoList.ContainsKey(pid)) return null;
		var appearanceInformation = new AppearanceInformation
		{
			FaceData = faceData,
			BodyData = bodyData,
			BgData = bgData
		};
		PersonInfoList[pid].AppearanceInformationList.Add(appearanceInformation);
		return appearanceInformation;
	}

	public AppearanceInformation? AddAppearanceInformationToPerson(string pid, AppearanceInformation appearanceInformation)
	{
		if (!PersonInfoList.ContainsKey(pid)) return null;
		PersonInfoList[pid].AppearanceInformationList.Add(appearanceInformation);
		return appearanceInformation;
	}

	#endregion

	#region 删除人员

	public void DeletePerson(string pid)
	{
		PersonInfoList.Remove(pid);
	}

	#endregion

	#region 保存文件到本地存储

	public void SavePersonInfoToFile()
	{
		var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PersonInfoArchive.json");
		var json = JsonConvert.SerializeObject(PersonInfoList);
		File.WriteAllText(filePath, json);
	}

	#endregion

	#region 从本地存储读取文件

	public void LoadPersonInfoFromFile()
	{
		var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PersonInfoArchive.json");
		if (!File.Exists(filePath)) return;
		var json = File.ReadAllText(filePath);
		PersonInfoList = JsonConvert.DeserializeObject<Dictionary<string, PersonInfo>>(json) ?? throw new
			InvalidOperationException();
	}

	#endregion

	#region Mockup, 生成随机人员并保存文件到本地存储

	public void MockupPersonInfo(int minCount, int maxCount)
	{
		var count = new Random().Next(minCount, maxCount);
		for (var i = 0; i < count; i++)
		{
			var personName = Utils.GenerateRandomPersonName();
			var person = new Person
			{
				Pid = Guid.NewGuid().ToString(),
				Name = personName,
				Age = (uint)new Random().Next(18, 60),
			};
			AddPerson(person);
			//随机添加1~3个人脸信息
			var faceCount = new Random().Next(1, 4);
			for (var j = 0; j < faceCount; j++)
			{
				var faceData = Utils.GenerateRandomFaceData();
				var bodyData = Utils.GenerateRandomBodyData();
				var bgData = Utils.GenerateRandomBgData();
				AddAppearanceInformationToPerson(person.Pid, faceData, bodyData, bgData);
			}
		}

		SavePersonInfoToFile();
	}

	#endregion
}