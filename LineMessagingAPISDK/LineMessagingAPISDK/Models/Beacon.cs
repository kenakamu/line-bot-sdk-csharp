using Newtonsoft.Json;

namespace LineMessagingAPISDK.Models
{
    /// <summary>
    /// Event object for when a user enters or leaves the range of a LINE Beacon. You can reply to beacon events.
    /// </summary>
    public class Beacon
    {
        /// <summary>
        ///  Hardware ID of the beacon that was detected 
        /// </summary>
        [JsonProperty("hwid")]
        public string Hwid { get; set; }

        /// <summary>
        /// Type of beacon event
        /// </summary>
        [JsonProperty("type")]
        public BeaconType Type { get; set; }

        /// <summary>
        /// Optional. Device message of beacon that was detected
        /// </summary>
        [JsonProperty("dm")]
        public string Dm { get; set; }
    }
}
