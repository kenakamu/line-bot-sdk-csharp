using Newtonsoft.Json;
using System;

namespace LineMessagingAPISDK.Models
{
    public class Activity
    {
        [JsonProperty("events")]
        public Event[] Events { get; set; }        
    }
}
