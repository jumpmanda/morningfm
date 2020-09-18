using MorningFM.Logic.DTOs.Spotify;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MorningFM.Logic
{
    public class SpotifyHandler
    {
        private readonly object listLock = new object();
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

        public async Task<Track[]> GetRecommendedTracks(string accessToken)
        {
            var request = $@"https://api.spotify.com/v1/recommendations?seed_tracks=";
            //Use top tracks to provide seed tracks to recommendation system           

            var trackBlob = await GetTopTracks(accessToken);
            var tracks = trackBlob.Take(5); 
            var res = tracks.Aggregate(
                new StringBuilder(),
                (current, next) => current.Append(current.Length == 0 ? "" : ",").Append(next.Id))
                .ToString();

            request += System.Web.HttpUtility.UrlEncode(res);

            var blob = await _http.Get<RecommendationsBlob>(accessToken, request);
            return blob?.Tracks; 
        }

        public async Task<string> CreateRecommendedPlaylist(string accessToken)
        {
            //get user id 
            var userProfile = await GetUserInfo(accessToken);  

            //create new playlist
            var request = $@"https://api.spotify.com/v1/users/{userProfile.Id}/playlists";
            var blob = await _http.Post<Playlist>(accessToken, request, JsonConvert.SerializeObject(new PlaylistRequest() { Name="MorningFM-Test", PublicMode=true, Description="Here's a playlist with recommended tracks and your latest podcast episodes!" }));

            //add recommended tracks to playlist 
            var addTracksRequest = $"https://api.spotify.com/v1/playlists/{blob.Id}/tracks"; 
            var trackBlob = await GetRecommendedTracks(accessToken);

            var trackList = trackBlob.Select(t => $"spotify:track:{t.Id}").ToArray();

            var isAdded = await AddTrackToPlaylist(accessToken, blob.Id, trackList);

            return blob?.Id; 
            
        }

        public async Task<bool> AddTrackToPlaylist(string accessToken, string playlistId, string trackId, int position)
        {
            var addTracksRequest = $"https://api.spotify.com/v1/playlists/{playlistId}/tracks";
            var status = await _http.Post(accessToken, addTracksRequest, JsonConvert.SerializeObject(new { uris = new string[] { trackId }, position = position }));
            return status == HttpStatusCode.OK || status == HttpStatusCode.NoContent;
        }

        public async Task<bool> AddTrackToPlaylist(string accessToken, string playlistId, string[] trackIds)
        {
            var addTracksRequest = $"https://api.spotify.com/v1/playlists/{playlistId}/tracks";
            var status = await _http.Post(accessToken, addTracksRequest, JsonConvert.SerializeObject(new { uris = trackIds }));
            return status == HttpStatusCode.OK || status == HttpStatusCode.NoContent;
        }

        public async Task<int> AddTrackToPlaylistWithPosition(string accessToken, string playlistId, string[] trackIds)
        {
            var failCount = 0; 
            for(int i = 0; i < trackIds.Length; i++)
            {
                var success = await AddTrackToPlaylist(accessToken, playlistId, trackIds[i], (i + 1) * 2);
                if (success) failCount++; 
            }
            return failCount; 
        }

        public async Task<string[]> GetLatestEpisodes(string accessToken, string[] showIds)
        {
            List<string> episodes = new List<string>();   

           foreach(var showId in showIds)
            {
                var request = $"https://api.spotify.com/v1/shows/{showId}/episodes";
                var payload = await _http.Get<EpisodeBlob>(accessToken, request);
                var results = payload?.Items;
                results.ToList().Sort((x, y) => DateTime.Compare(
                    DateTime.ParseExact(x.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    DateTime.ParseExact(y.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture)));

                //Check release date for most recent episode
                if (results != null)
                {
                    episodes.Add(results[0].Id);
                }
            }

            return episodes.ToArray();
        }

    }
}
