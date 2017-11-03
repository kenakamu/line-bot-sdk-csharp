using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LineMessagingAPISDK.Models
{
    public class RichMenuSize
    {
        /// <summary>
        /// Width of the rich menu. Must be 2500.
        /// </summary>
        [JsonProperty("width")]
        public int Width = 2500;

        /// <summary>
        /// Height of the rich menu. Possible values: 1686, 843.
        /// </summary>
        [JsonProperty("height")]
        public int Height { get; set; }

        public RichMenuSize(int height)
        {            
            this.Height = height;
        }
    }
}