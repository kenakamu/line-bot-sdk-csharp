using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LineMessagingAPISDK.Models
{
    /// <summary>
    /// Imagemaps are images with one or more links. You can assign one link for the entire image or multiple links which correspond to different regions of the image
    /// </summary>
    public class ImageMapMessage : Message
    {
        /// <summary>
        /// To use an imagemap, you must include URLs with the image width (px) at the end of the base URL so that the client can download the image at the required resolution.
        /// For example, if the base URL is,
        /// https://example.com/images/cats
        /// the URL for a client device to download a 700px image would be
        /// https://example.com/images/cats/700
        /// Below are the image resolutions required by client devices.
        /// •Width: 240px, 300px, 460px, 700px, 1040px
        /// The image used for the imagemap must meet the following specifications:
        /// •Image format: JPEG or PNG
        /// •File size: Up to 1 MB
        /// </summary>
        [StringLength(1000, ErrorMessage = "Max: 1000 characters")]
        [RegularExpression("^https://", ErrorMessage = "Require HTTPS")]
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }

        private string altText;
        /// <summary>
        /// Alternative text
        /// Max: 400 characters
        /// text will be truncated if it exceeds 400 characters.
        [JsonProperty("altText")]
        public string AltText
        {
            get { return altText; }
            set { altText = value?.Length > 400 ? value.Substring(0, 400) : value; }
        }

        /// <summary>
        /// Defines the size of the full imagemap with the width as 1040px. The top left is used as the origin of the area.
        /// </summary>
        [JsonProperty("baseSize")]
        public BaseSize BaseSize { get; set; }

        /// <summary>
        /// Object which specifies the actions and tappable regions of an imagemap.
        /// When a region is tapped, the user is redirected to the URI specified in uri and the message specified in message is sent.
        /// </summary>
        [JsonProperty("actions")]
        public List<ImageMapAction> Actions { get; set; }

        public ImageMapMessage(string baseUrl = "", string altText = "", BaseSize baseSize = null, List<ImageMapAction> actions = null)
        {
            Type = MessageType.Imagemap;
            this.BaseUrl = baseUrl;
            this.AltText = altText;
            this.BaseSize = baseSize;
            this.Actions = actions ?? new List<ImageMapAction>();
        }
    }    
}
