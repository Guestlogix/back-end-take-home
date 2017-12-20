require('./models/model');
var express = require('express');
var mongoose = require('mongoose');
var Airport = mongoose.model('Airport');
var Route = mongoose.model('Route');
var HashMap = require('hashmap');


var port = process.env.PORT || 3000;

var app = express();

var configDB = require('./config/database.js');
mongoose.connect(configDB.url,{ useMongoClient: true });

app.get('/:origin-:dest', function(req, res) {
	console.log("params " + req.params);
	res.send('hello world')
	var origin = req.params.origin;
	var destination = req.params.destination;

	console.log("origin " + req.params.origin);
	console.log("destination " + req.params.dest);
	var wantedRoutes = []
	var wantedRoutesHashMap1 = new HashMap();

	Route.find({origin: req.params.origin}, function (err, routes) {
		if(err)
			console.log('err')

		for(var i = 0; i < routes.length; i++) {
			//console.log(i);
			if(routes[i].destination == req.params.dest) {
				console.log("1stloop")
				console.log(routes[i].destination);
				wantedRoutes.push(routes[i])

			}	else {

				Route.find({origin: routes[i].destination}, function (err, routes1){
					for(var j = 0; j < routes1.length; j++) {
						//console.log(routes1[j].destination)
						if(routes1[j].destination == req.params.dest) {
							console.log("2ndloop")
							console.log(routes1[i].origin  + "=>" + routes1[j].destination);
							wantedRoutesHashMap1.set(routes[i], routes1[j])


			}
					}

				}
				)
			}
		}


	})
})




app.listen(port);
module.exports = app;