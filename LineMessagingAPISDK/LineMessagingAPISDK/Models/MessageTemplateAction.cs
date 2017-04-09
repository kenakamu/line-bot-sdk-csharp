using Newtonsoft.Json;
using System;

namespace LineMessagingAPISDK.Models
{
    /// <summary>
    /// When this action is tapped, the string in the text field is sent as a message from the user.
    /// </summary>
    public class MessageTemplateAction : TemplateAction
    {
        private string text;
        /// <summary>
        /// Text sent when the action is performed
        /// Max: 300 characters
        /// text will be truncated if it exceeds 300 characters.
        /// </summary>
        [JsonProperty("text")]
        public string Text
        {
            get { return text; }
            set { text = value?.Length > 300 ? value.Substring(0, 300) : value; }
        }

        public MessageTemplateAction(string label, string text)
        {
            Type = TemplateActionType.Message;
            this.Label = label;
            this.Text = text;
        }
    }
}
