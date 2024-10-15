/*
 
 
 
 摄像机模拟器
 单独的一个摄像机模拟器.
 如果模拟摄像机群,定时重启宕机摄像机,则需要批量实例和 TODO 尚未开发的测试群控器



*/

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Hello World!");
Console.ResetColor();
Console.WriteLine("摄像机硬件启动中,加载硬件信息中...");
//随机生成硬件信息(SN码,型号,版本号等)
Console.WriteLine("摄像机系统启动中...");
//随机读取预先准备好的多个系统镜像,包括 HTTP,WebSocket服务器地址信息,心跳间隔,Token有效时间等设置.
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("摄像机系统启动完成");
//进入loop
Console.ResetColor();
//通过WebSocket连接服务器
Console.WriteLine("摄像机连接WebSocket服务器中...");
//输出连接信息
//连接
//输出连接结果
//随机时间产生人脸识别结果,并发送给服务器,保存结果
		//随机发送失败但保存成功
		//按设置时间定期加载失败结果,重新发送
//按间隔时间设置心跳
//按间隔时间自动更新Token
//随机时间宕机,重启,或根据args判断是否永久运行(不主动宕机,运行到启动处断电)
//随时接受新的服务端推送的配置信息,并应用
