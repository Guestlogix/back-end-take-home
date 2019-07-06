import * as _ from 'lodash';
import { IStore, IAirport } from '../server/store';
export interface ShortestPathParameters {
	origin: string;
	destination: string;
}
export interface ShortestPathNodes {
	nodes: string[];
}
export enum ShortestPathError {
	InvalidOrigin,
	InvalidDestination,
	Invalid
}
export type ShortestPathResults = ShortestPathNodes|ShortestPathError;
export interface ShortestPathCalculatorOptions {
	store: IStore;
}
export interface IShortestPathCalculator {
	/**
	 * Given origin and destination, attempt to calculate the shortest path between them and return the results.
	 */
	calculate(param: ShortestPathParameters): ShortestPathResults;
}

/**
 * Helper function to determine if value can be considered a ShortestPathNodes object.
 */
export function isShortestPathNodes(value): value is ShortestPathNodes {
	return value && _.isArray(value.nodes);
}

/**
 * Creates a shortest path calculator using the given information in options.
 */
export function ShortestPathCalculator(options: ShortestPathCalculatorOptions): IShortestPathCalculator {
	const { store } = options;
	function calculate(param: ShortestPathParameters): ShortestPathResults {
		const { 
			origin: rawOrigin, 
			destination: rawDestination 
		} = param;
		const origin: IAirport = store.airports[rawOrigin];
		const destination: IAirport = store.airports[rawDestination];

		if (origin == null && destination == null) {
			return ShortestPathError.Invalid;
		} else if (origin == null) {
			return ShortestPathError.InvalidOrigin;
		} else if (destination == null) {
			return ShortestPathError.InvalidDestination;
		}

		if (origin.IATA3 === destination.IATA3) {
			return ShortestPathError.Invalid;
		}
	}

	return {
		calculate
	};
}

