const Graph = require('../utils/graph')
const filePath = '../data/full/routes.csv'
const csvLoader = require('../utils/csvLoader.js')
let routesGraphObj = new Graph()
module.exports = {

  async loadItems () {
    let fileToJSON = await csvLoader.loadCSVToJSON(filePath)
    for (let connections of fileToJSON) {
      routesGraphObj.addEdge(connections.Origin, connections.Destination)
    }
    return routesGraphObj
  },

  findShortestPath (source, destination) {
    let result = routesGraphObj.findShortestPath(source, destination)
    console.log(result)
    return result
  }

}
