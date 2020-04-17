using Newtonsoft.Json.Linq;
using RestApiTestExercise.Helpers;
using TechTalk.SpecFlow;

namespace RestApiTestExercise.Steps
{
    [Binding]
    class TestSetupSteps
    {
        private ScenarioContext _scenarioContext;

        public TestSetupSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        [BeforeScenario]
        public void TestSetup()
        {
            // This setup is to point at different environments using configuration.
            var fileSuffix = "";
#if UAT
            {
                fileSuffix = "UAT.json";
            }
#endif

#if QA
            {
                fileSuffix = "QA.json";
            }
#endif

            var environmentPropertiesJson = JsonHelper.ReadJsonRequestFromFile("RestApiTestExercise." + fileSuffix);

            var a = JObject.Parse(environmentPropertiesJson);

            foreach (JProperty item in (JToken)a)
            {
                _scenarioContext.Set(item.Value.ToString(), item.Name.ToString());
            }

        }
    }
}
