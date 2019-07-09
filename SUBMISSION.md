# Summary
* This project was done using n-layer architecture in C# (.Net Framework 4.6.1) and has a solution file that can be opened by Visual Studio 2017.
* There are 3 layers: WebAPI, Business and Database, each one has a project. 
* There is also a UnitTest project and it uses Moq to mock data.

# Running WebAPI
1. Inside Folder GuestlogixAPI, open the GuestLogixAPI.sln
2. Inside Visual Studio, build the solution in order to download all required packages.
3. After building successfully, press F5 to run the project.
4. It will open a page on localhost:5000.
5. To create a request, you can use PostMan. Here is an example: http://localhost:5000/api/route?origin=YYZ&destin=YVR

# Running UnitTest
1. After building the solution like previous steps, go to Test -> Windows -> Test Explorer
2. Run all tests.
