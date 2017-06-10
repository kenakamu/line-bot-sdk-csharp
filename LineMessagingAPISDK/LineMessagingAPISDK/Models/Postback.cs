using Newtonsoft.Json;

namespace LineMessagingAPISDK.Models
{
    /// <summary>
    /// Event object for when a user performs an action on a template message which initiates a postback. You can reply to postback events.
    /// </summary>
    public class Postback
    {
        /// <summary>
        /// Postback data
        /// </summary>
        [JsonProperty("data")]
        public string Data { get; set; }        
    }
}
