import PriorityQueue from './Queue';

interface Neighbor {
    node: string;
    airline: string;
    weight: number;
}

export default class Graph {
    /**
     * @param nodes Locations on the map
     * @param adjacencyList Holds an array of neighboring routes
     */
    constructor(private nodes: string[] = [], private adjacencyList: any = {}) {}

    /**
     * Adds a node to the graph (a new location on our map)
     *
     * @param node A string representing a location on the map
     */
    addNode(node: string) {
        // push node into the collection of node values
        this.nodes.push(node);
        // add a new entry in the adjacency list, setting its value to an empty array
        this.adjacencyList[node] = [];
    }

    /**
     * Push the neighboring node along with the time it takes to get there into the adjacencyList array
     *
     * @param node1 Starting point
     * @param node2 Destination point
     * @param airline A string representing the airlineId
     */
    addEdge(node1: string, node2: string, airline: string) {
        this.adjacencyList[node1].push({ node: node2, airline, weight: 1 });
        this.adjacencyList[node2].push({ node: node1, airline, weight: 1 });
    }

    /**
     * Find the shortest connecting path from the origin to the destination
     * This use the Dijkstra’s Algorithm
     *
     * @param startNode Origin node
     * @param endNode Destination node
     */
    findShortestPath(startNode: string, endNode: string) {
        const times: any = {};
        const backtrace: any = {};
        const pq = new PriorityQueue();

        // it is the shortest time to get to our start position
        times[startNode] = 0;

        /** Declare variables to check if searched node is in available nodes */
        let foundStart = false;
        let foundEnd = false;

        /** The shortest time it takes to reach the others could be anything, so we’ll initialize those times to infinity. */
        this.nodes.forEach(node => {
            if (node !== startNode) {
                times[node] = Infinity;
            }

            // set found status if current node equals to the start or end searched node
            if ((startNode === endNode) && node === startNode) {
                foundStart = true;
                foundEnd = true;
            } else if (node === startNode) {
                foundStart = true;
            } else if (node === endNode) {
                foundEnd = true;
            }
        });

        // if either the start or end node is not in nodes collection, no point going further
        if (!foundEnd || !foundStart) {
            return [];
        }

        // We add our starting node to the priority queue
        pq.enqueue([startNode, 0]);

        /**
         * We access the first element in the queue and start checking its neighbors,
         * which we find using the adjacency list we made at the very beginning.
         * 
         * We add the neighbor’s weight to the time it took to get to the node we’re on.
         */
        while (!pq.isEmpty()) {
            const shortestStep = pq.dequeue();
            const currentNode = shortestStep[0];

            this.adjacencyList[currentNode].forEach((neighbor: Neighbor) => {
                const time = times[currentNode] + neighbor.weight;

                /**
                 * if the calculated time is less than the time we currently have on file for this neighbor,
                 * then we update our times, we add this step to our backtrace, and we add the neighbor to our priority queue
                 */
                if (time < times[neighbor.node]) {
                    times[neighbor.node] = time;
                    backtrace[neighbor.node] = currentNode;
                    pq.enqueue([neighbor.node, time]);
                }
            });
        }

        const path = [endNode];
        let lastStep = endNode;

        while (lastStep !== startNode) {
            path.unshift(backtrace[lastStep]);
            lastStep = backtrace[lastStep];
        }

        // console.log(`Path is ${path} and time is ${times[endNode]}`);
        return path;
    }
}