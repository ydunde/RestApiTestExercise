using System;
using System.Collections.Generic;
using System.Net.Http;

namespace RestApiTestExercise.Helpers
{
    class HttpHelper
    {
        private static HttpClient _httpClient;

       

        public HttpHelper(string baseUrl)
        {
            
            GetClient(baseUrl);
        }
        public HttpClient GetClient(string baseUrl)
        {
            if (_httpClient == null)
                _httpClient = new HttpClient() { BaseAddress = new Uri(baseUrl) };

            return _httpClient;

        }

        public HttpResponseMessage SendAsync(Dictionary<string, string> headers, string httpMethod, HttpContent body, string resourceUri)
        {
           var request = new HttpRequestMessage();

            switch (httpMethod.ToUpper())
            {

                case "GET":
                    request.Method = HttpMethod.Get; break;
                case "POST":
                    request.Method = HttpMethod.Post; break;
                case "PUT":
                    request.Method = HttpMethod.Put; break;
                case "DELETE":
                    request.Method = HttpMethod.Delete; break;
                case "HEAD":
                    request.Method = HttpMethod.Head; break;
                default:
                    break;
            }
            AddJsonContentHeaders(request);
            request.RequestUri = new Uri(resourceUri, UriKind.Relative);

            // Adding any additional headers required
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }

            if (httpMethod.ToUpper() == "POST" || httpMethod.ToUpper() == "PUT")
            {
                request.Content = body;
            }
            return _httpClient.SendAsync(request).Result;
        }

        private void AddJsonContentHeaders(HttpRequestMessage request)
        {
         
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("ContentType", "application/json");
            request.Headers.Add("Accept-Language", "en-gb");
        }

    }
}
