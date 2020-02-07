﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SocialMediaPlatform.Models;

namespace SocialMediaPlatform.Support
{
    public class YoutubeService
    {
        public string Creds()
        {  
            return "Test"; // Change this to yours
        }

        static readonly HttpClient client = new HttpClient();

        static async Task<string> BaseCall(string type, Dictionary<string, string> par, string body = "", string baseUrl = "https://www.googleapis.com/youtube/v3/")
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                var param = new Helpers.QueryUriBuilder();
                
                foreach (KeyValuePair<string, string> data in par)
                {
                    param.Add(data.Key, data.Value);
                }

                string Url = "";

                if (!baseUrl.Contains("?"))
                {
                    Url = baseUrl + type + "?" + param;
                }

                if(par.ContainsKey("pageToken"))
                {
                    string token = par.FirstOrDefault(x => x.Key == "pageToken").Value;
                    System.Diagnostics.Debug.WriteLine(token);
                    String[] spearator = { "&" };
                    Int32 count = 5;
                    //URL TOKEN
                    string[] strlist = baseUrl.Split(spearator, count, StringSplitOptions.RemoveEmptyEntries);
                    
                    foreach (String s in strlist)
                    {
                        if(s.Contains(token))
                        {
                            return body;
                        }

                    }

                    Url = baseUrl +  "&" + param;
                }

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, Url);
                requestMessage.Headers.Add("Accept", "application / json");
                HttpResponseMessage response = await client.SendAsync(requestMessage);
                int responseMessages;

                if (!response.IsSuccessStatusCode) // Needs figure it out casting it straight string as method expects STRING
                {
                    responseMessages = (int)response.StatusCode;
                    return "" + responseMessages;
                }

                dynamic responseBody = JObject.Parse(await response.Content.ReadAsStringAsync());            
                JArray responseArray = responseBody["items"];
                string responseBodyString = responseBody.ToString();
                

                if(responseBody.ContainsKey("nextPageToken"))
                {

                    string nextPageToken = "";

                    Dictionary<string,object> parseJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseBodyString);
                           
                    foreach (KeyValuePair<string, object> data in parseJson)
                    {
                        if(data.Key.Contains("nextPageToken"))
                        {
                            nextPageToken = (string)data.Value;             
                        }
                    }

                    if (baseUrl.Contains(nextPageToken))
                    {
                        return responseBodyString;
                    }

                    var token = new Dictionary<string, string>();
                    token.Add("pageToken", nextPageToken);

                    JObject extendedJson = JObject.Parse(await BaseCall("playlistItems", token, responseBodyString, Url));
                    JArray itemExtended = (JArray)extendedJson["items"];

                    responseArray.Merge(itemExtended, new JsonMergeSettings
                    {
                        MergeArrayHandling = MergeArrayHandling.Union
                    });

                    return responseArray.ToString();
                }

                return responseBody.ToString();
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