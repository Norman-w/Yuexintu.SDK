using Newtonsoft.Json;
using System.Collections.Generic;
using Yuexintu.SDK.Service;

namespace Yuexintu.SDK.RequestAndResponse.Http
{
    /// <summary>
    /// 人脸抓拍请求模型
    /// </summary>
    public class FaceCaptureRequest : HttpRequestPackage
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [JsonProperty("did")]
        public string Did { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [JsonProperty("type")]
        public int Type { get; set; }

        /// <summary>
        /// 抓拍时间，UNIX时间戳，单位：秒
        /// </summary>
        [JsonProperty("time")]
        public long Time { get; set; }

        /// <summary>
        /// 设备的SN
        /// </summary>
        [JsonProperty("sn")]
        public string Sn { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [JsonProperty("age")]
        public int Age { get; set; }

        /// <summary>
        /// 性别，1-男，2-女
        /// </summary>
        [JsonProperty("gender")]
        public int Gender { get; set; }

        /// <summary>
        /// 人员ID
        /// </summary>
        [JsonProperty("pid")]
        public string Pid { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [JsonProperty("id_card")]
        public string IdCard { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [JsonProperty("work_id")]
        public string WorkId { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [JsonProperty("department")]
        public string Department { get; set; }

        /// <summary>
        /// 其他信息
        /// </summary>
        [JsonProperty("otherInfo")]
        public string OtherInfo { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        [JsonProperty("category")]
        public int Category { get; set; }

        /// <summary>
        /// 分数(识别后的相似度)
        /// </summary>
        [JsonProperty("score")]
        public int Score { get; set; }

        /// <summary>
        /// 是否戴安全帽
        /// </summary>
        [JsonProperty("helmet")]
        public int Helmet { get; set; }

        /// <summary>
        /// 是否吸烟
        /// </summary>
        [JsonProperty("smoke")]
        public int Smoke { get; set; }

        /// <summary>
        /// 手势
        /// </summary>
        [JsonProperty("handed")]
        public int Handed { get; set; }

        /// <summary>
        /// 特征
        /// </summary>
        [JsonProperty("feature")]
        public List<double> Feature { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        [JsonProperty("data")]
        public FaceDataModel Data { get; set; }

        public class FaceDataModel
        {
            /// <summary>
            /// ID
            /// </summary>
            [JsonProperty("id")]
            public int Id { get; set; }

            /// <summary>
            /// 人脸数据
            /// </summary>
            [JsonProperty("face_data")]
            public string FaceData { get; set; }
            
            /// <summary>
            /// 人包含身体的数据
            /// TODO 测试环境捕捉不到下半身所以暂时还没测试
            /// </summary>
            public string BodyData { get; set; }

            /// <summary>
            /// 人脸图片地址
            /// </summary>
            [JsonProperty("jpeg_url_face")]
            public string JpegUrlFace { get; set; }

            /// <summary>
            /// 背景数据
            /// </summary>
            [JsonProperty("bg_data")]
            public string BgData { get; set; }

            /// <summary>
            /// 全景图地址
            /// </summary>
            [JsonProperty("jpeg_url_frame")]
            public string JpegUrlFrame { get; set; }
        }
    }
}