using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace RestApiTestExercise.Helpers
{
    class ResponseHandler
    {
        private ScenarioContext _scenarioContext;
        public ResponseHandler(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        /// <summary>
        /// Handle will read all the http  properies into scenario context
        /// </summary>
        /// <param name="response"></param>
        public void Handle(HttpResponseMessage response)
        {



            foreach (var headerItem in response.Headers)
            {

                IEnumerable<string> values;
                string HeaderItemValue = "";
                values = response.Headers.GetValues(headerItem.Key.ToString());

                foreach (var valueItem in values)
                {
                    HeaderItemValue = HeaderItemValue + valueItem + ";";
                }

                Console.WriteLine(headerItem + " : " + HeaderItemValue);
                _scenarioContext.Set(HeaderItemValue, headerItem.Key);
            }
          
            Console.WriteLine($"ReasonPhrase:{response.ReasonPhrase}");
            Console.WriteLine($"ResponseMessage:{JToken.Parse(response.Content.ReadAsStringAsync().Result)}");
            _scenarioContext.Set(response.ReasonPhrase, "reasonPhrase");
            _scenarioContext.Set(JToken.Parse(response.Content.ReadAsStringAsync().Result), "httpResponseMessage");
            _scenarioContext.Set(response.IsSuccessStatusCode, "isSuccessStatusCode");
            _scenarioContext.Set((int)response.StatusCode, "statusCode");

        }

    }
}
