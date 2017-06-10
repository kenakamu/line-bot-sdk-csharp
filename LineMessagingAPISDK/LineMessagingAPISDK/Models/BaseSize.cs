using Newtonsoft.Json;

namespace LineMessagingAPISDK.Models
{
    /// <summary>
    /// BaseSize property of ImageMap
    /// </summary>
    public class BaseSize
    {
        /// <summary>
        /// Width of base image (set to 1040px）
        /// </summary>
        [JsonProperty("width")]
        public double Width { get; set; }

        /// <summary>
        /// Height of base image（set to the height that corresponds to a width of 1040px）
        /// </summary>
        [JsonProperty("height")]
        public double Height { get; set; }

        public BaseSize(double width, double height)
        {
            this.Width = width;
            this.Height = height;
        }
    }
}
