using System;
using System.Collections.Generic;
using System.Net.Http;

namespace RestApiTestExercise.Helpers
{
    class HttpHelper
    {
        private static HttpClient _httpClient;

        HttpRequestMessage request;

        public HttpHelper(string baseUrl)
        {
            request = new HttpRequestMessage();
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

            request.RequestUri = new Uri(resourceUri, UriKind.Relative);
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



    }
}
