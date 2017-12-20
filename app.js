var express = require('express');
var mongoose = require('mongoose');

var port = process.env.PORT || 3000;

var app = express();

require('./models/model');

app.get('/:origin-:dest', function(req, res) {
	console.log("params " + req.params);
	res.send('hello world')
	var origin = req.params.origin;
	var destination = req.params.destination;

	console.log("origin " + req.params.origin);
	console.log("destination " + req.params.dest);
})




app.listen(port);
module.exports = app;