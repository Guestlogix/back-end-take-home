const Graph = require('../utils/graph')
const filePath = '../data/full/routes.csv'
const csvLoader = require('../utils/csvLoader.js')
const _ = require('lodash')
let routesGraphObj = new Graph()
let fileToJSON
module.exports = {

  async loadItems () {
    fileToJSON = await csvLoader.loadCSVToJSON(filePath)
    for (let connections of fileToJSON) {
      routesGraphObj.addEdge(connections.Origin, connections.Destination)
    }
    return routesGraphObj
  },

  findShortestPath (source, destination) {
    let result = routesGraphObj.findShortestPath(source, destination)
    return result
  },

  splitMultipleRoutesToUnits (arr) {
    let response = []
    for (let i = 0; i < arr.length - 1; i++) {
      response.push({ path: [arr[i], arr[i + 1]] })
    }
    for (let details in response) {
      let results = _.filter(fileToJSON, { 'Origin': response[details].path[0], 'Destination': response[details].path[1] })
      response[details].AirlineDetails = results
    }
    return response
  }
}
