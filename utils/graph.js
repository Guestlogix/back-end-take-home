const graphDS = require('graph-data-structure')
class Graph {
  constructor () {
    this.graphObj = graphDS()
  }

  addEdge (source, destination) {
    this.graphObj.addEdge(source, destination)
  }

  findShortestPath (source, destination) {
    return this.graphObj.shortestPath(source, destination)
  }
}
module.exports = Graph
