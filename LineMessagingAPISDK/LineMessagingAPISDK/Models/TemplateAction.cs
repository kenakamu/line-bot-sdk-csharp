using Newtonsoft.Json;
using System;

namespace LineMessagingAPISDK.Models
{
    public enum TemplateActionType { Postback, Uri, Message }

    public abstract class TemplateAction
    {
        [JsonProperty("type")]
        public TemplateActionType Type { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }       
    }
}
