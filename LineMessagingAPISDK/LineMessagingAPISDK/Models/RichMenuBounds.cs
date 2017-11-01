using Newtonsoft.Json;

namespace LineMessagingAPISDK.Models
{
    public class RichMenuBounds
    {
        [JsonProperty("x")]
        public int X = 2500;

        [JsonProperty("y")]
        public int Y = 2500;

        [JsonProperty("width")]
        public int Width = 2500;

        [JsonProperty("height")]
        public int Height = 2500;

        public RichMenuBounds(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }
    }
}