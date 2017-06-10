using Newtonsoft.Json;
using System.Collections.Generic;

namespace LineMessagingAPISDK.Models
{
    /// <summary>
    /// Send messages to multiple users at any time.
    /// </summary>
    public class MulticastMessage
    {
        /// <summary>
        /// IDs of the receivers
        /// Max: 150 users
        /// Use IDs returned via the webhook event of source users. IDs of groups or rooms cannot be used.
        /// Do not use the LINE ID found on the LINE app.
        /// </summary>
        [JsonProperty("to")]
        public List<string> To { get; set; }

        /// <summary>
        /// Messages
        /// Max: 5
        /// </summary>
        [JsonProperty("messages")]
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
