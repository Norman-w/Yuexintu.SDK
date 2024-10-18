using Newtonsoft.Json;

public class WebSocketRequestPackageSimple
{
    public WebSocketRequestPackageSimple()
    {
        //支持反序列化
    }
    public WebSocketRequestPackageSimple(string message)
    {
        var webSocketRequestPayload = JsonConvert.DeserializeObject<WebSocketRequestPackageSimple>(message);
        if (webSocketRequestPayload == null)
        {
            throw new Exception($"{nameof(WebSocketRequestPackageSimple)}无法解析的消息: {message}");
        }
        MsgId = webSocketRequestPayload.MsgId;
        Data = webSocketRequestPayload.Data;
    }
    [JsonProperty("msgid")]
    public string MsgId { get; set; }

    [JsonProperty("data")]
    public BaseDataModel Data { get; set; }

    public class BaseDataModel
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}