import { expect } from 'chai';
import { ShortestPathCalculator, IShortestPathCalculator, ShortestPathError } from '../shared/shortest-path';
import { Store } from '../server/store';
describe('shortest path', () => {
	let pathCalculator: IShortestPathCalculator;
	before(() => {
		let store = Store({
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
			expect(result).to.equal(expected, `expected ${result} to match ShortestPathError.InvalidOrigin (${expected})`);
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
			expect(result).to.equal(expected, `expected ${result} to match ShortestPathError.InvalidDestination (${expected})`);
		}
	});
	it('is expected to detect if both origin and destination are invalid', () => {
		const invalidParams = [
			{}, { origin: '', destination: '' },
			{ origin: null, destination: null },
			{ origin: 'JFK', destination: 'JFK' },
			{ origin: 'XXX', destination: 'XXX' },
		];
		const expected = ShortestPathError.InvalidDestination;
		const results = invalidParams.map(pathCalculator.calculate);
		let i;
		for (i = 0; i < results.length; ++i) {
			const result = results[i];
			expect(result).to.equal(expected, `expected ${result}[${i}] to match ShortestPathError.Invalid (${expected})`);
		}
	});
	it.skip('is expected to calculate the route', () => {
		// TODO: expand the expectations here.
	});
});