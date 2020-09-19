using DnsClient.Internal;
using Microsoft.Extensions.Logging;
using MorningFM.Logic.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MorningFM.Logic
{
    public class HttpHandler
    {
        private Microsoft.Extensions.Logging.ILogger _logger;
        private HttpClient _httpClient; 
        public HttpHandler(Microsoft.Extensions.Logging.ILogger<HttpHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException("Logger not provided.");
            _httpClient = new HttpClient();
        }

        public async Task<T> Get<T>(string accessToken, string request)
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, request);            

            var response = await _httpClient.SendAsync(httpRequest);           
            var payload = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpHandlerException($"Http response unsuccessful. Status: {response.StatusCode} Message: {payload}");
            }

            return JsonConvert.DeserializeObject<T>(payload);
        }

        public async Task<T> Post<T>(string accessToken, string request, string bodyPayload)
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, request);
            httpRequest.Content = new StringContent(bodyPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(httpRequest);
            var payload = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpHandlerException($"Http response unsuccessful. Status: {response.StatusCode} Message: {payload}");
            }

            return JsonConvert.DeserializeObject<T>(payload);
        }

        public async Task<HttpStatusCode> Post(string accessToken, string request, string bodyPayload)
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, request);
            httpRequest.Content = new StringContent(bodyPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(httpRequest);
            var payload = await response.Content.ReadAsStringAsync();


            if (!response.IsSuccessStatusCode)
            {
                throw new HttpHandlerException($"Http response unsuccessful. Status: {response.StatusCode} Message: {payload}");
            }

            return response.StatusCode; 
        }
    }
}
