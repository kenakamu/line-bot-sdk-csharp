using Newtonsoft.Json;
using System;

namespace LineMessagingAPISDK.Models
{
    /// <summary>
    /// Action to include in your template message.
    /// </summary>
    public abstract class TemplateAction
    {
        [JsonProperty("type")]
        public TemplateActionType Type { get; set; }

        private string label;

        /// <summary>
        /// Label for the action
        /// Max: 20 characters
        /// label will be truncated if it exceeds 20 characters.
        /// </summary>
        [JsonProperty("label")]
        public string Label
        {
            get { return label; }
            set { label = value?.Length > 20 ? value.Substring(0, 20) : value; }
        }         
    }
}
