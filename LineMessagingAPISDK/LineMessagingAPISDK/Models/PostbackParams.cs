using Newtonsoft.Json;

namespace LineMessagingAPISDK.Models
{
    public class PostbackParams
    {
        /// <summary>
        /// full-date
        /// Date selected by user. Only included in the date mode.
        /// </summary>
        [JsonProperty("date")]
        public string Date { get; set; }

        /// <summary>
        /// time-hour ":" time-minute
        /// Time selected by the user.Only included in the time mode.
        /// </summary>
        [JsonProperty("time")]
        public string Time { get; set; }

        /// <summary>
        /// full-date "T" time-hour ":" time-minute
        /// Date and time selected by the user. Only included in the datetime mode.
        /// </summary>
        [JsonProperty("datetime")]
        public string DateTime { get; set; }
    }
}