using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LineMessagingAPISDK.Models
{
    /// <summary>
    /// Respond to events from users, groups, and rooms.
    /// Webhooks are used to notify you when an event occurs.For events that you can respond to, a replyToken is issued for replying to messages.
    /// Because the replyToken becomes invalid after a certain period of time, responses should be sent as soon as a message is received.Reply tokens can only be used once.
    /// </summary>
    public class ReplyMessage
    {
        [JsonProperty("replyToken")]
        public string ReplyToken { get; set; }

        [JsonProperty("messages")]
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
