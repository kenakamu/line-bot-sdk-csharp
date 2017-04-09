using LineMessagingAPISDK.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LineMessagingAPISDK
{
    public class LineClient
    {
        const string replyEndpoint = "https://api.line.me/v2/bot/message/reply";
        const string pushEndpoint = "https://api.line.me/v2/bot/message/push";
        const string multicastEndpoint = "https://api.line.me/v2/bot/message/multicast";
        const string contentEndpoint = "https://api.line.me/v2/bot/message/{0}/content";
        const string profileEndpoint = "https://api.line.me/v2/bot/profile/{0}";
        const string leaveEndpoint = "https://api.line.me/v2/bot/room/{0}/leave";
        public string AccessToken;

        public LineClient(string accessToken)
        {
            this.AccessToken = accessToken;
        }

        /// <summary>
        /// https://devdocs.line.me/en/#reply-message
        /// Respond to events from users, groups, and rooms.
        /// Webhooks are used to notify you when an event occurs.For events that you can respond to, a replyToken is issued for replying to messages.
        /// Because the replyToken becomes invalid after a certain period of time, responses should be sent as soon as a message is received.Reply tokens can only be used once.
        /// </summary>
        /// <param name="replyMessage"></param>
        public async Task ReplyToActivityAsync(ReplyMessage replyMessage)
        {
            using (HttpClient client = GetClient())
            {
                JsonSerializerSettings settings = new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                };

                settings.Converters.Add(new StringEnumConverter(true));

                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(replyMessage, settings),
                    Encoding.UTF8, "application/json");
                var result = await client.PostAsync(replyEndpoint, content);

                if (result.IsSuccessStatusCode)
                    return;
                else
                    throw new Exception(await result.Content.ReadAsStringAsync());
                //200 OK Request successful
                //400 Bad Request Problem with the request
                //401 Unauthorized Valid Channel access token is not specified
                //403 Forbidden Not authorized to use the API.Confirm that your account or plan is authorized to used the API. 
                //429 Too Many Requests Exceeded the rate limit for API calls
                //500 Internal Server Error Error on the internal server

            }
        }

        /// <summary>
        /// https://devdocs.line.me/en/#push-message
        /// Send messages to a user, group, or room at any time.
        /// INFO Use of the Push Message API is limited to certain plans.
        /// </summary>
        /// <param name="pushMessage"></param>
        public async Task PushAsync(PushMessage pushMessage)
        {
            using (HttpClient client = GetClient())
            {
                JsonSerializerSettings settings = new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                };

                settings.Converters.Add(new StringEnumConverter(true));

                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(pushMessage, settings),
                    Encoding.UTF8, "application/json");
                var result = await client.PostAsync(pushEndpoint, content);
                if (result.IsSuccessStatusCode)
                    return;
                else
                    throw new Exception(await result.Content.ReadAsStringAsync());
                //200 OK Request successful
                //400 Bad Request Problem with the request
                //401 Unauthorized Valid Channel access token is not specified
                //403 Forbidden Not authorized to use the API.Confirm that your account or plan is authorized to used the API. 
                //429 Too Many Requests Exceeded the rate limit for API calls
                //500 Internal Server Error Error on the internal server
            }
        }

        /// <summary>
        /// https://devdocs.line.me/en/#multicast
        /// Send messages to multiple users at any time
        /// </summary>
        /// <param name="to">IDs of the receivers. Max: 150 users. Use IDs returned via the webhook event of source users. IDs of groups or rooms cannot be used. Do not use the LINE ID found on the LINE app. </param>
        /// <param name="messages"> Messages Max: 5 </param>
        public async Task MulticastAsync(List<string> to, List<Message> messages)
        {
            using (HttpClient client = GetClient())
            {
                JsonSerializerSettings settings = new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                };

                settings.Converters.Add(new StringEnumConverter(true));
                MulticastMessage message = new MulticastMessage()
                {
                    To = to,
                    Messages = messages
                };

                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(message, settings),
                    Encoding.UTF8, "application/json");
                var result = await client.PostAsync(multicastEndpoint, content);
                if (result.IsSuccessStatusCode)
                    return;
                else
                    throw new Exception(await result.Content.ReadAsStringAsync());
                //200 OK Request successful
                //400 Bad Request Problem with the request
                //401 Unauthorized Valid Channel access token is not specified
                //403 Forbidden Not authorized to use the API.Confirm that your account or plan is authorized to used the API. 
                //429 Too Many Requests Exceeded the rate limit for API calls
                //500 Internal Server Error Error on the internal server
            }
        }

        /// <summary>
        /// https://devdocs.line.me/en/#content
        /// Retrieve image, video, and audio data sent by users.
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public async Task<Media> GetContent(string messageId)
        {
            using (HttpClient client = GetClient())
            {
                var result = await client.GetAsync(string.Format(contentEndpoint, messageId));
                if (result.IsSuccessStatusCode)
                {
                    return new Media()
                    {
                        Content = await result.Content.ReadAsStreamAsync(),
                        ContentType = result.Content.Headers.ContentType.MediaType,
                        FileName = result.Content.Headers.ContentDisposition != null ? 
                        result.Content.Headers.ContentDisposition.FileName.Replace("\"", "") : 
                        Guid.NewGuid().ToString()
                    };
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// https://devdocs.line.me/en/#bot-api-get-profile
        /// Get user profile information.
        /// </summary>
        /// <param name="mid">User MID</param>
        /// <returns></returns>
        public async Task<Profile> GetProfile(string mid)
        {
            using (HttpClient client = GetClient())
            {
                var result = await client.GetAsync(string.Format(profileEndpoint, mid));
                if (!result.IsSuccessStatusCode)
                    return null;

                Profile profiles = JsonConvert.DeserializeObject<Profile>(await result.Content.ReadAsStringAsync());

                return profiles;
            }
        }

        /// <summary>
        /// https://devdocs.line.me/en/#leave
        /// Leave a group or room
        /// </summary>
        /// <param name="id">group id or room id</param>
        public async Task Leave(string id)
        {
            using (HttpClient client = GetClient())
            {
                var result = await client.GetAsync(string.Format(leaveEndpoint, id));
                if (result.IsSuccessStatusCode)
                    return;
                else
                    throw new Exception(await result.Content.ReadAsStringAsync());
                //200 OK Request successful
                //400 Bad Request Problem with the request
                //401 Unauthorized Valid Channel access token is not specified
                //403 Forbidden Not authorized to use the API.Confirm that your account or plan is authorized to used the API. 
                //429 Too Many Requests Exceeded the rate limit for API calls
                //500 Internal Server Error Error on the internal server
            }
        }

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

            return client;
        }
    }
}
