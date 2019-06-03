const Graph = require('../utils/graph')

let filePath
process.env.MODE === 'TEST' ? filePath = '../data/test/routes.csv' : filePath = '../data/full/routes.csv'

const csvLoader = require('../utils/csvLoader.js')
const _ = require('lodash')
const routesGraphObj = new Graph()
let fileToJSON
module.exports = {
/**
 * Load the CSV file for Routes.
 *
 * @return {JSON} CSV to JSON for Routes data
 *
 */
  async loadItems () {
    fileToJSON = await csvLoader.loadCSVToJSON(filePath)
    for (const connections of fileToJSON) {
      routesGraphObj.addEdge(connections.Origin, connections.Destination)
    }
    return routesGraphObj
  },

  /**
 * Find shortest path based on graph-data-structure.
 * @param {String} source - Source location
 * @param {String} destination - Destination location
 * @return {Array} returns shortest path in array form
 *
 */
  findShortestPath (source, destination) {
    const result = routesGraphObj.findShortestPath(source, destination)
    return result
  },

  /**
 * Break the shortest path into source destination chunks for addition of data
 * @param {Array} arr - Source location
 * @return {Array} returns the chunked data for further processing
 *
 */
  splitMultipleRoutesToUnits (arr) {
    const response = []
    for (let i = 0; i < arr.length - 1; i++) {
      response.push({ path: [arr[i], arr[i + 1]] })
    }
    for (const details in response) {
      const results = _.filter(fileToJSON, { 'Origin': response[details].path[0], 'Destination': response[details].path[1] })
      response[details].AirlineDetails = results
    }
    return response
  }
}
