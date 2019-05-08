const fs = require('fs');
const csv = require('csv-parser');
const Airport = require('./app/models/airports.model');
const mongoose = require("mongoose");
const config = require("./config/config");

mongoose.connect(config.database, { useNewUrlParser: true });

function migrate() {
  fs.createReadStream(process.cwd() + '/data/airports.csv')
  .pipe(csv())
  .on('data', (data) => {
    const airport = new Airport({
      name: data.Name,
      city: data.City,
      country: data.Country,
      iata3: data['IATA 3'],
      latitude: data.Latitude,
      longitude: data.Longitude
    })
    airport.save((err, data) => {
      if(err) {
        throw err;
      }
      console.log(data._id, 'saved');
    });
  })
  .on('error', (err) => console.log(err))
  .on('end', () => console.log('CSV read complete'));
};

function runMigration() {
  let result;
  Airport.find({}, (err, airports) => {
    if(err) {
      throw err;
    }
    if(airports.length > 0) {
      console.log('Airport data exists');
    } else {
      migrate();
    }
  })
};

runMigration();
