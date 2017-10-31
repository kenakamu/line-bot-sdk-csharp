using Newtonsoft.Json;

namespace LineMessagingAPISDK.Models
{
    public class MemberIdsResponse
    {
        [JsonProperty("memberIds")]
        public string[] MemberIds { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }
    }
}
