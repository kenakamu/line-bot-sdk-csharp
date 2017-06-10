using Newtonsoft.Json;

namespace LineMessagingAPISDK.Models
{
    /// <summary>
    /// Object which specifies the actions and tappable regions of an imagemap.
    /// When a region is tapped, the user is redirected to the URI specified in uri and the message specified in message is sent.
    /// </summary>
    public abstract class ImageMapAction
    {
        [JsonProperty("type")]
        public ImageMapActionType Type { get; set; }
        
        [JsonProperty("area")]
        public ImageMapArea Area { get; set; }      
    }
}
