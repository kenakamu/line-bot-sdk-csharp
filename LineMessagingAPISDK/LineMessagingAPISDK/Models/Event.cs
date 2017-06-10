using Newtonsoft.Json;
using System.Collections.Generic;

namespace LineMessagingAPISDK.Models
{    
    public class Event
    {
        [JsonProperty("replyToken")]
        public string ReplyToken { get; set; }
        
        [JsonProperty("type")]
        public EventType Type { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("source")]
        public Source Source { get; set; }

        [JsonProperty("message", TypeNameHandling = TypeNameHandling.Auto)]
        public object Message { get; set; }

        [JsonProperty("postback")]
        public Postback Postback { get; set; }

        [JsonProperty("beacon")]
        public Beacon Beacon { get; set; }

        public ReplyMessage CreateReply(string text = null, Message message = null)
        {
            ReplyMessage replyMessage = new ReplyMessage();

            replyMessage.ReplyToken = this.ReplyToken;

            if (!string.IsNullOrEmpty(text))
                replyMessage.Messages.Add(new TextMessage(text));
            if (message != null)
                replyMessage.Messages.Add(message);
            return replyMessage;
        }

        public ReplyMessage CreateReply(string text = null, List<Message> messages = null)
        {
            ReplyMessage replyMessage = new ReplyMessage();

            replyMessage.ReplyToken = this.ReplyToken;

            if (!string.IsNullOrEmpty(text))
                replyMessage.Messages.Add(new TextMessage(text));
            if (messages != null)
                replyMessage.Messages.AddRange(messages);
            return replyMessage;
        }

        public PushMessage CreatePush(string text = null, Message message = null)
        {
            PushMessage pushMessage = new PushMessage();

            pushMessage.To = this.Source.UserId ?? this.Source.GroupId ?? this.Source.RoomId;

            if (!string.IsNullOrEmpty(text))
                pushMessage.Messages.Add(new TextMessage(text));
            if (message != null)
                pushMessage.Messages.Add(message);
            return pushMessage;
        }

        public PushMessage CreatePush(string text = null, List<Message> messages = null)
        {
            PushMessage pushMessage = new PushMessage();

            pushMessage.To = this.Source.UserId ?? this.Source.GroupId ?? this.Source.RoomId;

            if (!string.IsNullOrEmpty(text))
                pushMessage.Messages.Add(new TextMessage(text));
            if (messages != null)
                pushMessage.Messages.AddRange(messages);
            return pushMessage;
        }
    }
}
