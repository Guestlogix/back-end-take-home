const filePath = '../data/full/airports.csv'
const csvLoader = require('../utils/csvLoader.js')
const _ = require('lodash')
let fileToJSON
module.exports = {
/**
 * Load the CSV file for Airports.
 *
 * @return {JSON} CSV to JSON for Airports data
 *
 */
  async loadItems () {
    fileToJSON = await csvLoader.loadCSVToJSON(filePath)
    return fileToJSON
  },
  /**
 * fetch airport details from the CSV file provided.
 * @param {Array} data - Data which contains the data from routes and airlines
 * @return {Array} returns data with airport details based on routes and airlines
 *
 */
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
