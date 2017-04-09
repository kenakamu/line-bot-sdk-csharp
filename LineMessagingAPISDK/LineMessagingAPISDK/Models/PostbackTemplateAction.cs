using Newtonsoft.Json;
using System;

namespace LineMessagingAPISDK.Models
{
    /// <summary>
    /// When this action is tapped, a postback event is returned via webhook with the specified string in the data field.
    /// If you have included the text field, the string in the text field is sent as a message from the user.
    /// </summary>
    public class PostbackTemplateAction : TemplateAction
    {
        private string data;
        /// <summary>
        /// String returned via webhook in the postback.data property of the postback event
        /// Max: 300 characters
        /// data will be truncated if it exceeds 300 characters.
        /// </summary>
        [JsonProperty("data")]
        public string Data
        {
            get { return data; }
            set { data = value?.Length > 300 ? value.Substring(0, 300) : value; }
        }

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

        public PostbackTemplateAction(string label, string data, string text)
        {
            Type = TemplateActionType.Postback;
            this.Label = label;
            this.Data = data;
            this.Text = text;
        }
    }
}
