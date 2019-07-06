import { IAirport, IStore } from '../server/store';
import { getDistance } from 'geolib';
import * as _ from 'lodash';
import { ShortestPathNodes } from './shortest-path';

function coord(airport: IAirport) {
	const { Latitude, Longitude } = airport;
	return {
		latitude: Latitude,
		longitude: Longitude
	};
}

/**
 * Tracks and groups a bunch of nodes together as a Journey.
 * Mostly created to reduce the cognitive load of recursion.
 */
export interface Journey<T> {
	nodes: T[];
}
function createJourney(...args: IAirport[]) {
	return { nodes: args.map(x => x.IATA3) };
}

/**
 * An edge represents a connection to the destination.
 */
interface Edge {
	to: IAirport;
	distance: number;
}

export function findShortestPath(store: IStore, origin: IAirport, destination: IAirport): ShortestPathNodes {
	const { routes, airports } = store;
	// notes: quick google search seems to indicate bfs can be applied to graphs that can have cycles.
	const visitedNodes: Record<string, IAirport> = {
		[origin.IATA3]: origin // include origin!
	};
	const routesByOrigin = _.groupBy(routes, 'Origin');

	function visit(from: IAirport): Journey<string>[] {
		// get net new locations to check
		const newEdges = edges(from)
			.filter(edge => isNewLocation(edge.to));
		if (newEdges.length === 0) {
			// when no more connections can be made, 
			// we should still cap it off properly
			return [createJourney(from)];
		}

		return processEdges(from, newEdges);
	}

	function processEdges(from: IAirport, newEdges: Edge[]): Journey<string>[] {
		// two things can happen, either:
		// - one of the netNew locations is the destination
		// - or it's not, so continue looking
		const nextNodes: IAirport[] = [];
		let i;
		for (i = 0; i < newEdges.length; ++i) {
			const edge = newEdges[i];
			if (isDestination(edge.to)) {
				// if we hit the destination, we don't need to explore the other branches.
				return [createJourney(from, edge.to)]; 
			} else {
				nextNodes.push(edge.to);
			}
		}

		const journeys = nextNodes.map(airport => {
			return visit(airport).map(journey => {
				// bubble up this location with additional locations found while visiting
				return { 
					nodes: [from.IATA3, ...journey.nodes]
				};
			})
		});
		
		return _.flatMap(journeys);
	}

	/**
	 * Given an origin, return an array of Edges to process
	 */
	function edges(from: IAirport): Edge[] {
		return (routesByOrigin[from.IATA3] || []).map(route => {
			const to = airports[route.Destination];
			const distance = getDistance(coord(from), coord(to));
			return {
				to, distance
			};
		});
	}
	/**
	 * Note: This filter will update visitedNodes with the node that's being checked.
	 */
	function isNewLocation(node: IAirport) {
		if (!visitedNodes[node.IATA3]) {
			// destination should never count as visited.
			// there's a separate stop condition for destination.
			if (!isDestination(node)) {
				visitedNodes[node.IATA3] = node;
			}
			return true;
		} else {
			return false;
		}
	}
	function isDestination(node: IAirport) {
		return destination.IATA3 === node.IATA3;
	}

	const allJourneys = visit(origin);
	const validNodes = allJourneys
		.filter(j => _.last(j.nodes) === destination.IATA3)
		.map(j => j.nodes);

	// shortest route first!
	const sortedNodes = _.sortBy(validNodes, (j) => j.length);

	// I am curious about longest routes too, for a test case
	// console.log(_.last(sortedNodes));
	
	// return nodes from the first journey
	return { nodes: sortedNodes[0] || [] };
}

