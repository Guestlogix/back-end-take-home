import { expect } from 'chai';
import { findShortestPath } from "../shared/algo";
import { IStore, createStore, IAirport } from "../server/store";

describe('shortest path algorithms', () => {
	describe('test store', () => {
		let store: IStore;
		before(async () => {
			const folder = 'test';
			// const folder = 'full';
			store = await createStore({
				airlinesCsv: `./data/${folder}/airlines.csv`,
				airportsCsv: `./data/${folder}/airports.csv`,
				routesCsv: `./data/${folder}/routes.csv`
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
	describe('full store', () => {
		let store: IStore;
		before(async () => {
			// const folder = 'test';
			const folder = 'full';
			store = await createStore({
				airlinesCsv: `./data/${folder}/airlines.csv`,
				airportsCsv: `./data/${folder}/airports.csv`,
				routesCsv: `./data/${folder}/routes.csv`
			});
		});
		it('should work with full data', () => {
			const { airports, routes } = store;
			const YYZ = airports['YYZ'];
			const JFK = airports['JFK'];
			const YVR = airports['YVR'];
			const ORD = airports['ORD'];
			const matrix: [IAirport, IAirport, string[]][] = [
				[YYZ, JFK, ['YYZ', 'JFK']],
				[YYZ, YVR, ['YYZ', 'YVR']],
				[YYZ, ORD, ['YYZ', 'ORD']]
			];
			matrix.forEach(([origin, destination, expectedNodes], i) => {
				const results = findShortestPath(store, origin, destination);
				expect(results).to.deep.equal({ nodes: expectedNodes });
			});
		});
	});
});