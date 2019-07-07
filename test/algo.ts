import { expect } from 'chai';
// import { findShortestPath } from "../shared/algo/dfs-modified";
import { findShortestPath } from "../shared/algo/dijkstra";
import { IStore, createStore, IAirport } from "../server/store";

describe.only('shortest path algorithms', () => {
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
				expect(results.nodes).to.deep.equal(expectedNodes);
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
			const { 
				YYZ, JFK, LAX, YVR, ORD, GVA, JUB, CGO
			} = airports;
			const matrix: [IAirport, IAirport, string[]][] = [
				[YYZ, JFK, ['YYZ', 'JFK']],
				[YYZ, YVR, ['YYZ', 'YVR']],
				[YYZ, ORD, ['YYZ', 'ORD']],
				[YYZ, GVA, ['YYZ', 'EWR', 'GVA']],
				// [YYZ, JUB, ['YYZ', 'IST', 'CAI', 'JUB']], 
				[YYZ, JUB, ['YYZ', 'IST', 'ADD', 'JUB']],
				[LAX, CGO, ['LAX', 'CAN', 'CGO']]
			];
			matrix.forEach(([origin, destination, expectedNodes], i) => {
				const results = findShortestPath(store, origin, destination);
				console.log(results);
				expect(results.nodes).to.deep.equal(expectedNodes);
			});
		});
		it.skip('try to find longest path', () => {
			const { airports, routes } = store;
			const LAX = airports['LAX'];
			const YYZ = airports['YYZ'];
			const destinations = ['WAW', 'CPH', 'AMS', 'CTU', 'CGO', 'BKK']
			destinations.forEach(destination => {
				const airport = airports[destination];
				console.log(findShortestPath(store, LAX, airport).nodes);
			});
		});
	});
	describe('free form test', () => {
		const makeAirport = (code) => ({ Name: '', City: '', Country: '', IATA3: code, Longitude: 0, Latitude: 0 })
		const makeRoute = (from, to) => ({ AirlineId: '', Origin: from, Destination: to });
		let store: IStore = {
			airlines: {},
			airports: {
				AAA: makeAirport('AAA'),
				BBB: makeAirport('BBB'),
				CCC: makeAirport('CCC'),
				DDD: makeAirport('DDD'),
				EEE: makeAirport('EEE'),
			},
			routes: [
				makeRoute('AAA', 'BBB'),
				makeRoute('AAA', 'CCC'),
				makeRoute('BBB', 'DDD'),
				makeRoute('DDD', 'EEE'),
				makeRoute('CCC', 'EEE')
			]
		}
		it('is expected to find the shortest route', () => {
			const { airports } = store;
			const { AAA, EEE } = airports;
			const matrix: [IAirport, IAirport, string[]][] = [
				[AAA, EEE, ['AAA', 'CCC', 'EEE']],
			];
			matrix.forEach(([origin, destination, expectedNodes], i) => {
				const results = findShortestPath(store, origin, destination);
				expect(results.nodes).to.deep.equal(expectedNodes);
			});
		});
	});
});