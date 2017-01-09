using Newtonsoft.Json;
using System;

namespace LineMessagingAPISDK.Models
{
    public class UriTemplateAction : TemplateAction
    {       
        [JsonProperty("uri")]
        public string Uri { get; set; }

        public UriTemplateAction(string label, string uri)
        {
            Type = TemplateActionType.Uri;
            this.Label = label;
            this.Uri = uri;
        }
    }
}
