# RestApiTestExercise
C# Rest Automation Exercise

Using MsTest, SpecFlow and HttpClient to develop the automation tests.
#Using the Buid Configuraion tests can be pointed to run in QA or UAT by loading the environment specific settings


# Observations:
1. No Security : No Basic/OAuth is implemented . API is not secured at all.HTTP port is active.
2. Results are returned in Latin language. Assuming that this api will be consumed within UK , results should be translated. Preferably    by setting the "Accept-Language" should be able to return the results in appropriate Language.
3. Escape characters are being returned as is in the body property of the reposne. Ex: " quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto"
4. Will be good if multiple media types are supported. At the moment "application/json" is only supported.
5. Resource "posts/0/comments" is returning 200 response code with empty results. Arguably this can be 404. 
6. Performance and Security (injections) not considered for this exercise.

# I would probably will deside not go LIVE with this API for below reasons
    1. No security on this api resources.
    2. API to be consumed in UK. It seems globalisation/location aspects not considered.
    3. Performance of api  is unknown.
    4. Minor issues with escape chars and response codes.

# I would probably will deside to go LIVE with this API for below reasons
    1. Intended for Open use and no security required.
    2. Globalisation/Location is not required and expecting the consumers to handle as per their needs.
    3. Support Multiple MediaTypes not required/mandatory to go give , consumers can handle at their end.
    4. Performace of this application is not a concern.
