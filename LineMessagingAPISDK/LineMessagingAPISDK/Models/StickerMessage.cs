using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace LineMessagingAPISDK.Models
{
    /// <summary>
    /// For a list of the sticker IDs for stickers that can be sent with the Messaging API, see Sticker list.
    /// </summary>
    public class StickerMessage : Message
    {
        [JsonProperty("packageId")]
        public string PackageId { get; set; }

        [JsonProperty("stickerId")]
        public string StickerId { get; set; }

        public StickerMessage(string packageId, string stickerId)
        {
            Type = MessageType.Sticker;
            this.PackageId = packageId;
            this.StickerId = stickerId;
        }
    }
}
