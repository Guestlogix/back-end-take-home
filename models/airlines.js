let filePath
process.env.MODE === 'TEST' ? filePath = '../data/test/airlines.csv' : filePath = '../data/full/airlines.csv'

const csvLoader = require('../utils/csvLoader.js')
const _ = require('lodash')
let fileToJSON
module.exports = {
/**
 * Load the CSV file for Airlines.
 *
 * @return {JSON} CSV to JSON for AirlineData
 *
 */
  async loadItems () {
    fileToJSON = await csvLoader.loadCSVToJSON(filePath)
    return fileToJSON
  },
  /**
 * fetch airline details from the CSV file provided.
  * @param {Array} data - Data which contains the data from routes
 * @return {Array} returns data with Airline details based on routes
 *
 */
  getAirlineDetails (data) {
    for (const details in data) {
      const detailsInner = data[details].AirlineDetails
      for (const airlineDetails in detailsInner) {
        const results = _.filter(fileToJSON, {'2 Digit Code': detailsInner[airlineDetails]['Airline Id']})
        detailsInner[airlineDetails].details = results
      }
    }
    return data
  }
}
