using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SocialMediaPlatform.Support.Helpers;
using SocialMediaPlatform.Models.Spotify;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using SocialMediaPlatform.Models;

namespace SocialMediaPlatform.Support
{
    public class SpotifyService
    {
        public const string BaseUrl = "https://api.spotify.com/v1";

        private async Task<SpotifyToken> GetAuthHeader()
        {
            var instance = new Helpers.AuthSpot();
            SpotifyToken token = await instance.ActivateToken();
            return token;
        }

        public async Task<SpotifyPlaylist> GetPlayList()
        {
            SpotifyToken tokenfile = await GetAuthHeader();
            string token = tokenfile.AccessToken;

            StringBuilder builder = new StringBuilder("Bearer " + token);
            token = builder.ToString();

           
            //NEED TO ADD CHECK FOR EXPIRED TOKEN just example playlist used
            string playlistUrl = GetPlayListUrl("1YVfgqp3GsQrI47CXzWiRd");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization",token);

            HttpResponseMessage resp = await client.GetAsync(playlistUrl);
            string msg = await resp.Content.ReadAsStringAsync();
            //System.Diagnostics.Debug.WriteLine(msg);
            return JsonConvert.DeserializeObject<SpotifyPlaylist>(msg);

        }

        public string GetPlayListUrl(string playlistID, string loc = "", int limit = 50)
        {
            limit = Math.Min(50, limit);
            StringBuilder builder = new StringBuilder(BaseUrl + "/playlists/" + playlistID);
            builder.Append("?limit=" + limit);
            return builder.ToString();
        }

       
    }
}