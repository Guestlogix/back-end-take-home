import * as fs from 'fs';
import * as _ from 'lodash';
// Assumption: we're trusting the csv is well formatted.
function readLines(filePath: string): string[] {
	return fs.readFileSync(filePath, 'utf8').split(/\r?\n/g);
}
function recordConstructor<T extends object>(headerLine: string) {
	const fields = headerLine.split(',');
	return function (dataLine: string) {
		const values = dataLine.split(',');
		return fields.reduce((record, field, index) => {
			return {
				[field]: values[index],
				...record
			};
		}, {}) as T;
	};
}

function createCollection<T extends object>(filePath: string): T[] {
	const [header, ...data] = readLines(filePath);
	const makeRecord = recordConstructor<T>(header);
	return data.map(makeRecord);
}
interface IRawRoute {
	['Airline Id']: string;
	Origin: string;
	Destination: string;
}
interface IRawAirport {
	Name: string;
	City: string;
	Country: string;
	['IATA 3']: string;
	Latitute: string;
	Longitude: string;
}
interface IRawAirline {
	Name: string;
	['2 Digit Code']: string;
	['3 Digit Code']: string;
	Country: string;
}
export interface IRoute {
	AirlineId: string;
	Origin: string;
	Destination: string;
}
export interface IAirport {
	Name: string;
	City: string;
	Country: string;
	IATA3: string;
	Latitude: number;
	Longitude: number;
}
export interface IAirline {
	Name: string;
	TwoDigitCode: string;
	ThreeDigitCode: string;
	Country: string;
}

export interface IStore {
	/**
	 * Airlines, keyed by 2 Digit Code
	 */
	airlines: Record<string, IAirline>;
	/**
	 * Airports, keyed by IATA 3
	 */
	airports: Record<string, IAirport>;
	routes: IRoute[];
}
export interface StoreOptions {
	airlinesCsv: string;
	airportsCsv: string;
	routesCsv: string;
}
export function Store(options: StoreOptions): IStore {
	const {
		airlinesCsv, airportsCsv, routesCsv
	} = options;
	const airlines: IAirline[] = createCollection<IRawAirline>(airlinesCsv)
		.map(airline => {
			const { Name, Country } = airline;
			return {
				Name, Country,
				TwoDigitCode: airline['2 Digit Code'],
				ThreeDigitCode: airline['3 Digit Code']
			};
		});
	const airports: IAirport[] = createCollection<IRawAirport>(airportsCsv)
		.map(airport => {
			const { Name, City, Country, Latitute, Longitude } = airport;
			return {
				Name, City, Country,
				IATA3: airport['IATA 3'],
				Latitude: parseFloat(Latitute),
				Longitude: parseFloat(Longitude)
			};
		});
	
	const routes: IRoute[] = createCollection<IRawRoute>(routesCsv)
		.map(route => {
			const { Origin, Destination } = route;
			return {
				Origin, Destination,
				AirlineId: route['Airline Id']
			};
		});
	return {
		airlines: _.keyBy(airlines, 'TwoDigitCode'), 
		airports: _.keyBy(airports, 'IATA3'), 
		routes
	};
}