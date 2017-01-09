using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace LineMessagingAPISDK.Models
{
    public class TextMessage : Message
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        public TextMessage(string text)
        {
            Type = MessageType.Text;
            this.Text = text;
        }
    }
}
