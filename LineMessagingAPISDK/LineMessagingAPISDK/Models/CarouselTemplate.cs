using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LineMessagingAPISDK.Models
{
    public class CarouselTemplate : Template
    {
        [JsonProperty("columns")]
        public List<TemplateColumn> Columns { get; set; }

        public CarouselTemplate(List<TemplateColumn> columns = null)
        {
            Type = TemplateType.Carousel;
            Columns = columns ?? new List<TemplateColumn>();
        }
    }
}
