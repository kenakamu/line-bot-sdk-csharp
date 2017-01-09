using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace LineMessagingAPISDK.Models
{
    public class AudioMessage : Message
    {
        [JsonProperty("originalContentUrl")]
        public string OriginalContentUrl { get; set; }

        [JsonProperty("duration")]
        public int? DurationInMilliseconds { get; set; }

        public AudioMessage(string originalContentUrl, int durationInMilliseconds)
        {
            Type = MessageType.Audio;
            this.OriginalContentUrl = originalContentUrl;
            this.DurationInMilliseconds = durationInMilliseconds;
        }
    }
}
