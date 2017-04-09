using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
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
        /// Base URL of image (Max: 1000 characters)
        /// HTTPS
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
        
        [JsonProperty("baseSize")]
        public BaseSize BaseSize { get; set; }

        [JsonProperty("actions")]
        public List<ImageMapAction> Actions { get; set; }

        public ImageMapMessage(string baseUrl, string altText, BaseSize baseSize = null, List<ImageMapAction> actions = null)
        {
            Type = MessageType.ImageMap;
            this.BaseUrl = baseUrl;
            this.AltText = altText;
            this.BaseSize = baseSize;
            this.Actions = actions ?? new List<ImageMapAction>();
        }
    }

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

    public enum ImageMapActionType { Imagemap }

    public class ImageMapAction
    {
        [JsonProperty("width")]
        public ImageMapActionType Type { get; set; }

        [JsonProperty("linkUri")]
        public string LinkUri { get; set; }

        [JsonProperty("area")]
        public ImageMapArea Area { get; set; }

        public ImageMapAction(string linkUri, ImageMapArea area)
        {
            Type = ImageMapActionType.Imagemap;
            this.LinkUri = linkUri;
            this.Area = area;
        }
    }

    public class ImageMapArea
    {
        [JsonProperty("x")]
        public double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }

        [JsonProperty("width")]
        public double Width { get; set; }

        [JsonProperty("height")]
        public double Height { get; set; }

        public ImageMapArea(double x, double y, double width, double height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }
    }
}
