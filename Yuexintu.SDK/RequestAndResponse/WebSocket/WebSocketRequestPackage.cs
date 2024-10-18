using Newtonsoft.Json;

public class WebSocketRequestPackage
{
    public WebSocketRequestPackage()
    {
        //支持反序列化
    }
    public WebSocketRequestPackage(string message)
    {
        var webSocketRequestPayload = JsonConvert.DeserializeObject<WebSocketRequestPackage>(message);
        if (webSocketRequestPayload == null)
        {
            throw new Exception($"{nameof(WebSocketRequestPackage)}无法解析的消息: {message}");
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