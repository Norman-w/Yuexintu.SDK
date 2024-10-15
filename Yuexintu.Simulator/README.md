# 栎芯图人脸识别设备模拟器
模拟人脸识别摄像头的基本行为,包括

## 正常启动和工作
* 上电启动后,通过预先配置好的WebSocket连接服务端,设备作为客户端.连接时调用`"uri"："/connect"`接口,并发送设备的基本信息,如设备DID,设备序列号,IP地址等
* 连接成功后,设备会通过WebSocket定时向服务端发送token更新消息,以保证旧token及时失效,新token及时生效,使得服务端使用该token向设备发送消息时,能够被设备正确解析 
* 连接成功后,设备会通过WebSocket定时向服务端发送心跳消息,以保持连接的活跃状态
* 摄像头发现新人脸后,通过WebSocket下发(通知服务器)人员信息及照片(尚不确定)

## 故障模拟

## 查询

* 服务器可通过WebSocket发送消息,查询抓拍图像的文件列表(不清楚和下面一条的区别)
* 服务器可通过WebSocket发送消息,查询行人抓拍图像的文件列表(不清楚和上面一条
* 服务器可通过WebSocket发送消息,按月获取每天的视频文件数量
* 服务器可通过WebSocket发送消息,获取摄像头捕捉到的视频文件信息(通常在检测到人脸时生成视频记录)
* 服务器可通过WebSocket发送消息,查询人员列表
* 服务器可通过WebSocket发送消息,查询人员信息
* 服务器可通过WebSocket发送消息,以图搜图,根据一张图片查询抓拍到的这个人的所有瞬间信息列表(图像路径,相似度分,时间等)

## 更新
* 服务器可通过WebSocket发送信息,删除(摄像头上的)人员信息

## 设置模拟
* 服务器可通过WebSocket发送消息,控制人脸参数
* 服务器可通过WebSocket发送消息,控制录像参数
* 服务器可通过WebSocket发送消息,控制摄像头重启的区别)