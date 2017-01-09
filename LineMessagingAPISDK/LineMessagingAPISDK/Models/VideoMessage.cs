using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace LineMessagingAPISDK.Models
{
    public class VideoMessage : Message
    {
        [JsonProperty("originalContentUrl")]
        public string OriginalContentUrl { get; set; }

        [JsonProperty("previewImageUrl")]
        public string PreviewImageUrl { get; set; }

        public VideoMessage(string originalContentUrl, string previewImageUrl)
        {
            Type = MessageType.Video;
            this.OriginalContentUrl = originalContentUrl;
            this.PreviewImageUrl = previewImageUrl;
        }
    }
}
