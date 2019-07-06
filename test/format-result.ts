import { expect } from 'chai';
import { formatResult } from '../server/format-result';
import { ShortestPathError } from '../shared/shortest-path';
describe('format result', () => {
	it('is expected to correctly format route results', () => {
		const result2 = {
			nodes: ['YYZ', 'JFK']
		};
		const result4 = {
			nodes: ['YYZ', 'JFK', 'LAX', 'YVR']
		};
		const expected2 = 'YYZ -> JFK';
		const expected4 = 'YYZ -> JFK -> LAX -> YVR';
		expect(formatResult(result2)).to.equal(expected2);
		expect(formatResult(result4)).to.equal(expected4);
	});
	it('is expected to correctly format no route results', () => {
		const result0 = {
			nodes: []
		};
		const expected0 = 'No Route';
		expect(formatResult(result0)).to.equal(expected0);
	});
	it('is expected to print the correct error messages on invalid input', () => {
		const invalid = 'Invalid Origin And Destination';
		const invalidOrigin = 'Invalid Origin';
		const InvalidDestination = 'Invalid Destination';
		expect(formatResult(ShortestPathError.Invalid)).to.equal(invalid);
		expect(formatResult(ShortestPathError.InvalidOrigin)).to.equal(invalidOrigin);
		expect(formatResult(ShortestPathError.InvalidDestination)).to.equal(InvalidDestination);
	});
});