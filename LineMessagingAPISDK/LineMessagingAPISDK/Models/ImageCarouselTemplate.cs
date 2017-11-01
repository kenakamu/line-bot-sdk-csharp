using LineMessagingAPISDK.Validators;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LineMessagingAPISDK.Models
{
    /// <summary>
    /// Template with multiple images which can be cycled like a carousel..
    /// </summary>
    public class ImageCarouselTemplate : Template
    {

        /// <summary>
        ///  Array of columns
        ///  Max: 10 
        /// </summary>
        [ItemCounts(10, ErrorMessage = "You can store up to 10 Columns")]
        [JsonProperty("columns")]
        public List<ImageColumn> Columns { get; set; }

        public ImageCarouselTemplate(List<ImageColumn> columns = null)
        {
            Type = TemplateType.Image_carousel;
            Columns = columns ?? new List<ImageColumn>();
        }
    }
}
