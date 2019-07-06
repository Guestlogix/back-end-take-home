import { execute } from "../shared/algos/bfs";
import { IStore, Store } from "../server/store";

describe('shortest path algorithms', () => {
	let store: IStore;
	before(() => {
		store = Store({
			airlinesCsv: './data/test/airlines.csv',
			airportsCsv: './data/test/airports.csv',
			routesCsv: './data/test/routes.csv'
		});
	});
	describe('bfs', () => {
		it.only('should work', () => {
			const { airports } = store;
			const YYZ = airports['YYZ'];
			const JFK = airports['JFK'];
			const YVR = airports['YVR'];
			const results = execute(store, YYZ, JFK);
			console.log(results);
		});
	});
});