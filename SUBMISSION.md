The /findRoute api is implemented in python 2.7 , using Flask.

Build and run Instructions:
	a) Please install python 2.7
	b) Install flask by running command "pip install flask"
	c) run the app from /api directory by following command "python app.py"
            The app will run on port 80.
	    

The api is hosted at http://18.217.181.95/

Note:The route is found using Breadth First Search.

Request format:

	http://18.217.181.95/findRoute?org=<origin-code>&dst=<destination-code>

Response format: JSON

   { 
	type: Error | Success, 
	
	message : string,
	
	route: [
		<string airport-code>
	],
	
	detailedRoute : [ 
		{
			org:{
				code: <string airport code>,
				airport: <string airport-name>,
				city: <string city-name>,
			},
			
			dst : {
				code: <string airport code>,
				airport: <string airport-name>,
				city: <string city-name>
			}
			
			flightOptions : [
				{
					code:<string airline code>,
					name:<string airline name>,
					
				}
			]
		}
	]
    }



Sample Responses:

1) http://18.217.181.95/findRoute?org=JFK&dst=YYR

Response:

{

	"message": "Route Found",
	"route": ["JFK", "YUL", "YHZ", "YYR"],
	"type": "Success",
       "detailedRoute": [{
		"dst": {
			"airport": "Montreal / Pierre Elliott Trudeau International Airport",
			"city": "Montreal",
			"code": "YUL"
		},
		"flightOptions": [{
			"code": "WS",
			"name": "WestJet"
		}],
		"org": {
			"airport": "John F Kennedy International Airport",
			"city": "New York",
			"code": "JFK"
		}
	}, {
		"dst": {
			"airport": "Halifax / Stanfield International Airport",
			"city": "Halifax",
			"code": "YHZ"
		},
		"flightOptions": [{
			"code": "AC",
			"name": "Air Canada"
		}],
		"org": {
			"airport": "Montreal / Pierre Elliott Trudeau International Airport",
			"city": "Montreal",
			"code": "YUL"
		}
	}, {
		"dst": {
			"airport": "Goose Bay Airport",
			"city": "Goose Bay",
			"code": "YYR"
		},
		"flightOptions": [{
			"code": "AC",
			"name": "Air Canada"
		}],
		"org": {
			"airport": "Halifax / Stanfield International Airport",
			"city": "Halifax",
			"code": "YHZ"
		}
	}]
}

2) http://18.217.181.95/findRoute?org=JFK&dst=JFK

   Response:
          {"message":"Origin and Destination must be different:JFK","type":"Error"}



3) http://18.217.181.95/findRoute?org=XXX&dst=YYR

   Response:

	  {'message': 'Invalid Origin:XXX', 'type': 'Error'}

4)  http://18.217.181.95/findRoute?org=JFK&dst=XXX

   Response:

	 {'message': 'Invalid Destination:XXX', 'type': 'Error'}
	
	
5) When no route exists, the response is in the format:

   Response:

{ 'message': 'No Route Exists', 'type': 'Failure'}


	