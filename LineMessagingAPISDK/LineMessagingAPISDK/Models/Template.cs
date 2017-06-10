using Newtonsoft.Json;

namespace LineMessagingAPISDK.Models
{
    public abstract class Template
    {
        [JsonProperty("type")]
        public TemplateType Type { get; internal set; }
    }
}
