using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace RestApiTestExercise.Helpers
{
    class RequestHeaderBuilder
    {
        public HttpRequestMessage BuildDefaultHeaders(HttpRequestMessage request)
        {

            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Accept-Language", "en-gb");
            return request;

        }

        public HttpRequestMessage AddAdditionalHeaders(Dictionary<string, string> headers, HttpRequestMessage request)
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
            return request;
        }
    }

}
