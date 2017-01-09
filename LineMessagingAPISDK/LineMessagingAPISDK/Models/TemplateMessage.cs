using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace LineMessagingAPISDK.Models
{
    public class TemplateMessage : Message
    {

        [JsonProperty("altText")]
        public string AltText { get; set; }

        [JsonProperty("template")]
        public Template Template { get; set; }

        public TemplateMessage(string altText, Template template = null)
        {
            Type = MessageType.Template;
            this.AltText = altText;
            this.Template = template;
        }
    }
}
