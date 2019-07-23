Candidate: Sumair Ali

*How to run*
- .Net Core C# Web API application with Swagger
- It can be deployed to IIS and navigating to `{baseUrl}/swagger` should provide the UI
- By default, API is accessing test data but it can be modified in `RouteCalculator\appsettings.json:RelativeFolderPath`
- Alternatively, it can be launched with Visual Studio too

*Features*
- Separate Repository layer which can be extended in the future to load data 
from SQL for example without any modifications to the Service layer or Controller
- Service layer houses the logic to calculate the shortest route
- Service layer uses caching to avoid repetitive data loads
- Controller provides route results as JSON with success status code 
- Controller provides a simple error message with appropriate status code (e.g. Not Found, Bad request)

- A test project is included. However, ran into some trouble with testing the service due to caching.
For some reason mocking is not working for the caching class. So tests are incomplete.