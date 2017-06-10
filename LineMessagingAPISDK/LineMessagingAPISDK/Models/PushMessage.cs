using Newtonsoft.Json;
using System.Collections.Generic;

namespace LineMessagingAPISDK.Models
{
    /// <summary>
    /// Send messages to a user, group, or room at any time.
    /// </summary>
    public class PushMessage
    {
        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("messages")]
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
