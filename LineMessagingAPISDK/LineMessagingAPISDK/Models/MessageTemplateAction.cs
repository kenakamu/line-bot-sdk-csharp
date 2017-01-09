using Newtonsoft.Json;
using System;

namespace LineMessagingAPISDK.Models
{
    public class MessageTemplateAction : TemplateAction
    { 
        [JsonProperty("text")]
        public string Text { get; set; }

        public MessageTemplateAction(string label, string text)
        {
            Type = TemplateActionType.Message;
            this.Label = label;
            this.Text = text;
        }
    }
}
