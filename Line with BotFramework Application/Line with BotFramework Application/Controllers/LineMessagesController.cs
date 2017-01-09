using LineMessagingAPISDK;
using LineMessagingAPISDK.Models;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.DirectLine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using dl = Microsoft.Bot.Connector.DirectLine.Models;
using lm = LineMessagingAPISDK.Models;

namespace $safeprojectname$.Controllers
{
    public class LineMessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post(HttpRequestMessage request)
        {
            if (!await VaridateSignature(request))
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            lm.Activity activity = JsonConvert.DeserializeObject<lm.Activity>
                (await request.Content.ReadAsStringAsync());

            // Line may send multiple events in one message, so need to handle them all.
            foreach (Event lineEvent in activity.Events)
            {
                LineMessageHandler handler = new LineMessageHandler(lineEvent);
                await handler.Initialize();
                Profile profile = await handler.GetProfile(lineEvent.Source.UserId);

                switch (lineEvent.Type)
                {
                    case EventType.Beacon:
                        await handler.HandleBeaconEvent();
                        break;
                    case EventType.Follow:
                        await handler.HandleFollowEvent();
                        break;
                    case EventType.Join:
                        await handler.HandleJoinEvent();
                        break;
                    case EventType.Leave:
                        await handler.HandleLeaveEvent();
                        break;
                    case EventType.Message:
                        Message message = JsonConvert.DeserializeObject<Message>(lineEvent.Message.ToString());
                        switch (message.Type)
                        {
                            case MessageType.Text:
                                await handler.HandleTextMessage();
                                break;
                            case MessageType.Audio:
                            case MessageType.Image:
                            case MessageType.Video:
                                await handler.HandleMediaMessage();
                                break;
                            case MessageType.Sticker:
                                await handler.HandleStickerMessage();
                                break;
                            case MessageType.Location:
                                await handler.HandleLocationMessage();
                                break;
                        }
                        break;
                    case EventType.Postback:
                        await handler.HandlePostbackEvent();
                        break;
                    case EventType.Unfollow:
                        await handler.HandleUnfollowEvent();
                        break;
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        private async Task<bool> VaridateSignature(HttpRequestMessage request)
        {
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["ChannelSecret"].ToString()));
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(await request.Content.ReadAsStringAsync()));
            var contentHash = Convert.ToBase64String(computeHash);
            var headerHash = Request.Headers.GetValues("X-Line-Signature").First();

            return contentHash == headerHash;
        }
    }

    public class LineMessageHandler
    {
        private Event lineEvent;
        private static string directLineSecret = ConfigurationManager.AppSettings["DirectLineSecret"].ToString();
        private LineClient lineClient = new LineClient(ConfigurationManager.AppSettings["ChannelToken"].ToString());
        private DirectLineClient dlClient = new DirectLineClient(directLineSecret);
        private string conversationId; // DirectLine ConversationId
        private string watermark; // Limit the messages to get from DirectLine
        private Dictionary<string, object> userParams;

        public LineMessageHandler(Event lineEvent)
        {
            this.lineEvent = lineEvent;
        }

        public async Task Initialize()
        {
            var lineId = lineEvent.Source.UserId ?? lineEvent.Source.GroupId ?? lineEvent.Source.RoomId;

            if (CacheService.caches.Keys.Contains(lineId))
            {
                // Get preserved ConversationId and Watermark from cache.
                // If we scale out, then we have to use different method
                userParams = CacheService.caches[lineId] as Dictionary<string, object>;
                conversationId = userParams.Keys.Contains("ConversationId") ? userParams["ConversationId"].ToString() : "";
                watermark = userParams.Keys.Contains("Watermark") ? userParams["Watermark"].ToString() : "0";
            }
            else
            {
                // If no cache, then create new one.
                userParams = new Dictionary<string, object>();
                var conversation = await dlClient.Conversations.NewConversationAsync();
                userParams["ConversationId"] = conversationId = conversation.ConversationId;
                CacheService.caches[lineId] = userParams;
                watermark = "1";
            }
        }

        public async Task HandleBeaconEvent()
        {
        }

        public async Task HandleFollowEvent()
        {
        }

        public async Task HandleJoinEvent()
        {
        }

        public async Task HandleLeaveEvent()
        {
        }

        public async Task HandlePostbackEvent()
        {
            dl.Message sendMessage = new dl.Message()
            {
                Text = lineEvent.Postback.Data,
                FromProperty = lineEvent.Source.UserId
            };

            // Send the message, then fetch and reply messages,
            await dlClient.Conversations.PostMessageAsync(conversationId, sendMessage);
            await GetAndReplyMessages();
        }

        public async Task HandleUnfollowEvent()
        {
        }

        public async Task<Profile> GetProfile(string mid)
        {
            return await lineClient.GetProfile(mid);
        }

        public async Task HandleTextMessage()
        {
            var textMessage = JsonConvert.DeserializeObject<TextMessage>(lineEvent.Message.ToString());

            dl.Message sendMessage = new dl.Message()
            {
                Text = textMessage.Text,
                FromProperty = lineEvent.Source.UserId
            };

            // Send the message, then fetch and reply messages,
            await dlClient.Conversations.PostMessageAsync(conversationId, sendMessage);
            await GetAndReplyMessages();
        }

        public async Task HandleMediaMessage()
        {
            Message message = JsonConvert.DeserializeObject<Message>(lineEvent.Message.ToString());
            // Get media from Line server.
            var media = await lineClient.GetContent(message.Id);

            // Send the message, then fetch and reply messages,
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", directLineSecret);
                StreamContent content = new StreamContent(media.Content);
                content.Headers.TryAddWithoutValidation("Content-Type", media.ContentType);
                content.Headers.TryAddWithoutValidation("Content-Disposition", $"form-data; name='file'; filename='{media.FileName}'");
                var response = await client.PostAsync(
                    $"https://directline.botframework.com/api/conversations/{conversationId}/upload",
                    content);
            }

            await GetAndReplyMessages();
        }

        public async Task HandleStickerMessage()
        {
            //https://devdocs.line.me/files/sticker_list.pdf
            var stickerMessage = JsonConvert.DeserializeObject<StickerMessage>(lineEvent.Message.ToString());
            var replyMessage = new StickerMessage("1", "1");
            await Reply(replyMessage);
        }

        public async Task HandleLocationMessage()
        {
            var locationMessage = JsonConvert.DeserializeObject<LocationMessage>(lineEvent.Message.ToString());

            dl.Message sendMessage = new dl.Message()
            {
                Text = locationMessage.Title,
                FromProperty = lineEvent.Source.UserId,
                ChannelData = new Place(
                    address: locationMessage.Address,
                    geo: new GeoCoordinates(
                        latitude: locationMessage.Latitude,
                        longitude: locationMessage.Longitude,
                        name: locationMessage.Title),
                    name: locationMessage.Title)
            };

            // Send the message, then fetch and reply messages,
            await dlClient.Conversations.PostMessageAsync(conversationId, sendMessage);
            await GetAndReplyMessages();
        }

        private async Task Reply(Message replyMessage)
        {
            try
            {
                await lineClient.ReplyToActivityAsync(lineEvent.CreateReply(message: replyMessage));
            }
            catch
            {
                await lineClient.PushAsync(lineEvent.CreatePush(message: replyMessage));
            }
        }

        /// <summary>
        /// Get all messages from DirectLine and reply back to Line
        /// </summary>
        private async Task GetAndReplyMessages()
        {
            dl.MessageSet result = string.IsNullOrEmpty(watermark) ?
                await dlClient.Conversations.GetMessagesAsync(conversationId) :
                await dlClient.Conversations.GetMessagesAsync(conversationId, watermark);

            userParams["Watermark"] = (Int64.Parse(result.Watermark)).ToString();

            foreach (var message in result.Messages)
            {
                if (message.FromProperty == lineEvent.Source.UserId)
                    continue;

                Message replyMessage = new Message();

                if (message.Attachments != null && message.Attachments.Count != 0)
                {
                    foreach (var attachment in message.Attachments)
                    {
                        if (attachment.ContentType.Contains("image"))
                        {
                            var originalContentUrl = "";
                            var previewImageUrl = "";
                            if (!string.IsNullOrEmpty(attachment.Url))
                                originalContentUrl =
                                    attachment.Url.Contains("http") ? attachment.Url :
                                    "https://directline.botframework.com/" + attachment.Url;
                            else
                            {
                                // Have to create Uri accessible to Line
                            }

                            replyMessage = new ImageMessage(originalContentUrl, previewImageUrl);
                        }
                        else if (attachment.ContentType.Contains("audio"))
                        {
                            var originalContentUrl = "";
                            var durationInMilliseconds = 0;
                            if (!string.IsNullOrEmpty(attachment.Url))
                                originalContentUrl =
                                    attachment.Url.Contains("http") ? attachment.Url :
                                    "https://directline.botframework.com/" + attachment.Url;
                            else
                            {
                                // Have to create Uri accessible to Line
                            }

                            replyMessage = new AudioMessage(originalContentUrl, durationInMilliseconds);
                        }
                        else if (attachment.ContentType.Contains("video"))
                        {
                            var originalContentUrl = "";
                            var previewImageUrl = "";

                            if (!string.IsNullOrEmpty(attachment.Url))
                                originalContentUrl =
                                    attachment.Url.Contains("http") ? attachment.Url :
                                    "https://directline.botframework.com/" + attachment.Url;
                            else
                            {
                                // Have to create Uri accessible to Line
                            }

                            replyMessage = new VideoMessage(originalContentUrl, previewImageUrl);
                        }
                    }
                }
                else if (message.ChannelData != null)
                {
                    Entity entity = JsonConvert.DeserializeObject<Entity>(message.ChannelData.ToString());
                    switch (entity.Type)
                    {
                        case "GeoCoordinates":
                            GeoCoordinates geoCoordinates = JsonConvert.DeserializeObject<GeoCoordinates>(entity.Properties.ToString());
                            replyMessage = new LocationMessage(message.Text, geoCoordinates.Name, geoCoordinates.Latitude, geoCoordinates.Longitude);
                            break;
                        case "Place":
                            Place place = JsonConvert.DeserializeObject<Place>(entity.Properties.ToString());
                            GeoCoordinates geo = JsonConvert.DeserializeObject<GeoCoordinates>(place.Geo.ToString());
                            replyMessage = new LocationMessage(place.Name, place.Address, geo.Latitude, geo.Longitude);
                            break;
                        case "Confirm":
                            string title = entity.Properties.ToString();
                            ConfirmTemplate confirmTemplate = new ConfirmTemplate(title, new List<TemplateAction>()
                            { new MessageTemplateAction("Yes", "Yes"), new MessageTemplateAction("No", "No") });
                            replyMessage = new TemplateMessage("Confirm template", confirmTemplate);
                            break;
                        case "Rich":
                            List<Attachment> attachments =
                                JsonConvert.DeserializeObject<List<Attachment>>(entity.Properties["Attachments"].ToString());

                            if (attachments.Count == 1)
                            {
                                var attachment = attachments.First();

                                HeroCard hcard = null;

                                if (attachment.ContentType == "application/vnd.microsoft.card.hero")
                                    hcard = JsonConvert.DeserializeObject<HeroCard>(attachment.Content.ToString());
                                else if (attachment.ContentType == "application/vnd.microsoft.card.thumbnail")
                                {
                                    ThumbnailCard tCard = JsonConvert.DeserializeObject<ThumbnailCard>(attachment.Content.ToString());
                                    hcard = new HeroCard(tCard.Title, tCard.Subtitle, tCard.Text, tCard.Images, tCard.Buttons, null);
                                }
                                else
                                    break;

                                ButtonsTemplate buttonsTemplate = new ButtonsTemplate(hcard.Images.First().Url, hcard.Title, hcard.Subtitle);

                                foreach (var button in hcard.Buttons)
                                {
                                    switch (button.Type)
                                    {
                                        case "openUrl":
                                            buttonsTemplate.Actions.Add(new UriTemplateAction(button.Title, button.Value));
                                            break;
                                        case "imBack":
                                            buttonsTemplate.Actions.Add(new MessageTemplateAction(button.Title, button.Value));
                                            break;
                                        case "postBack":
                                            buttonsTemplate.Actions.Add(new PostbackTemplateAction(button.Title, button.Value));
                                            break;
                                    }
                                }

                                replyMessage = new TemplateMessage("Buttons template", buttonsTemplate);
                            }
                            else
                            {
                                CarouselTemplate carouselTemplate = new CarouselTemplate();

                                foreach (var attachment in attachments)
                                {
                                    TemplateColumn tColumn = new TemplateColumn();
                                    HeroCard hcard = null;

                                    if (attachment.ContentType == "application/vnd.microsoft.card.hero")
                                        hcard = JsonConvert.DeserializeObject<HeroCard>(attachment.Content.ToString());
                                    else if (attachment.ContentType == "application/vnd.microsoft.card.thumbnail")
                                    {
                                        ThumbnailCard tCard = JsonConvert.DeserializeObject<ThumbnailCard>(attachment.Content.ToString());
                                        hcard = new HeroCard(tCard.Title, tCard.Subtitle, tCard.Text, tCard.Images, tCard.Buttons, null);
                                    }
                                    else
                                        continue;

                                    foreach (var button in hcard.Buttons)
                                    {
                                        switch (button.Type)
                                        {
                                            case "openUrl":
                                                tColumn.Actions.Add(new UriTemplateAction(button.Title, button.Value));
                                                break;
                                            case "imBack":
                                                tColumn.Actions.Add(new MessageTemplateAction(button.Title, button.Value));
                                                break;
                                            case "postBack":
                                                tColumn.Actions.Add(new PostbackTemplateAction(button.Title, button.Value));
                                                break;
                                        }
                                    }

                                    tColumn.ThumbnailImageUrl = hcard.Images.First().Url;
                                    tColumn.Text = hcard.Subtitle;
                                    tColumn.Title = hcard.Title;

                                    carouselTemplate.Columns.Add(tColumn);
                                }

                                replyMessage = new TemplateMessage("Carousel template", carouselTemplate);
                            }
                            break;
                    }
                }
                else if (!string.IsNullOrEmpty(message.Text))
                {
                    if (message.Text.Contains("\n\n*"))
                    {
                        var lines = message.Text.Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
                        ButtonsTemplate buttonsTemplate = new ButtonsTemplate(text: lines[0]);

                        foreach (var line in lines.Skip(1))
                        {
                            buttonsTemplate.Actions.Add(new PostbackTemplateAction(line, line.Replace("* ", "")));
                        }

                        replyMessage = new TemplateMessage("Buttons template", buttonsTemplate);
                    }
                    else
                        replyMessage = new TextMessage(message.Text);
                }

                await Reply(replyMessage);
            }
        }
    }

    public static class CacheService
    {
        public static Dictionary<string, object> caches;

        static CacheService()
        {
            caches = new Dictionary<string, object>();
        }
    }
}
