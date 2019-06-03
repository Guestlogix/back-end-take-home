const filePath = '../data/full/airlines.csv'
const csvLoader = require('../utils/csvLoader.js')
const _ = require('lodash')
let fileToJSON
module.exports = {

  async loadItems () {
    fileToJSON = await csvLoader.loadCSVToJSON(filePath)
    return fileToJSON
  },

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
