using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineMessagingAPISDK.Models
{
    /// <summary>
    /// Defines the size of the full imagemap with the width as 1040px. The top left is used as the origin of the area.
    /// </summary>
    public class ImageMapArea
    {
        /// <summary>
        /// Horizontal position of the tappable area
        /// </summary>
        [JsonProperty("x")]
        public double X { get; set; }

        /// <summary>
        /// Vertical position of the tappable area
        /// </summary>
        [JsonProperty("y")]
        public double Y { get; set; }

        /// <summary>
        /// Width of the tappable area
        /// </summary>
        [JsonProperty("width")]
        public double Width { get; set; }

        /// <summary>
        /// Height of the tappable area
        /// </summary>
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
