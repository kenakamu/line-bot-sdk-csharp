using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LineMessagingAPISDK.Models
{
    public class AudioMessage : Message
    {       
        /// <summary>
        /// URL of audio file (Max: 1000 characters)
        /// HTTPS
        /// m4a
        /// Less than 1 minute
        /// Max 10 MB
        /// </summary>
        [StringLength(1000, ErrorMessage = "Max: 1000 characters")]
        [RegularExpression("^https://.*(m4a)$", ErrorMessage = "Require HTTPS and m4a")]
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
