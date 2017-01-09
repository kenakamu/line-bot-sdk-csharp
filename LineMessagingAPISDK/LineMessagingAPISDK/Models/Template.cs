using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LineMessagingAPISDK.Models
{
    public enum TemplateType { Buttons, Confirm, Carousel }

    public abstract class Template
    {
        [JsonProperty("type")]
        public TemplateType Type { get; internal set; }
    }
}
