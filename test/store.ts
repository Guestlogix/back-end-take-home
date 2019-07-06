import { expect } from 'chai';
import { IStore, Store } from '../server/store';
describe('store', () => {
	let store: IStore;
	before(() => {
		store = Store({
			airlinesCsv: './data/test/airlines.csv',
			airportsCsv: './data/test/airports.csv',
			routesCsv: './data/test/routes.csv'
		});
	});
	it('is expected that a store parses airlines.csv correctly', () => {
		const expectedAirline = { Name: 'Air Canada', TwoDigitCode: 'AC', ThreeDigitCode: 'ACA', Country: 'Canada' };
		expect(store.airlines[0]).to.deep.equal(expectedAirline);
	});
	it('is expected that a store parses airport.csv correctly', () => {
		const expectedAirport = { Name: 'John F Kennedy International Airport', City: 'New York', Country: 'United States', IATA3: 'JFK', Latitute: 40.63980103, Longitude: -73.77890015 };
		expect(store.airports[0]).to.deep.equal(expectedAirport);
	});
	it('is expected that a store parses routes.csv correctly', () => {
		const expectedRoute = { AirlineId: 'AC', Origin: 'YYZ', Destination: 'JFK' };
		expect(store.routes[0]).to.deep.equal(expectedRoute);
	});
});