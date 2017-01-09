using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace LineMessagingAPISDK.Models
{
    public enum BeaconType { enter }

    public class Beacon
    {
        [JsonProperty("hwid")]
        public string Wwid { get; set; }
        
        [JsonProperty("type")]
        public BeaconType Type { get; set; }        
    }
}
