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
    for (const details in data) {
      const airportLocations = data[details].path
      const airportDetails = []
      for (const airportDetailsIterator in airportLocations) {
        const results = _.filter(fileToJSON, {'IATA 3': airportLocations[airportDetailsIterator]})
        airportDetails.push(results)
      }
      data[details].AirportDetails = airportDetails
    }
    return data
  }
}
