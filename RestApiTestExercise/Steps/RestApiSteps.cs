using FluentAssertions;
using Newtonsoft.Json.Linq;
using RestApiTestExercise.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace RestApiTestExercise.Steps
{
    [Binding]
    public class RestApiSteps

    {
        private readonly ScenarioContext _scenarioContext;
        private HttpHelper _restHelper;
        private string body;
        private readonly RequestBodyBuilder _requestBodyBuilder;
        private readonly Dictionary<string, string> _headers;
        private readonly ResponseHandler responseHandler;
        public RestApiSteps(ScenarioContext scenarioContext)
        {

            _scenarioContext = scenarioContext;
            _requestBodyBuilder = new RequestBodyBuilder();
            _headers = new Dictionary<string, string>();
            responseHandler = new ResponseHandler(_scenarioContext);


        }

        [Given(@"I have valid endpoint")]
        public void GivenIHaveValidEndpoint()
        {
            _restHelper = new HttpHelper(_scenarioContext.Get<string>("baseApiURL"));
        }

        [When(@"I do a ""(.*)"" request for resource ""(.*)""")]
        public void WhenIDoARequestForResource(string httpMethod, string resourceUri)
        {


            _headers.Add("Accept", "application/json");
            _headers.Add("ContentType", "application/json");

            body = _requestBodyBuilder.Build(httpMethod, "requestBody.json");

            var response = _restHelper.SendAsync(_headers, httpMethod, new StringContent(body), resourceUri);

            responseHandler.Handle(response);
        }


        [Then(@"I should receive the status code as ""(.*)""")]
        public void ThenIShouldReceiveTheStatusCodeAs(int statusCode)
        {

            _scenarioContext.Get<int>("statusCode").Should().Be(statusCode, $"Successful api request should have successStatusCode as {statusCode}");

        }

        [Then(@"I should receive the success response")]
        public void TheIShouldReceiveTheSuccessResponse()
        {

            _scenarioContext.Get<bool>("isSuccessStatusCode").Should().Be(true, $"Successful api request should have isSuccessStatusCode as true");

        }
        [Then(@"I should expect to see below details in response")]
        public void ThenIShouldExpectToSeeBelowDetailsInResponse(Table table)
        {
            var responseMessage = _scenarioContext.Get<JToken>("httpResponseMessage");
            foreach (var row in table.Rows)
            {
                foreach (var column in row)
                {
                    var expectedValue = column.Value;
                    var actualValue = responseMessage[column.Key].ToString();
                    actualValue.Should().Be(expectedValue);
                }
            }
        }
        [Then(@"I should have ""(.*)"" as ""(.*)"" in comments")]
        public void ThenIShouldHaveAsInComments(string propertyName, string propertyValue)
        {
            var responseMessage = _scenarioContext.Get<JToken>("httpResponseMessage");
            foreach (var jsonProperty in responseMessage.Children())
            {
                Console.WriteLine($"{propertyName}:{jsonProperty[propertyName].ToString()}");
                jsonProperty[propertyName].ToString().Should().Be(propertyValue);
            }
        }
    }
}
    

    

