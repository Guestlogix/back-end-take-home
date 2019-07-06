import * as _ from 'lodash';
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
export function ShortestPathCalculator(options: ShortestPathCalculatorOptions) {
	/**
	 * Given origin and destination, attempt to calculate the shortest path between them and return the results.
	 */
	function calculate(param: ShortestPathParameters): ShortestPathResults {
		const { origin, destination } = param;
		throw new Error('Not Implemented Yet');
	}

	return {
		calculate
	};
}

