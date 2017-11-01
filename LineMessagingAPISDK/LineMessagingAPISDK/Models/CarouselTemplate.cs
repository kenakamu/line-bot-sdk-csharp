using LineMessagingAPISDK.Validators;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LineMessagingAPISDK.Models
{
    /// <summary>
    /// Template message with multiple columns which can be cycled like a carousel.
    /// </summary>
    public class CarouselTemplate : Template
    {
        /// <summary>
        ///  Array of columns
        ///  Max: 10 
        /// </summary>
        [ItemCounts(10, ErrorMessage = "You can store up to 10 Columns")]
        [JsonProperty("columns")]
        public List<TemplateColumn> Columns { get; set; }

        public CarouselTemplate(List<TemplateColumn> columns = null)
        {
            Type = TemplateType.Carousel;
            Columns = columns ?? new List<TemplateColumn>();
        }
    }
}
