using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LineMessagingAPISDK.Models
{
    /// <summary>
    /// Template message with two action buttons.
    /// </summary>
    public class ConfirmTemplate : Template
    {
        private string text;

        /// <summary>
        /// Message text
        /// Max: 240 characters
        /// text will be truncated if it exceeds 240 characters.
        /// </summary>
        [JsonProperty("text")]
        public string Text
        {
            get { return text; }
            set { text = value?.Length > 240 ? value.Substring(0, 240) : value; }
        }

        [JsonProperty("actions")]
        public List<TemplateAction> Actions { get; set; }

        public ConfirmTemplate(string text = null, List<TemplateAction> actions = null)
        {
            Type = TemplateType.Confirm;           
            this.Text = text;
            this.Actions = actions ?? new List<TemplateAction>();
        }
    }
}
