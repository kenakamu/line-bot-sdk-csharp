using Newtonsoft.Json;
using System;

namespace LineMessagingAPISDK.Models
{
    public enum SourceType { User, Group, Room }

    public class Source
    {
        [JsonProperty("type")]
        public SourceType Type { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("groupId")]
        public string GroupId { get; set; }

        [JsonProperty("roomId")]
        public string RoomId { get; set; }
    }
}
