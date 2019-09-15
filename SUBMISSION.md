# Guestlogix Take Home Test - Backend - Submission

This solution has been created as an ASP.NET WebAPI project in C#, using [Visual Studio Community 2019](https://visualstudio.microsoft.com/downloads/), and deployed to Microsoft Azure App Services.

For testing purposes, I used the [Advanced REST Client (ARC)](https://install.advancedrestclient.com/) tool to create the required GET request to both the localhost and Azure endpoints.

## Design decisions

I considered the following points while designing the solution:

- Created C# classes for Airport, Airline, and Route objects in the Models folder. Following the ASP.NET standard.
- At the moment, I am ignoring the Airline class.
- Airport and Route classes include methods to load the data from either the test or full datasets. This allows the GET request to be configurable for testing purposes.
- The main search algorithm is a variation of the [Breadth First Search](https://www.geeksforgeeks.org/breadth-first-search-or-bfs-for-a-graph/). I decided to think of the Routes as a Graph. And as I iterate through the connected nodes, I am keeping track of the airports that have been visited already, in order to ignore them and avoid creating loops.
- HomeController.cs can be ignored since it is an ASP.NET MVC controller and is only used for showing a landing page when running the project.
- FlightRoutesController.cs, in the Controllers folder, contains the API and main algorithm.
- Both datasets have been uploaded inside the data folder. Under full or test, respectivelly.

## Testing on a local machine

The project can be cloned and run using Visual Studio 2019. This will expose a localhost endpoint that looks like the following:

- https://localhost:44326/

## Testing on Microsoft Azure App Services

The project has been deployed to a WebApp container in Azure. The following is the endpoint:

- https://flightsweb.azurewebsites.net/

## GET api/FlightRoutes/GetConnections?origin={origin}&destination={destination}&full={full}

To create a request, the client must append the following parameters to the URL:

| Parameter   | Description                                                          |Example|
|-------------|----------------------------------------------------------------------|-------|
| origin      | Specifies the origin airport 3 digit code                            | YYZ   |
| destination | Specifies the destination airport 3 digit code                       | YVR   |
| full        | Indicates whether to use the full (true) or the test (false) dataset | FALSE |

### GET Request examples:

To get the shortest route from YYZ to ORD using the testing dataset, do a GET to:
https://flightsweb.azurewebsites.net/api/FlightRoutes/GetConnections?origin=YYZ&destination=ORD&full=false

Expected result: "There are no flights from YYZ to ORD. Choose a different origin, or destination and try again."

To get the shortest route from YYZ to ORD using the full dataset, do a GET to:
https://flightsweb.azurewebsites.net/api/FlightRoutes/GetConnections?origin=YYZ&destination=ORD&full=true

Expected result: "Found shortest path from YYZ to ORD. Route: YYZ > ATL > ORD"