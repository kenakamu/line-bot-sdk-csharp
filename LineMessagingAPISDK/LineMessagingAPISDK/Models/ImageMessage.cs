using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace LineMessagingAPISDK.Models
{
    public class ImageMessage : Message
    {
        [JsonProperty("originalContentUrl")]
        public string OriginalContentUrl { get; set; }

        [JsonProperty("previewImageUrl")]
        public string PreviewImageUrl { get; set; }

        public ImageMessage(string originalContentUrl, string previewImageUrl)
        {
            Type = MessageType.Image;
            this.OriginalContentUrl = originalContentUrl;
            this.PreviewImageUrl = previewImageUrl;
        }
    }
}
