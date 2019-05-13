**Nathaniel Faries**

This is my submission for the take-home assingment. Unfortunately it's quite late due to prior commitments I had taking up my evenings after receiving the assignment. I told my recruiter about these things but never heard back. At any rate, after far too much finagling with my personal computer's installation of .NET and Visual Studio and more issues that I would have imagined possible, I have cobbled together a solution. It's not my finest work but without scrapping what I've written and starting over yet again I don't think I can get past the IDE/.NET issues...and that would take a lot _more_ time (not that I'm not already late).

The application itself is reasonably simple. Using the template API project in Visual Studio, I've exposed an endpoint that allows you to pass an origin and destination airport code as part of the URL. This will then create the models for the graph using the airport and route csv files (the airlines file was mostly ignored as it did not provide value in this exercise. Future improvements may require it but it is easily added). Once the model is made, the graph is searched using BFS until a route is found, at which point it is returned as a list of flights.

**To run this** 

Simply open the .sln file inside of here in Visual Studio and click the green run button at the top. The browser should open and the default page will show the formatting instructions for the URL, but to be extra clear the format is:

`[localhost]/api/values/{origin}/{destination}`

for example:

`[localhost]/api/values/YYZ/YUL`

Note: There are a lot of VS template files included in the branch. I'll highlight the things I actually wrote here.

**ValuesController.cs**

The api endpoint. It calls the graph constructor, the `ShortestPath` method, and returns the result. It also will catch exceptions thrown by the `ShortestPath` method and return the message contained within.

**Airline.cs**

Airline model. Unused.

**Airport.cs**

Airport model. By putting the outbound flights directly in this object when creating the graph, it made the search function a little simpler to follow as all adjacency was contained within the objects themselves. The `IncomingFlight` property allows for reviewing the succesful route in the BFS search.

**Route.cs**

Route model. Simply has two `Airport` objects as properties.

**ModelConstructor.cs**

Simply where the csv parsing actually happens.

**AirRouteGraph.cs**

This is where the list of `Airport` and `Route` objects gets stored, and where the `ShortestPath` method is. The method is simply breadth-first search with some exception throwing in exceptional cases (i.e. bad input, no route).

***Exception.cs**

All these were created to aid in unit testing, as it's much simpler to assert that a type of exception was thrown than it is to check the message...for some reason.

**AirRouteGraphTests.cs**

Unit tests. I'm honestly not super sure on why but Visual Studio (and then commandline dotnet) refused to run these tests. I spent far too long trying to sort that out, but these tests are at least, as far as I can tell without actually running them, accurate. I set up a small test network and wrote test cases to cover error handling as well as the success path, with different lengths of routes. I specifically included a test case that would have a longer route available to make sure it was the _minimum_ being returned. 

**Future improvements**

As alluded in the long-winded introduction there's quite a lot of things I would improve in this solution if I worked on it further, such as
* Working unit tests
* An actual user interface
* Persist the graph so it's not rebuilt on every request
* Include airline filter functionality
* Include airline information with the flights.
* Factor in flight distance into graph weights (maybe?)
