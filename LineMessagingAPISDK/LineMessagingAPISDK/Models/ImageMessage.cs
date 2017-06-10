using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LineMessagingAPISDK.Models
{
    public class ImageMessage : Message
    {
        /// <summary>
        /// Image URL (Max: 1000 characters)
        /// HTTPS
        /// JPEG
        /// Max: 1024 x 1024
        /// Max: 1 MB
        /// </summary>
        [StringLength(1000, ErrorMessage = "Max: 1000 characters")]
        [RegularExpression("^https://.*(jpg|jpeg)$", ErrorMessage = "Require HTTPS and jpeg")]       
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
        [RegularExpression("^https://.*(jpg|jpeg)$", ErrorMessage = "Require HTTPS and jpeg")]       
        [JsonProperty("previewImageUrl")]
        public string PreviewImageUrl { get; set; }

        public ImageMessage(string originalContentUrl, string previewImageUrl)
        {
            Type = MessageType.Image;
            this.OriginalContentUrl = originalContentUrl.Replace("http://", "https://");
            this.PreviewImageUrl = previewImageUrl;
        }
    }
}
