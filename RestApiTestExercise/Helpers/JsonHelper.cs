using System.IO;
namespace RestApiTestExercise.Helpers
{

    public static class JsonHelper

        {

        public static string ReadJsonRequestFromFile(string filePath)
        {

            using StreamReader stream = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(filePath));
            return stream.ReadToEnd();

        }

    }




    



}
