using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using SocialMediaPlatform.Support.Helpers;


namespace SocialMediaPlatform.Support
{ 
    public class YoutubeService
    {
        public string Creds()
        {  
            return "Change it"; // Change this to yours
        }

        static readonly HttpClient client = new HttpClient();

        static async Task<string> BaseCall(string type, Dictionary<string, string> par)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                string baseUrl = "https://www.googleapis.com/youtube/v3/";

                var param = new Helpers.QueryUriBuilder();

                foreach (KeyValuePair<string, string> data in par)
                {
                    param.Add(data.Key, data.Value);
                }

                string Url = baseUrl + type + "?" + param;
        
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, Url);
                requestMessage.Headers.Add("Accept", "application / json");
                HttpResponseMessage response = await client.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                return responseBody;
            }
            catch (HttpRequestException e) 
            {
                return "Returned Nothing or call terribly wrong" + e;
            }
        }

        public async Task<object> getPlayList(string playListID)
        {
            string apiKey = Creds();
            var parm = new Dictionary<string, string>();
            parm.Add("part","snippet");
            parm.Add("playlistId", playListID);
            parm.Add("key", apiKey);
            string type = "playlistItems";

            object response = await BaseCall(type, parm);
            // TODO: Need to add Cache into response
            return response;
        }

    }
}