using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace LineMessagingAPISDK.Models
{
    public enum BeaconType { enter }

    /// <summary>
    /// Event object for when a user enters or leaves the range of a LINE Beacon. You can reply to beacon events.
    /// </summary>
    public class Beacon
    {
        [JsonProperty("hwid")]
        public string Wwid { get; set; }
        
        [JsonProperty("type")]
        public BeaconType Type { get; set; }        
    }
}
