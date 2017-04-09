using Newtonsoft.Json;
using System.Collections.Generic;

namespace LineMessagingAPISDK.Models
{
    public class MulticastMessage
    {
        [JsonProperty("to")]
        public List<string> To { get; set; }
        
        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }
    }
}
