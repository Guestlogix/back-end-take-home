import { expect } from 'chai';
import { findShortestPath } from "../shared/algo";
import { IStore, Store, IAirport } from "../server/store";

describe('shortest path algorithms', () => {
	let store: IStore;
	before(() => {
		store = Store({
			airlinesCsv: './data/test/airlines.csv',
			airportsCsv: './data/test/airports.csv',
			routesCsv: './data/test/routes.csv'
		});
	});
	it('should work with the examples in the README', () => {
		const { airports } = store;
		const YYZ = airports['YYZ'];
		const JFK = airports['JFK'];
		const YVR = airports['YVR'];
		const ORD = airports['ORD'];
		const matrix: [IAirport, IAirport, string[]][] = [
			[YYZ, JFK, ['YYZ', 'JFK']],
			[YYZ, YVR, ['YYZ', 'JFK', 'LAX', 'YVR']],
			[YYZ, ORD, []]
		];
		matrix.forEach(([origin, destination, expectedNodes], i) => {
			const results = findShortestPath(store, origin, destination);
			expect(results).to.deep.equal({ nodes: expectedNodes });
		});
	});
});