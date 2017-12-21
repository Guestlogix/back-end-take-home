require('./models/model');
var express = require('express');
var mongoose = require('mongoose');
var Airport = mongoose.model('Airport');
var Route = mongoose.model('Route');
var HashMap = require('hashmap');
var geolib = require('geolib');
var forEach = require('async-foreach').forEach;


var port = process.env.PORT || 3001;

var app = express();

var configDB = require('./config/database.js');
mongoose.connect(configDB.url,{ useMongoClient: true });


/*app.get('/distanceOfAll', function(req, res) {
	console.log("here");
	distance = -1;

	Route.count({"distance": {"$exists": false}}, function(err, c) {
        console.log("count " + c);
    })

	Route.find({"distance": {"$exists": false}}, function(err, route) {
		console.log(route.length);
		
		forEach(route, function(item, index, array) {
			console.log(index);
			

		Airport.findOne({"iata3": item.origin}, function(err, airport1) {
				if(err)
					console.log(err)


				Airport.findOne({"iata3": item.destination}, function(err, airport2) {
					if(err)
						console.log(err)

					if(airport1 && airport2) {
					 distance = geolib.getDistance(
						{latitude: airport1.latitude, longitude: airport1.longitude},
						{latitude: airport2.latitude, longitude: airport2.longitude}
					)
					}
					console.log(distance);
					item.distance = distance;
					
					item.save(function (err, route1) {
                        if (err)
                            console.log(err);
                       

                        console.log("distance " + index + " = " + route1.distance)
                    })
                


				})
			})
})
	}).limit(1000);

})
*/

app.get('/:origin-:dest', function(req, res) {
	
	var origin = req.params.origin;
	var destination = req.params.destination;

	
	var wantedRoutes = []
	var wantedRoutes1 = []
	var wantedRoutes2 = []
	var currenti = {};
	var currentj= {};
	var currentk = {};
	var distance = 0;
	var shortestRoute = []
	var shortestDistance = -1;

	Route.find({origin: req.params.origin}, function (err, routes) {
		if(err)
			res.send("err" + err)

		forEach(routes, function(route0, i, routes) {
			
			
			
			if(routes.length < 1) {
				res.send("No Routes Found")
			}
			
			if(route0.destination == req.params.dest) {
				console.log("1stloop")
				
				console.log(route0.origin + "->" + route0.destination)	
				console.log(route0.distance)
				if(shortestDistance == -1) {
					shortestRoute[0] = route0
					shortestDistance = route0.distance
				} else {
					if(currenti.distance < shortestDistance) {
						shortestDistance = route0.distance
						shortestRoute[0] = route0
					}
				}
				wantedRoutes.push(route0)

				
			}	 {
				console.log(route0.destination)
				Route.find({origin: route0.destination}, function (err, routes1){
					if((i == routes.length - 1) && routes1.length == 0) {
						if(shortestDistance == -1) {
							res.send("No Routes Found")
						} else {
							res.send({"shortestRoute": shortestRoute, "shortestDistance": shortestDistance});
						}
					}
					forEach(routes1, function(route1, j, routes1) {
						
						
						if(route1.destination == req.params.dest) {
						if(shortestDistance == -1) {
							shortestRoute[0] = route0
							shortestRoute[1] = route1
							shortestDistance = route0.distance + route1.distance
						} else {
							if((route0.distance + route1.distance) < shortestDistance) {
								shortestRoute[0] = route0
								shortestRoute[1] = route1
								shortestDistance = route0.distance + route1.distance
							}
						}
							wantedRoutes1.push([route0, route1])
							
			}  
				//console.log("2 stop over")
				Route.find({origin: route1.destination}, function(err, routes2){
					if((i == routes.length - 1) && (j == routes1.length -1) && routes2.length == 0) {
						if(shortestDistance == -1) {
							res.send("No Routes Found")
						} else {
							res.send({"shortestRoute": shortestRoute, "shortestDistance": shortestDistance});
						}
					}
						
					
					forEach(routes2, function(route2, k, routes2) {
					
						
						if(route2.destination = req.params.dest) {
						if(shortestDistance == -1) {
							shortestRoute[0] = route0
							shortestRoute[1] = route1
							shortestRoute[2] = route2
							shortestDistance = route0.distance + route1.distance + route2.distance
						} else {
							if((route0.distance + route1.distance + route2.distance) < shortestDistance) {
								shortestRoute[0] = route0
								shortestRoute[1] = route1
								shortestRoute[2] = route2
								shortestDistance = route0.distance + route1.distance + route2.distance
							}
						}
							wantedRoutes2.push([route0, route1, route2])
							//console.log(wantedRoutes2[k])
						}
						
						if((i == routes.length - 1) && (j == routes1.length -1) &&  (k == routes2.length -1)) {
						if(shortestDistance == -1) {
							res.send("No Routes Found")
						} else {
							res.send({"shortestRoute": shortestRoute, "shortestDistance": shortestDistance});
						}
					}


					})
					
					
				})


			
					}) //

				}
				)
			}
			
			


		}) //

		


	})
})


function findDistanceBetween2Airports(code1, code2) {
	console.log('here' + code1 + " here " + code2)
	Airport.findOne({"iata3": code1}, function(err, airport1) {
				if(err)
					console.log(err)

				Airport.findOne({"iata3": code2}, function(err, airport2) {
					if(err)
						consle.log(err)

					var distance = geolib.getDistance(
						{latitude: airport1.latitude, longitude: airport1.longitude},
						{latitude: airport2.latitude, longitude: airport2.longitude}
					)
					
					return distance;
					


				})
			})
}

app.listen(port);
module.exports = app;
