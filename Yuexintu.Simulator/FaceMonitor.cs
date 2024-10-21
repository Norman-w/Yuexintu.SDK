using System.ComponentModel;

namespace Yuexintu.Simulator;

/// <summary>
/// 人脸监测器模拟器,当监测到人员后会上报信息
/// 简单的就是直接从给如的人员列表中检测到人员就上报
/// 偶尔也上报一个陌生人脸信息
/// </summary>
public class FaceMonitor
{
	private readonly PersonInformationArchive? _personInformationArchive;
	public FaceMonitor(PersonInformationArchive? personInformationArchive)
	{
		_personInformationArchive = personInformationArchive;
		_worker = new BackgroundWorker();
		_worker.DoWork += Worker_DoWork;
	}
	public class FaceDetectedEventArgs : EventArgs
	{
		public required string Pid { get; set; }
		public DateTime Time { get; set; }
	}
	public class FaceUnknownCapturedEventArgs : EventArgs
	{
		public required byte[] Image { get; set; }
		public DateTime Time { get; set; }
	}

	public delegate void FaceDetectedEventHandler(object sender, FaceDetectedEventArgs e);
	public delegate void FaceUnknownCapturedEventHandler(object sender, FaceUnknownCapturedEventArgs e);
	
	public event FaceDetectedEventHandler? FaceDetected;
	public event FaceUnknownCapturedEventHandler? FaceUnknownCaptured;

	private readonly BackgroundWorker _worker;
	
	public void Start()
	{
		_worker.RunWorkerAsync();
	}
	public void Stop()
	{
		_worker.CancelAsync();
	}

	private void Worker_DoWork(object? sender, DoWorkEventArgs e)
	{
		//检测到的比例是80%, 20%的概率检测到陌生人
		//每隔1~10秒检测到一个人
		var random = new Random();

		while (true)
		{
			var isDetected = random.Next(1, 100) <= 80;
			Thread.Sleep(random.Next(1000, 10000));
			var personDic = _personInformationArchive.PersonInfoList;
			if (isDetected)
			{
				var randomPid = personDic.Keys.ElementAt(random.Next(0, personDic.Count));
				var person = personDic[randomPid];
				OnFaceDetected(new FaceDetectedEventArgs
				{
					Pid = person.Pid,
					Time = DateTime.Now
				});
			}
			else
			{
				OnFaceUnknownCaptured(new FaceUnknownCapturedEventArgs
				{
					Image = Array.Empty<byte>(),
					Time = DateTime.Now
				});
			}
		}
	}

	private void OnFaceUnknownCaptured(FaceUnknownCapturedEventArgs faceUnknownCapturedEventArgs)
	{
		FaceUnknownCaptured?.Invoke(this, faceUnknownCapturedEventArgs);
	}

	private void OnFaceDetected(FaceDetectedEventArgs faceDetectedEventArgs)
	{
		FaceDetected?.Invoke(this, faceDetectedEventArgs);
	}
}