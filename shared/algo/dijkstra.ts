import { IAirport, IStore } from "../../server/store";
import { ShortestPathNodes } from "../shortest-path";
import _ = require("lodash");

/**
 * Implementation is based off the pseudocode found in https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm
 */
export function findShortestPath(store: IStore, origin: IAirport, destination: IAirport): ShortestPathNodes {
	const { routes, airports } = store;
	let queue: IAirport[] = [];
	const visited: Record<string, boolean> = {};
	const routesByOrigin = _.groupBy(routes, 'Origin');

	// this will track our shortest path
	// key: last airport to be visited
	// value: accumulated path
	const shortestPath: Record<string, string[]> = {
		[origin.IATA3]: [origin.IATA3]
	};

	queue.push(origin);
	const isNotVisited = (airport: IAirport) => !visited[airport.IATA3];
	while (queue.length > 0) {
		let source = queue.shift();
		if (!visited[source.IATA3]) {
			// airports are marked as visited
			visited[source.IATA3] = true;

			// visit all adjacent airports
			const airports = adjacentAirports(source);
			let i;
			for (i = 0; i < airports.length; ++i) {
				const next = airports[i];

				if (next.IATA3 === destination.IATA3) {
					// destination found
					return { 
						nodes: [...shortestPath[source.IATA3], next.IATA3]
					};
				}
				if (isNotVisited(next)) {
					// track our accumulated path against the next airport
					const currLength = shortestPath[next.IATA3] 
						? shortestPath[next.IATA3].length
						: Infinity;
					const nextLength = shortestPath[source.IATA3].length;
					// but only update if the new found path is shorter than the existing one
					if (nextLength < currLength) {
						shortestPath[next.IATA3] = [...shortestPath[source.IATA3], next.IATA3];
					}
					
					// add new adjacent airports to the queue for processing
					queue.push(next);
				}
			}
		}
	}

	function adjacentAirports(from: IAirport): IAirport[] {
		return (routesByOrigin[from.IATA3] || [])
			.map(route => airports[route.Destination]);
	}

	// No path found
	return { nodes: [] };
}