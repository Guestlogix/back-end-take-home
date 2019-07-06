import * as fs from 'fs';
import * as _ from 'lodash';
import * as csv from 'csv-parser';
// Assumption is broken. Don't want to deal with csv flavors.
async function createCollection<T extends object>(filePath: string): Promise<T[]> {
	return new Promise((resolve, reject) => {
		const results = [];
		fs.createReadStream(filePath)
			.pipe(csv())
			.on('data', data => results.push(data))
			.on('end', () => {
				resolve(results);
			}).on('error', err => {
				reject(err);
			});
	});
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
export async function createStore(options: StoreOptions): Promise<IStore> {
	const {
		airlinesCsv, airportsCsv, routesCsv
	} = options;
	const rawAirlines = await createCollection<IRawAirline>(airlinesCsv);
	const airlines: IAirline[] = rawAirlines
		.map(airline => {
			const { Name, Country } = airline;
			return {
				Name, Country,
				TwoDigitCode: airline['2 Digit Code'],
				ThreeDigitCode: airline['3 Digit Code']
			};
		});
	const rawAirports = await createCollection<IRawAirport>(airportsCsv);
	const airports: IAirport[] = rawAirports
		.map(airport => {
			const { Name, City, Country, Latitute, Longitude } = airport;
			return {
				Name, City, Country,
				IATA3: airport['IATA 3'],
				Latitude: parseFloat(Latitute),
				Longitude: parseFloat(Longitude)
			};
		});
	const rawRoutes = await createCollection<IRawRoute>(routesCsv);
	const routes: IRoute[] = rawRoutes
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