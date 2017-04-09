using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace LineMessagingAPISDK.Models
{
    /// <summary>
    /// When this action is tapped, the URI specified in the uri field is opened.
    /// </summary>
    public class UriTemplateAction : TemplateAction
    {
        /// <summary>
        ///  URI opened when the action is performed (Max: 1000 characters)
        ///  http, https, tel
        /// </summary>
        [StringLength(1000, ErrorMessage = "Max: 1000 characters")]
        [RegularExpression("^(https|http|tel):", ErrorMessage = "Should starts with https, http or tel")]
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
