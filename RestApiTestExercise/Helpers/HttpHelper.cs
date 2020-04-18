using System;
using System.Collections.Generic;
using System.Net.Http;

namespace RestApiTestExercise.Helpers
{
    class HttpHelper
    {
        private static HttpClient _httpClient;
        private readonly RequestHeaderBuilder requestHeaderBuilder;
     

        public HttpHelper(string baseUrl)
        {
                GetClient(baseUrl);
            requestHeaderBuilder = new RequestHeaderBuilder();
        }

        public void SetBaseUrl(string baseUrl)
        {
            _httpClient.BaseAddress = new Uri(baseUrl);

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

            requestHeaderBuilder.BuildDefaultHeaders(request);
            // Adding any additional headers required
            requestHeaderBuilder.AddAdditionalHeaders(headers, request);

            request.RequestUri = new Uri(resourceUri, UriKind.Relative);

            if (httpMethod.ToUpper() == "POST" || httpMethod.ToUpper() == "PUT")
            {
                request.Content = body;

            }

            return _httpClient.SendAsync(request).Result; ;
        }



    }
}
