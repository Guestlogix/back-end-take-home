import { expect } from 'chai';
describe('sanity check', () => {
	it('should be able to pass a test', () => {
		expect(1 + 1).to.equal(2);
	});
	it('should be able to fail a test', () => {
		expect(1 + 1).to.equal(3);
	});
});