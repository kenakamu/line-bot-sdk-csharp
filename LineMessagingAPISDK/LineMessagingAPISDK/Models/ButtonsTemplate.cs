using LineMessagingAPISDK.Validators;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LineMessagingAPISDK.Models
{
    /// <summary>
    /// Template message with an image, title, text, and multiple action buttons.
    /// </summary>
    public class ButtonsTemplate : Template
    {
        /// <summary>
        ///  Image URL (Max: 1000 characters)
        ///  HTTPS
        ///  JPEG or PNG
        ///  Aspect ratio: 1:1.51
        ///  Max width: 1024px
        ///  Max: 1 MB
        /// </summary>
        [StringLength(1000, ErrorMessage = "Max: 1000 characters")]
        [RegularExpression("^https://.*(jpg|jpeg|png)$", ErrorMessage = "Require HTTPS and jpeg/png")]
        [JsonProperty("thumbnailImageUrl")]
        public string ThumbnailImageUrl { get; set; }

        private string title;
        /// <summary>
        /// Max: 40 characters
        /// text will be truncated if it exceeds 40 characters.
        /// </summary>
        [JsonProperty("title")]
        public string Title
        {
            get { return title; }
            set { title = value?.Length > 40 ? value.Substring(0, 40) : value; }
        }

        private string text;
        /// <summary>
        /// Message text
        /// Max: 160 characters(no image or title)
        /// Max: 60 characters(message with an image or title)
        /// text will be truncated if it exceeds its limit.
        /// </summary>
        [JsonProperty("text")]
        public string Text
        {
            get { return text; }
            set
            {
                if (string.IsNullOrEmpty(ThumbnailImageUrl) && string.IsNullOrEmpty(Title))
                    text = value?.Length > 160 ? value.Substring(0, 160) : value;
                else
                    text = value?.Length > 60 ? value.Substring(0, 60) : value;
            }
        }

        /// <summary>
        ///  Action when tapped
        ///  Max: 4 
        /// </summary>
        [ItemCounts(4, ErrorMessage ="You can store up to 4 actions")]
        [JsonProperty("actions")]
        public List<TemplateAction> Actions { get; set; }

        public ButtonsTemplate(string thumbnailImageUrl= null, string title = null, string text = null, List<TemplateAction> actions = null)
        {
            Type = TemplateType.Buttons;
            this.ThumbnailImageUrl = thumbnailImageUrl;
            this.Title = title;
            this.Text = text;
            this.Actions = actions ?? new List<TemplateAction>();
        }
    }
}
