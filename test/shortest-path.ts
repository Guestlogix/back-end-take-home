import { expect } from 'chai';
import { 
	ShortestPathCalculator, IShortestPathCalculator, 
	ShortestPathError, isShortestPathNodes 
} from '../shared/shortest-path';
import { createStore } from '../server/store';
describe('shortest path', () => {
	let pathCalculator: IShortestPathCalculator;
	before(async () => {
		let store = await createStore({
			airlinesCsv: './data/test/airlines.csv',
			airportsCsv: './data/test/airports.csv',
			routesCsv: './data/test/routes.csv'
		});
		pathCalculator = ShortestPathCalculator({ store });
	});
	it('is expected to detect if the origin is invalid', () => {
		const invalidParams = [
			{ destination: 'JFK' },
			{ origin: null, destination: 'JFK' },
			{ origin: '', destination: 'JFK' },
			{ origin: 'XXX', destination: 'JFK' }
		];
		
		const expected = ShortestPathError.InvalidOrigin;
		const results = invalidParams.map(pathCalculator.calculate);
		let i;
		for (i = 0; i < results.length; ++i) {
			const result = results[i];
			expect(result).to.equal(expected, `expected results[${i}] (${result}) to match ShortestPathError.InvalidOrigin (${expected})`);
		}
		
	});
	it('is expected to detect if the destination is invalid', () => {
		const invalidParams = [
			{ origin: 'JFK' },
			{ origin: 'JFK', destination: null },
			{ origin: 'JFK', destination: '' },
			{ origin: 'JFK', destination: 'XXX' }
		];
		const expected = ShortestPathError.InvalidDestination;
		const results = invalidParams.map(pathCalculator.calculate);
		let i;
		for (i = 0; i < results.length; ++i) {
			const result = results[i];
			expect(result).to.equal(expected, `expected results[${i}] (${result}) to match ShortestPathError.InvalidDestination (${expected})`);
		}
	});
	it('is expected to detect if both origin and destination are invalid', () => {
		const invalidParams = [
			{}, { origin: '', destination: '' },
			{ origin: null, destination: null },
			{ origin: 'JFK', destination: 'JFK' },
			{ origin: 'XXX', destination: 'XXX' },
		];
		const expected = ShortestPathError.Invalid;
		const results = invalidParams.map(pathCalculator.calculate);
		let i;
		for (i = 0; i < results.length; ++i) {
			const result = results[i];
			expect(result).to.equal(expected, `expected results[${i}] (${result}) to match ShortestPathError.Invalid (${expected})`);
		}
	});
	it('is expected to calculate the route', () => {
		const matrix = [
			['YYZ', 'JFK', ['YYZ', 'JFK']],
			['YYZ', 'YVR', ['YYZ', 'JFK', 'LAX', 'YVR']],
			['YYZ', 'ORD', []],
		];
		const validate = ([origin, destination, expectedNodes]) => {
			const results = pathCalculator.calculate({ origin, destination });
			if (!isShortestPathNodes(results)) {
				throw new Error(`${origin} > ${destination} did not produce a route.`);
			}
			expect(results.nodes).to.deep.equal(expectedNodes);
		};
		matrix.forEach(validate);
	});	
	
});