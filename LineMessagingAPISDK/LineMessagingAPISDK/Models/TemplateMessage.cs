using Newtonsoft.Json;

namespace LineMessagingAPISDK.Models
{
    /// <summary>
    /// Template messages are messages with predefined layouts which you can customize. There are three types of templates available that can be used to interact with users through your bot.
    /// </summary>
    public class TemplateMessage : Message
    {
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
        /// Object with the contents of the template.
        /// </summary>
        [JsonProperty("template")]
        public Template Template { get; set; }

        public TemplateMessage(string altText, Template template)
        {
            Type = MessageType.Template;
            this.AltText = altText;
            this.Template = template;
        }
    }
}
