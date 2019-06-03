const filePath = '../data/full/airports.csv'
const csvLoader = require('../utils/csvLoader.js')
const _ = require('lodash')
let fileToJSON
module.exports = {

  async loadItems () {
    fileToJSON = await csvLoader.loadCSVToJSON(filePath)
    return fileToJSON
  },

  getAirportDetails (data) {
    for (let details in data) {
      let airportLocations = data[details].path
      let airportDetails = []
      for (let airportDetailsIterator in airportLocations) {
        let results = _.filter(fileToJSON, {'IATA 3': airportLocations[airportDetailsIterator]})
        airportDetails.push(results)
      }
      data[details].AirportDetails = airportDetails
    }
    return data
  }
}
