using Newtonsoft.Json;
using System;

namespace LineMessagingAPISDK.Models
{
    public class Postback
    {
        [JsonProperty("data")]
        public string Data { get; set; }        
    }
}
