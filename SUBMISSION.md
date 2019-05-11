Information

Built solution using nodejs using Dijkstra's algorithm 

The solution handled inconsistent/Invalid data by
. Ensuring distinct vertix is added to the Vertex/Node
. Ensuring only valid routes (Origin / Destination) are added to the edges
. ensuring there are available airline associated with a route

To run project locally
. Clone the project
. Install project's dependencies using npm install
. Run project using node index.js or nodemon index.js
. The server is started on PORT : 9000
. Go to your browser or an alternate API testing application e.g POSTMAN
. Make a call to http://localhost:9000/routes/?origin=YZF&destination=YEG
. You can change value for the query parameters origin and destination

Observation
I noticed some data are not valid but I didn't want to edit the data provided to me for the project,therefore, I handled the anomalities in the solution.
