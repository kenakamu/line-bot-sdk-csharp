using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LineMessagingAPISDK.Models
{
    public class ButtonsTemplate : Template
    {        
        [JsonProperty("thumbnailImageUrl")]
        public string ThumbnailImageUrl { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("actions")]
        public List<TemplateAction> Actions { get; set; }

        public ButtonsTemplate(string thumbnailImageUrl= null, string title = null, string text = "", List<TemplateAction> actions = null)
        {
            Type = TemplateType.Buttons;
            this.ThumbnailImageUrl = thumbnailImageUrl;
            this.Title = title;
            this.Text = text;
            this.Actions = actions ?? new List<TemplateAction>();
        }
    }
}
