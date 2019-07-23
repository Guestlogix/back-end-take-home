# Summary
* This application uses C# and.Net Framework 4.6.1.
* This application is partitioned into three C# projects: FlightRouteApi(main), FlightRouteApi.Library(class library), FlightRouteApi.Tests(unit tests).

# How to Run
1. Open Visual Studio as administrator.
2. Open the solution by selecting "FlightRouteApi.sln".
3. In Solution Explorer, set FlightRouteApi as the start up project.
4. Run FlightRouteApi by click "Local IIS" button.
5. A web page will show up, it is the boilerplate web page from Visual Studio. Feel free to ignore it. 
6. Enter this in browser's address bar to see the result: http://localhost/FlightRouteApi/api/routes?origin=yyz&destination=yvr

# Note for Running Unit Tests
* If this application was downloaded as a zip file, Windows will likely block running the tests due to security. 
* Prior to unzipping, right click the zip file -> General tab -> Security -> check the "Unblock" box -> OK