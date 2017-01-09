using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LineMessagingAPISDK.Models
{
    public class ConfirmTemplate : Template
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("actions")]
        public List<TemplateAction> Actions { get; set; }

        public ConfirmTemplate(string text = "", List<TemplateAction> actions = null)
        {
            Type = TemplateType.Confirm;           
            this.Text = text;
            this.Actions = actions;
        }
    }
}
