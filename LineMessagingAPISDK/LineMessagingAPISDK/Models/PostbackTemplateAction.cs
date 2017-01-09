using Newtonsoft.Json;
using System;

namespace LineMessagingAPISDK.Models
{
    public class PostbackTemplateAction : TemplateAction
    {    
        [JsonProperty("data")]
        public string Data { get; set; }

        public PostbackTemplateAction(string label, string data)
        {
            Type = TemplateActionType.Postback;
            this.Label = label;
            this.Data = data;
        }
    }
}
