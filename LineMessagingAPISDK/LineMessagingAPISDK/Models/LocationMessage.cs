using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace LineMessagingAPISDK.Models
{
    public class LocationMessage : Message
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

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
