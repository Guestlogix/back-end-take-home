const graphDS = require('graph-data-structure')
class Graph {
  /**
 * Constructor: load the Graph datastructure library
 */
  constructor () {
    this.graphObj = graphDS()
  }
  /**
 * Adds source and edge to graph data structure.
 *
 * @param {string} source - 3 character source input
 * @param {string} destination -  3 character destination input
 *
 */
  addEdge (source, destination) {
    this.graphObj.addEdge(source, destination)
  }
  /**
 * Adds source and edge to graph data structure.
 *
 * @param {string} source - 3 character source input
 * @param {string} destination -  3 character destination input
 * @return {Array} return shortest path in array format
 *
 */
  findShortestPath (source, destination) {
    return this.graphObj.shortestPath(source, destination)
  }
}
module.exports = Graph
