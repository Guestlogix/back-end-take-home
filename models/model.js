var mongoose = require('mongoose');
var Schema = mongoose.Schema;



var airportsSchema = new mongoose.Schema({
	name: {type: String},
	city: {type: String},
	country: {type: String},
	iata3: {type: String},
	latitude: {type: Number},
	longitude: {type: Number}
})

var routesSchema = new mongoose.Schema({
	airlineId: {type: String},
	origin: {type: String},
	destination: {type: String}
})


mongoose.model('Airport', airportsSchema, 'Airports');
mongoose.model('Route', routesSchema, 'Routes');