using Newtonsoft.Json;

namespace LineMessagingAPISDK.Models
{
    public class RichMenuArea
    {
        /// <summary>
        /// Object describing the boundaries of the area in pixels. See bounds object.
        /// </summary>
        [JsonProperty("bounds")]
        public RichMenuBounds Bounds { get; set; }

        [JsonProperty("action")]
        public TemplateAction Action { get; set; }
    }
}