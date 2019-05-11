const PriorityQueue = require('./priority-queue')

class WeightedGraph {
    constructor(){
        this.adjacencyList = {}
    }

    addVertex(vertex){
        if(!this.adjacencyList[vertex])
            this.adjacencyList[vertex] = []
    }  
    addEdge(vertex1, vertex2, weight) {
        this.adjacencyList[vertex1].push({node:vertex2,weight})
        this.adjacencyList[vertex2].push({node:vertex1,weight })
    }

    dijkstra(start, finish){
        let nodes = new PriorityQueue()
        let distances = {}
        let previous = {}
        let smallest;
        let path = [] // to return at end

        // build up initial state
        for (let vertex in this.adjacencyList){
            if(vertex === start){
                distances[vertex] = 0
                nodes.enqueue(vertex, 0)
            }else{
                distances[vertex] = Infinity
                nodes.enqueue(vertex, Infinity)
            }
            previous[vertex] = null
        }
        // as long as there is something to visit
        while(nodes.values.length){
            smallest = nodes.dequeue().val
            if(smallest === finish){
                while(previous[smallest]){
                    path.push(smallest)
                    smallest = previous[smallest]
                }break;
            }
            if(smallest || distances[finish] !== Infinity){
                for (let neigbour in this.adjacencyList[smallest]){
                    let nextNode = this.adjacencyList[smallest][neigbour]
                    let candidate = distances[smallest] + nextNode.weight
                    let nextNeigbor = nextNode.node
                    if(candidate < distances[nextNeigbor]){
                        distances[nextNeigbor] = candidate
                        previous[nextNeigbor] = smallest
                        nodes.enqueue(nextNeigbor, candidate) 
                    }
                }
            }
        }
        path = path.length < 1 ? path : path.concat(smallest).reverse()
        return path
    }
}

module.exports = WeightedGraph