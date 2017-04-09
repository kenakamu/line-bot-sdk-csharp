using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace LineMessagingAPISDK.Models
{
    public class LocationMessage : Message
    {
        private string title;
        /// <summary>
        /// Max: 100 characters
        /// title will be truncated if it exceeds 100 characters.
        /// </summary>
        [JsonProperty("title")]
        public string Title
        {
            get { return title; }
            set { title = value?.Length > 100 ? value.Substring(0, 100) : value; }
        }

        private string address;

        /// <summary>
        /// Max: 100 characters
        /// address will be truncated if it exceeds 100 characters.
        /// </summary>
        [JsonProperty("address")]
        public string Address
        {
            get { return address; }
            set { address = value?.Length > 100 ? value.Substring(0, 100) : value; }
        }

        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; set; }

        public LocationMessage(string title, string address, double? latitude, double? longitude)
        {
            Type = MessageType.Location;
            this.Title = title;
            this.Address = address;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
    }
}
