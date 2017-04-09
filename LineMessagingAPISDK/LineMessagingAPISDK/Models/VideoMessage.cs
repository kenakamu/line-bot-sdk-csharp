using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;

namespace LineMessagingAPISDK.Models
{
    public class VideoMessage : Message
    {
        /// <summary>
        /// URL of video file (Max: 1000 characters)
        /// HTTPS
        /// mp4
        /// Less than 1 minute
        /// Max: 10 MB
        /// </summary>
        [StringLength(1000, ErrorMessage = "Max: 1000 characters")]
        [RegularExpression("^https://.*(mp4|mpeg4)$", ErrorMessage = "Require HTTPS and mp4")]       
        [JsonProperty("originalContentUrl")]
        public string OriginalContentUrl { get; set; }

        /// <summary>
        /// Preview image URL (Max: 1000 characters)
        /// HTTPS
        /// JPEG
        /// Max: 240 x 240
        /// Max: 1 MB
        /// </summary>
        [StringLength(1000, ErrorMessage = "Max: 1000 characters")]
        [RegularExpression("^https://.*(jpeg|jpg)$", ErrorMessage = "Require HTTPS and jpg")]       
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
