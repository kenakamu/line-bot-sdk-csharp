using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace LineMessagingAPISDK.Models
{
    public class ImageMapMessage : Message
    {
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }

        [JsonProperty("altText")]
        public string AltText { get; set; }

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
        [JsonProperty("width")]
        public double Width { get; set; }

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
