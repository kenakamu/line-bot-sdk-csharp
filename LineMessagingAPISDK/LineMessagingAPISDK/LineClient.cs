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
        const string contentEndpoint = "https://api.line.me/v2/bot/message/{0}/content";
        const string profileEndpoint = "https://api.line.me/v2/bot/profile/{0}";
        public string AccessToken;

        public LineClient(string accessToken)
        {
            this.AccessToken = accessToken;
        }

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

        //public async Task<MediaContent> GetContentPreview(string messageId)
        //{
        //    using (HttpClient client = GetClient())
        //    {
        //        var result = await client.GetAsync(string.Format(contentPreviewEndpoint, messageId));
        //        if (result.IsSuccessStatusCode)
        //        {
        //            return new MediaContent()
        //            {
        //                FileName = result.Content.Headers.ContentDisposition.FileName,
        //                MediaType = result.Content.Headers.ContentType.MediaType,
        //                Content = await result.Content.ReadAsByteArrayAsync()
        //            };
        //        }
        //        else
        //            return null;
        //    }
        //}

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

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

            return client;
        }
    }
}
