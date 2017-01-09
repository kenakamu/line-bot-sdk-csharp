using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace LineMessagingAPISDK.Models
{
    public class Profile
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
        
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("pictureUrl")]
        public string PictureUrl { get; set; }

        [JsonProperty("statusMessage")]
        public string StatusMessage { get; set; }        
    }
}
