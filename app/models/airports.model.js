const mongoose = require('mongoose');
Schema = mongoose.Schema;

const airportSchema = new Schema({
  name: {
    type: String
  },
  city: {
    type: String
  },
  country: {
    type: String
  },
  iata3: {
    type: String
  },
  latitude: {
    type: String
  },
  longitude: {
    type: String
  }
})  

module.exports = mongoose.model('Airport', airportSchema);