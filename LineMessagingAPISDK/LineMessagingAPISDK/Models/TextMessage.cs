using Newtonsoft.Json;

namespace LineMessagingAPISDK.Models
{
    /// <summary>
    /// Message object which contains the text sent from the source or send to user.
    /// </summary>
    public class TextMessage : Message
    {
        private string text;

        /// <summary>
        /// Message text
        /// Max: 2000 characters
        /// text will be truncated if it exceeds 2000 characters.
        /// </summary>
        [JsonProperty("text")]
        public string Text
        {
            get { return text; }
            set { text = value?.Length > 2000 ? value.Substring(0, 2000) : value; }
        }

        public TextMessage(string text)
        {
            Type = MessageType.Text;
            this.Text = text;
        }
    }
}
