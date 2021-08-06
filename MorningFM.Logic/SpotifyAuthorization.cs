using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MorningFM.Logic
{
    [DataContract]
    public class SpotifyAccessBlob
    {
        [DataMember(Name ="access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "token_type")]
        public string TokenType { get; set; }

        [DataMember(Name = "scope")]
        public string Scope { get; set; }

        [DataMember(Name = "expires_in")]
        public int Expiration { get; set; }

        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }
    }

    public class SpotifyAuthorization
    {
        private ILogger<SpotifyAuthorization> _logger; 
        private protected string _clientId { private get; set; }
        private protected string _clientSecret { private get; set; }
        private const string _responseType = "code";
        private readonly string _redirectUri;
        private const string _scope = "user-read-private user-read-email user-library-read user-top-read playlist-modify-public playlist-modify-private user-read-playback-position";
        private HttpClient _httpClient; 

        public SpotifyAuthorization(string clientId, string clientSecret, string redirectUri, ILogger<SpotifyAuthorization> logger)
        {
            if (string.IsNullOrEmpty(clientId)) throw new ArgumentNullException("ClientId was not provided.");
            if (string.IsNullOrEmpty(clientSecret)) throw new ArgumentNullException("ClientSecret was not provided.");
            _logger = logger ?? throw new ArgumentNullException("Logger not provided.");
            _clientId = clientId;
            _clientSecret = clientSecret;
            _redirectUri = redirectUri; 
            _httpClient = new HttpClient();           
        }

        public string GetLoginPage()
        {
            return $@"https://accounts.spotify.com/authorize?client_id={_clientId}&response_type={_responseType}&redirect_uri={_redirectUri}&scope={_scope}";

        }

        public async Task<SpotifyAccessBlob> GetAccessBlob(string code)
        {
            var uri = "https://accounts.spotify.com/api/token"; 

            var plainText = $"{_clientId}:{_clientSecret}";
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var basicAuth = System.Convert.ToBase64String(plainTextBytes);

            var requestParams = new Dictionary<string, string>();
            requestParams["grant_type"] = "authorization_code"; 
            requestParams["code"] = code; 
            requestParams["redirect_uri"] = _redirectUri; 

            HttpContent content = new FormUrlEncodedContent(requestParams);
            content.Headers.ContentType.MediaType = "application/x-www-form-urlencoded"; 
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basicAuth);

            var response = await _httpClient.PostAsync(uri, content);
            var payload =  await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Http response unsuccessful. Status: {response.StatusCode} Message: {payload}");
            }

            return JsonConvert.DeserializeObject<SpotifyAccessBlob>(payload);           

        }

        /// <summary>
        /// Find user current access token to request a refresh to extend timeout
        /// </summary>
        /// <param name="userId"></param>
        public void RefreshAccessToken(string userId)
        {

        }

    }
}
