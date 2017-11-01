using LineMessagingAPISDK.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
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
        const string groupMemberProfileEndpoint = "https://api.line.me/v2/bot/group/{0}/member/{1}";
        const string roomMemberProfileEndpoint = "https://api.line.me/v2/bot/room/{0}/member/{1}";
        const string groupMemberIdsEndpoint = "https://api.line.me/v2/bot/group/{0}/members/ids?start={1}";
        const string roomMemberIdsEndpoint = "https://api.line.me/v2/bot/room/{0}/members/ids?start={1}";
        const string richMenuEndpoint = "https://api.line.me/v2/bot/richmenu";
        const string richMenuImageEndpoint = "https://api.line.me/v2/bot/richmenu/{0}/content";
        const string richMenuIdForUserEndpoint = "https://api.line.me/v2/bot/user/{0}/richmenu";


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
            if (to.Count > 150)
                throw new Exception("Max: 150 users");
            if (messages.Count > 5)
                throw new Exception("Max: 5 Messages");

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

        /// <summary>
        /// https://developers.line.me/en/docs/messaging-api/reference/#get-group-member-profile
        /// Get group member profile
        /// </summary>
        /// <param name="groupId">group id</param>
        /// <param name="memberId">member id</param>
        /// <returns></returns>
        public async Task<Profile> GetGroupMemberProfile(string groupId, string memberId)
        {
            using (HttpClient client = GetClient())
            {
                var result = await client.GetAsync(string.Format(groupMemberProfileEndpoint, groupId, memberId));
                if (!result.IsSuccessStatusCode)
                    return null;
                else
                {
                    Profile profile = JsonConvert.DeserializeObject<Profile>(await result.Content.ReadAsStringAsync());
                    return profile;
                }                
            }
        }

        /// <summary>
        /// https://developers.line.me/en/docs/messaging-api/reference/#get-room-member-profile
        /// Get room member profile
        /// </summary>
        /// <param name="roomId">room id</param>
        /// <param name="memberId">member id</param>
        /// <returns></returns>
        public async Task<Profile> GetRoomMemberProfile(string roomId, string memberId)
        {
            using (HttpClient client = GetClient())
            {
                var result = await client.GetAsync(string.Format(roomMemberProfileEndpoint, roomId, memberId));
                if (!result.IsSuccessStatusCode)
                    return null;
                else
                {
                    Profile profile = JsonConvert.DeserializeObject<Profile>(await result.Content.ReadAsStringAsync());
                    return profile;
                }
            }
        }

        /// <summary>
        /// https://developers.line.me/en/docs/messaging-api/reference/#get-group-member-user-ids
        /// Get group member user IDs
        /// </summary>
        /// <param name="groupId">group id</param>
        /// <param name="start">continuationToken</param>
        /// <returns></returns>
        public async Task<MemberIdsResponse> GetGroupMemberIds(string groupId, string start = "")
        {
            using (HttpClient client = GetClient())
            {
                var result = await client.GetAsync(string.Format(groupMemberIdsEndpoint, groupId, start));
                if (!result.IsSuccessStatusCode)
                    return null;
                else
                {
                    var groupMembersResponse = JsonConvert.DeserializeObject<MemberIdsResponse>(await result.Content.ReadAsStringAsync());
                    return groupMembersResponse;
                }
            }
        }

        /// <summary>
        /// https://developers.line.me/en/docs/messaging-api/reference/#get-room-member-user-ids
        /// Get room member user IDs
        /// </summary>
        /// <param name="roomId">room id</param>
        /// <param name="start">continuationToken</param>
        /// <returns></returns>
        public async Task<MemberIdsResponse> GetRoomMemberIds(string roomId, string start = "")
        {
            using (HttpClient client = GetClient())
            {
                var result = await client.GetAsync(string.Format(roomMemberIdsEndpoint, roomId, start));
                if (!result.IsSuccessStatusCode)
                    return null;
                else
                {
                    var groupMembersResponse = JsonConvert.DeserializeObject<MemberIdsResponse>(await result.Content.ReadAsStringAsync());
                    return groupMembersResponse;
                }
            }
        }

        /// <summary>
        /// Gets a rich menu via a rich menu ID.
        /// https://developers.line.me/en/docs/messaging-api/reference/#get-rich-menu
        /// </summary>
        /// <param name="richMenuId">RichMenu Id</param>
        /// <returns></returns>
        public async Task<RichMenu> GetRichMenu(string richMenuId)
        {
            using (HttpClient client = GetClient())
            {
                var result = await client.GetAsync(richMenuEndpoint + $"/{richMenuId}");
                if (!result.IsSuccessStatusCode)
                    return null;
                else
                {
                    var richMenu = JsonConvert.DeserializeObject<RichMenu>(await result.Content.ReadAsStringAsync());
                    return richMenu;
                }
            }
        }

        /// <summary>
        /// Creates a rich menu. 
        /// https://developers.line.me/en/docs/messaging-api/reference/#create-rich-menu
        /// Note: You must upload a rich menu image and link the rich menu to a user for the rich menu to be displayed.You can create up to 10 rich menus for one bot.
        /// </summary>
        /// <param name="richMenu">RichMenu object</param>
        /// <returns></returns>
        public async Task CreateRechMenu(RichMenu richMenu)
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
                    JsonConvert.SerializeObject(richMenu, settings),
                    Encoding.UTF8, "application/json");
                var result = await client.PostAsync(richMenuEndpoint, content);
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
        /// Deletes a rich menu.
        /// https://developers.line.me/en/docs/messaging-api/reference/#delete-rich-menu
        /// </summary>
        /// <param name="richMenuId">RichMenu Id</param>
        /// <returns></returns>
        public async Task DeleteRichMenu(string richMenuId)
        {
            using (HttpClient client = GetClient())
            {
                var result = await client.DeleteAsync(richMenuEndpoint + $"/{richMenuId}");
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
        /// Gets the ID of the rich menu linked to a user.
        /// https://developers.line.me/en/docs/messaging-api/reference/#get-rich-menu-id-of-user
        /// </summary>
        /// <param name="richMenuId">RichMenu Id</param>
        /// <returns></returns>
        public async Task<string> GetRichMenuIdForUser(string userId)
        {
            using (HttpClient client = GetClient())
            {
                var result = await client.GetAsync(string.Format(richMenuIdForUserEndpoint, userId));
                if (!result.IsSuccessStatusCode)
                    return null;
                else
                {
                    var richMenu = JsonConvert.DeserializeObject<RichMenu>(await result.Content.ReadAsStringAsync());
                    return richMenu.RichMenuId;
                }
            }
        }

        /// <summary>
        /// Links a rich menu to a user.
        /// https://developers.line.me/en/docs/messaging-api/reference/#link-rich-menu-to-user
        /// Note: Only one rich menu can be linked to a user at one time
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="richMenuId">RichMenu Id</param>
        /// <returns></returns>
        public async Task LinkRichMenuToUser(string userId, string richMenuId)
        {
            using (HttpClient client = GetClient())
            {
                var result = await client.PostAsync(string.Format(richMenuIdForUserEndpoint, userId) + $"/{richMenuId}", null);
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
        /// Unlinks a rich menu from a user.
        /// https://developers.line.me/en/docs/messaging-api/reference/#unlink-rich-menu-from-user
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns></returns>
        public async Task UnlinkRichMenuToUser(string userId)
        {
            using (HttpClient client = GetClient())
            {
                var result = await client.DeleteAsync(string.Format(richMenuIdForUserEndpoint, userId));
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
        /// Downloads an image associated with a rich menu.
        /// https://developers.line.me/en/docs/messaging-api/reference/#download-rich-menu-image
        /// </summary>
        /// <param name="richMenuId">RichMenuId</param>
        /// <returns></returns>
        public async Task<Media> GetRichMenuImage(string richMenuId)
        {
            using (HttpClient client = GetClient())
            {
                var result = await client.DeleteAsync(string.Format(richMenuImageEndpoint, richMenuId));
                if (!result.IsSuccessStatusCode)
                    return null;

                return new Media()
                {
                    Content = await result.Content.ReadAsStreamAsync(),
                    ContentType = result.Content.Headers.ContentType.MediaType,
                    FileName = result.Content.Headers.ContentDisposition != null ?
                        result.Content.Headers.ContentDisposition.FileName.Replace("\"", "") :
                        Guid.NewGuid().ToString()
                };
            }
        }

        /// <summary>
        /// Upload rich menu image
        /// https://developers.line.me/en/docs/messaging-api/reference/#upload-rich-menu-image
        /// Notes: 
        /// Images must have one of the following resolutions: 2500x1686, 2500x843.
        /// You cannot replace an image attached to a rich menu.To update your rich menu image, create a new rich menu object and upload another image.
        /// </summary>
        /// <param name="richMenuId">RichMenu Id</param>
        /// <returns></returns>
        public async Task UploadRichMenuImage(string richMenuId, Stream image)
        {
            using (HttpClient client = GetClient())
            {
                var result = await client.PostAsync(string.Format(richMenuImageEndpoint, richMenuId), new StreamContent(image));
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
        /// Gets a list of all uploaded rich menus.
        /// https://developers.line.me/en/docs/messaging-api/reference/#get-rich-menu-list
        /// </summary>
        /// <returns></returns>
        public async Task<List<RichMenu>> GetRichMenuList()
        {
            using (HttpClient client = GetClient())
            {
                var result = await client.GetAsync(richMenuEndpoint + "/list");
                if (!result.IsSuccessStatusCode)
                    return null;
                else
                {
                    var richMenus = JsonConvert.DeserializeObject<List<RichMenu>>(((JToken.Parse(await result.Content.ReadAsStringAsync())["richmenus"]) as JArray).ToString());
                    return richMenus;
                }
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
