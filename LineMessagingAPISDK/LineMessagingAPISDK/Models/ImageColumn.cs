using LineMessagingAPISDK.Validators;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LineMessagingAPISDK.Models
{
    /// <summary>
    /// Column for Carousel Template
    /// </summary>
    public class ImageColumn
    {
        /// <summary>
        ///  Image URL (Max: 1000 characters)
        ///  HTTPS
        ///  JPEG or PNG
        ///  Aspect ratio: 1:1
        ///  Max width: 1024px
        ///  Max: 1 MB
        /// </summary>
        [StringLength(1000, ErrorMessage = "Max: 1000 characters")]
        [RegularExpression("^https://.*(jpg|jpeg|png)$", ErrorMessage = "Require HTTPS and jpeg/png")]
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        /// <summary>
        ///  Action when image is tapped
        /// </summary>
        [JsonProperty("action")]
        public TemplateAction Action { get; set; }
        
        public ImageColumn(string imageUrl, TemplateAction action)
        {
            this.ImageUrl = imageUrl;
            this.Action = action;
        }
    }
}
