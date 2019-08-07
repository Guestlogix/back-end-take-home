# Overall Info

For solving this task I decided to use ASP.Net Core 2.2 application and Dijkstra algorithm.
I have also added distance between airports by geolocation as weight to the calculation.

### How to run it

As a .Net Core application it can be runned by simple command `...back-end-take-home\FastestRouteCalculator\FastestRouteCalculator> dotnet run`

Or you can use `docker run` command with default setup

### How to test it

After application is up you can call `...api/routes/{ORIGIN}/{DESTINCATION}` endpoint (e.g. `http://localhost:5000/routes/JFK/GRU`).
OR You can call hosted application on `https://routecalc.azurewebsites.net/routes/JFK/GRU`
OR You can call api call via Swagger UI `https://routecalc.azurewebsites.net` 

NOTE: First call might take up to 10 seconds because of initial data processing.

### Tests

There are Tests project in my solutio that covers all requested cases:

| Origin | Destination | Expected Result          |
|--------|-------------|--------------------------|
| YYZ    | JFK         | YYZ -> JFK               |
| YYZ    | YVR         | YYZ -> JFK -> LAX -> YVR |
| YYZ    | ORD         | No Route                 |
| XXX    | ORD         | Invalid Origin           |
| ORD    | XXX         | Invalid Destination      |

### Questions

If you have any questions during running or testing - feel free to contact me anytime.