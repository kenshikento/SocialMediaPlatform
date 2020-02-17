using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using SocialMediaPlatform.Models.Spotify;
using Newtonsoft.Json;

namespace SocialMediaPlatform.Support.Helpers
{
    public class AuthSpot
    {
        public static string CLIENT_ID = "add ur own";
        public static string CLIENT_SECRET = "add ur own";
        public static string REDIRECT_URI = "";

        private string GetAuthHeader() => $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes(CLIENT_ID + ":" + CLIENT_SECRET))}";

        public async Task<SpotifyToken> GetToken(Dictionary<string,string> args)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", GetAuthHeader());
            HttpContent content = new FormUrlEncodedContent(args);

            HttpResponseMessage resp = await client.PostAsync("https://accounts.spotify.com/api/token", content);
            string msg = await resp.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<SpotifyToken>(msg);
        }

        public async Task<SpotifyToken> ActivateToken ()
        {
            var args = new Dictionary<string, string>()
            {
                ["grant_type"] = "client_credentials"
            };

            return await GetToken(args);
        }
    }
}