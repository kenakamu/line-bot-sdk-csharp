using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LineMessagingAPISDK.Models
{
    public class RichMenu
    {
        /// <summary>
        /// Rich menu ID
        /// </summary>
        [JsonProperty("richMenuId")]
        public string RichMenuId { get; set; }

        /// <summary>
        /// size object which contains the width and height of the rich menu displayed in the chat. Rich menu images must be one of the following sizes: 2500x1686, 2500x843.
        /// </summary>
        [JsonProperty("size")]
        public RichMenuSize Size { get; set; }

        /// <summary>
        /// true to display the rich menu by default. Otherwise, false.
        /// </summary>
        [JsonProperty("selected")]
        public bool Selected { get; set; }

        /// <summary>
        /// Name of the rich menu. This value can be used to help manage your rich menus and is not displayed to users. Maximum of 300 characters.
        /// </summary>
        [StringLength(300, ErrorMessage = "Max: 300 characters")]
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Text displayed in the chat bar. Maximum of 14 characters.
        /// </summary>
        [StringLength(14, ErrorMessage = "Max: 14 characters")]
        [JsonProperty("chatBarText")]
        public string ChatBarText { get; set; }

        /// <summary>
        /// Array of area objects which define the coordinates and size of tappable areas. Maximum of 20 area objects.
        /// </summary>
        [JsonProperty("areas")]
        public List<RichMenuArea> Areas { get; set; }
    }
}
