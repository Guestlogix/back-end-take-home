import { expect } from 'chai';
import { IStore, Store } from '../server/store';
describe('store', () => {
	let store: IStore;
	before(() => {
		const folder = 'test';
		// const folder = 'full';
		store = Store({
			airlinesCsv: `./data/${folder}/airlines.csv`,
			airportsCsv: `./data/${folder}/airports.csv`,
			routesCsv: `./data/${folder}/routes.csv`
		});
	});
	it('is expected that a store parses airlines.csv correctly', () => {
		const expectedAirline = { Name: 'Air Canada', TwoDigitCode: 'AC', ThreeDigitCode: 'ACA', Country: 'Canada' };
		expect(store.airlines['AC']).to.deep.equal(expectedAirline);
	});
	it('is expected that a store parses airport.csv correctly', () => {
		const expectedAirport = { Name: 'John F Kennedy International Airport', City: 'New York', Country: 'United States', IATA3: 'JFK', Latitude: 40.63980103, Longitude: -73.77890015 };
		expect(store.airports['JFK']).to.deep.equal(expectedAirport);
	});
	it('is expected that a store parses routes.csv correctly', () => {
		const firstRoute = store.routes[0];
		expect(firstRoute).to.haveOwnProperty('AirlineId');
		expect(firstRoute).to.haveOwnProperty('Destination');
		expect(firstRoute).to.haveOwnProperty('Origin');
	});
});