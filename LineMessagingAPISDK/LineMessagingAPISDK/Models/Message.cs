using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace LineMessagingAPISDK.Models
{
    public enum MessageType { Text, Image, Video, Audio, Location, Sticker, ImageMap, Template }

    public class Message
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("type")]
        public MessageType Type { get; set; }
    }
}
