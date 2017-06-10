using Newtonsoft.Json;

namespace LineMessagingAPISDK.Models
{
    public class MessageImageMapAction : ImageMapAction
    {
        private string text;

        /// <summary>
        /// Message to send
        /// Max: 400 characters
        /// </summary>
        [JsonProperty("text")]
        public string Text
        {
            get { return text; }
            set { text = value?.Length > 400 ? value.Substring(0, 400) : value; }
        }

        public MessageImageMapAction(string text, ImageMapArea area)
        {
            this.Type = ImageMapActionType.Message;
            this.Text = text;
            this.Area = area;
        }
    }
}
