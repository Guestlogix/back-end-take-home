# Summary
* This project was using architecture C# and.Net Framework 4.7.2.
* The projects is splitting by projects (Service, Domain, Infra and Test).
* In the Test is been used xUnit and loaded the data using the files in folder "3 - Infra/Data/test".

# Running WebAPI
1. In the solution go until folder 1 - Service, you'll find the project with name Routes.Api, set the project how default.
2. Please open Manage Nuget Package and download all missing packages.
3. Run the project.
4. It'll open Swagger's page.
5. Click in Routes and after click in link "GET /routes/get-route-origin-destination".
6. Fill the fileds and click Try it out!
5. You can try too: http://localhost:28744/routes/get-route-origin-destination?origin=YYZ&destination=YVR

# Running Test
1. You can see the test in the folder 4 - Test/Routes.Test
2. Go until the folder Repositories and open the file FlightRepositoryTest.
3. Rull All Test.

#Extra
* The project is possible find N possibility to route.
* You can define how many maximum stops you want to configure in the project, by default is 3.
* The result is order by Stop and Kilometers.