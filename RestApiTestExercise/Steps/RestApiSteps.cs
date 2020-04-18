using FluentAssertions;
using Newtonsoft.Json.Linq;
using RestApiTestExercise.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [Given(@"I do a ""(.*)"" request for resource ""(.*)""")]
        public void WhenIDoARequestForResource(string httpMethod, string resourceUri)
        {



            body = _requestBodyBuilder.Build(httpMethod, "");

            var response = _restHelper.SendAsync(_headers, httpMethod, new StringContent(body), resourceUri);

            responseHandler.Handle(response);
        }


        [Then(@"I should receive the status code as ""(.*)""")]
        [Given(@"I should receive the status code as ""(.*)""")]
        public void ThenIShouldReceiveTheStatusCodeAs(int statusCode)
        {

            _scenarioContext.Get<int>("statusCode").Should().Be(statusCode, $"Request should have StatusCode as {statusCode}");

        }

        [Then(@"I should receive the success state as ""(.*)""")]
        public void TheIShouldReceiveTheSuccessResponse(bool successState)
        {

            _scenarioContext.Get<bool>("isSuccessStatusCode").Should().Be(successState, $"Api request should have isSuccessStatusCode as {successState}");

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

        [Then(@"I should expect to see ""(.*)"" results in the response")]
        public void ThenIShouldExpectToSeeResultsInTheResponse(int resultsCount)
        {
            var responseMessage = _scenarioContext.Get<JArray>("httpResponseMessage");
            responseMessage.Count.Should().Be(resultsCount);
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

        [Given(@"I should expect to see ""(.*)"" comments in the response")]
        public void GivenIShouldExpectToSeeResultsInTheResponse(int resultsCount)
        {
            var responseMessage = _scenarioContext.Get<JArray>("httpResponseMessage");
            responseMessage.Count.Should().Be(resultsCount);
            _scenarioContext.Set(responseMessage,"comments");
        }
        [Then(@"I should expect to all comments for postId: ""(.*)"" are returned")]
        public void ThenIShouldExpectToAllCommentsForPostIdAreReturned(string postId)
        {
            var allComments = JToken.Parse(_scenarioContext.Get<JToken>("comments").ToString());
            var commentsWithPostIdFromAllComments = allComments.Where(j => j["postId"].Value<string>() == postId).Select(j => j);


            var commentsWithPostId = _scenarioContext.Get<JArray>("httpResponseMessage");

            Console.WriteLine($"PostId: {postId} - comments For single post request{commentsWithPostId.Count} and comments from all comments request{commentsWithPostIdFromAllComments.Count()}");
            commentsWithPostId.Count.Should().Be(commentsWithPostIdFromAllComments.Count());
        }

      

    }
}
    

    

