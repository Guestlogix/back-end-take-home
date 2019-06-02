class Graph {
  constructor (vertexCount) {
    this.vertexCount = vertexCount
    this.adjuctList = new Map()
  }

  addVertex (vertice) {
    this.adjuctList.set(vertice, [])
  }

  addEdge (source, destination) {
    this.AdjList.get(source).push(destination)
    this.AdjList.get(source).push(destination)
  }
}
