using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LineMessagingAPISDK.Models
{
    public class TemplateColumn
    {
        [JsonProperty("thumbnailImageUrl")]
        public string ThumbnailImageUrl { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("actions")]
        public List<TemplateAction> Actions { get; set; } = new List<TemplateAction>();
    }
}
