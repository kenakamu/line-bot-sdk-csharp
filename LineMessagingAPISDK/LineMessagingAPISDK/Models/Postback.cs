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

        /// <summary>
        /// Object with the date and time selected by a user through a datetime picker action. The full-date, time-hour, and time-minute formats follow the RFC3339 protocol.
        /// </summary>
        [JsonProperty("params")]
        public PostbackParams Params { get; set; }
    }
}
