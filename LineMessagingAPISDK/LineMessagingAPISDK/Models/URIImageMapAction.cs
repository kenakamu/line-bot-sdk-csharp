using Newtonsoft.Json;

namespace LineMessagingAPISDK.Models
{
    public class UriImageMapAction : ImageMapAction
    {
        [JsonProperty("linkUri")]
        public string LinkUri { get; set; }

        public UriImageMapAction(string linkUri, ImageMapArea area)
        {
            this.Type = ImageMapActionType.Uri;
            this.LinkUri = linkUri;
            this.Area = area;
        }
    }
}
