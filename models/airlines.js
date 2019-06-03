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
    for (let details in data) {
      let detailsInner = data[details].AirlineDetails
      for (let airlineDetails in detailsInner) {
        let results = _.filter(fileToJSON, {'2 Digit Code': detailsInner[airlineDetails]['Airline Id']})
        detailsInner[airlineDetails].details = results
      }
    }
    return data
  }
}
