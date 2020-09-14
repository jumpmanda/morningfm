using MorningFM.Logic.DTOs.Spotify;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MorningFM.Logic
{
    public class SpotifyHandler
    {
        private HttpClient _httpClient; 
        private HttpHandler _http; 

        public SpotifyHandler()
        {
            _httpClient = new HttpClient();
            _http = new HttpHandler(); 
        }

        public async Task<string> GetPlaylists(string accessToken)
        {
            var playlistAccessToken = "BQDMXhBY7hGKyAllacMzzywwRfKVUcfQXUHZqt7Y8qYIYOT_wgLyG-ifj4O0fAQ9rriV9ObjjStsaX_gFmKjQ7k8mudCo1rFD7N74uMfBZOlD85m3bFjz7k9NgeC28LEHQh2FJEA5NdlBJtIz8KTityzXRkOdx_aXcJd7Wi07mlRzIHZLTZ4f2tXEPU";
            var request = $@"https://api.spotify.com/v1/me/playlists";

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", playlistAccessToken);

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, request);
            httpRequest.Content = new StringContent("", Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(httpRequest);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<UserProfile> GetUserInfo(string accessToken){
            var request = $@"https://api.spotify.com/v1/me";
            var blob = await _http.Get<UserProfile>(accessToken, request);
            return blob; 
        }

        public async Task<ShowBlob[]> GetCurrentUserSavedShows(string accessToken)
        {
            var request = $@"https://api.spotify.com/v1/me/shows";
            var blob = await _http.Get<SavedShowBlob>(accessToken, request);
            return blob?.Items;
        }

        public async Task<Track[]> GetTopTracks(string accessToken)
        {
            var request = $@"https://api.spotify.com/v1/me/top/tracks";
            var blob = await _http.Get<TrackBlob>(accessToken, request);
            return blob?.Items; 
        }

        public async Task GetRecommendedTracks()
        {
            var request = $@"https://api.spotify.com/v1/recommendations";

        }

    }
}
