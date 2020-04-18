# RestApiTestExercise
C# Rest Automation Exercise

Using MsTest, SpecFlow and HttpClient to develop the automation tests.


# Observations:
1. No Security : No Basic/OAuth is implemented . API is not secured at all.Http port is active.
2. Results are returned in Latin language. Assuming that this api will be consumed within UK , results should be translated. Preferably by    setting the "Accept-Language" should be able to return the results in appropriate Language.
3. Will be good if multiple media types are supported. At the moments  "application/json" is supported.
4. Escape characters are being returned as is in the body property of the reposne. Ex: " quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto"

