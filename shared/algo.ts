import { IAirport, IStore } from '../server/store';
import { getDistance } from 'geolib';
import * as _ from 'lodash';

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

export function execute(store: IStore, origin: IAirport, destination: IAirport) {
	const { routes, airports } = store;
	// notes: quick google search seems to indicate bfs can be applied to graphs that can have cycles.
	const visitedNodes: Record<string, IAirport> = {
		[origin.IATA3]: origin // include origin!
	};
	const routesByOrigin = _.groupBy(routes, 'Origin');

	function visit(from: IAirport): Journey<string>[] {
		// get net new locations to check
		const netNew = edges(from)
			.filter(edge => isNewLocation(edge.to));
		if (netNew.length === 0) {
			// when no more connections can be made, 
			// we should still cap it off properly
			return [createJourney(from)];
		}

		// two things can happen, either:
		// - one of the netNew locations is the destination
		// - or it's not, so continue looking
		const branches = netNew.map(edge => {
			if (isDestination(edge.to)) {
				// netNew location is the destination.
				// cap off the journey.
				return createJourney(from, edge.to); 
			} else {
				// netNew location is not the destination
				// keep visiting new locations
				return visit(edge.to).map(x => {
					// bubble up this location with additional locations found while visiting
					return { nodes: [from.IATA3, ...x.nodes] };
				});
			}
		});
		
		return _.flatMap(branches);
	}

	/**
	 * Given an origin, return an array of Edges to process
	 */
	function edges(from: IAirport): Edge[] {
		return routesByOrigin[from.IATA3].map(route => {
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

	return visit(origin);
}

