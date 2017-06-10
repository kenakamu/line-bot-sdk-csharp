using Newtonsoft.Json;

namespace LineMessagingAPISDK.Models
{
    public class Source
    {
        [JsonProperty("type")]
        public SourceType Type { get; set; }

        /// <summary>
        ///  ID of the source user 
        /// </summary>
        [JsonProperty("userId")]
        public string UserId { get; set; }

        /// <summary>
        ///  ID of the source group 
        /// </summary>
        [JsonProperty("groupId")]
        public string GroupId { get; set; }

        /// <summary>
        /// ID of the source room
        /// </summary>
        [JsonProperty("roomId")]
        public string RoomId { get; set; }
    }
}
