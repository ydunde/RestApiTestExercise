namespace RestApiTestExercise.Helpers
{
    class RequestBodyBuilder
    {

        public string Build(string httpMethod, string fileName)
        {
            if (!string.IsNullOrEmpty(fileName)&& httpMethod.ToUpper().Equals("POST") || httpMethod.ToUpper().Equals("PUT"))
                return JsonHelper.ReadJsonRequestFromFile("RestApiTestExercise.TestData." + fileName);
            else
                return "";
        }

    }
}
