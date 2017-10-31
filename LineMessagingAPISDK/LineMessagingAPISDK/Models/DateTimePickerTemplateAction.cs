using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace LineMessagingAPISDK.Models
{
    /// <summary>
    /// When this action is tapped, the URI specified in the uri field is opened.
    /// </summary>
    public class DatetimePickerTemplateAction : TemplateAction
    {
        /// <summary>
        ///  String returned via webhook in the postback.data property of the postback event
        ///  Max: 300 characters
        /// </summary>
        [StringLength(300, ErrorMessage = "Max: 300 characters")]
        [JsonProperty("data")]
        public string Data { get; set; }

        /// <summary>
        /// Action mode
        /// date: Pick date
        /// time: Pick time
        /// datetime: Pick date and time
        /// </summary>
        [JsonProperty("mode")]
        public DatetimePickerMode Mode { get; set; }

        /// <summary>
        /// Initial value of date or time
        /// </summary>
        [JsonProperty("initial")]
        public string Initial { get; set; }

        /// <summary>
        /// Largest date or time value that can be selected.
        /// Must be greater than the min value.
        /// </summary>
        [JsonProperty("max")]
        public string Max { get; set; }

        /// <summary>
        /// Smallest date or time value that can be selected.
        /// Must be less than the max value.
        /// </summary>
        [JsonProperty("min")]
        public string Min { get; set; }

        public DatetimePickerTemplateAction(string label, string data, DatetimePickerMode mode, string initial="", string max = "", string min = "")
        {
            if(!string.IsNullOrEmpty(max) && !string.IsNullOrEmpty(min))
            {
                if (DateTime.Parse(max) < DateTime.Parse(min))
                    throw new Exception("min must be less than the max value");
            }

            Type = TemplateActionType.DatetimePicker;
            this.Label = label;
            this.Data = data;
            this.Mode = mode;
            this.Initial = initial;
            this.Max = max;
            this.Min = min;
        }
    }
}
