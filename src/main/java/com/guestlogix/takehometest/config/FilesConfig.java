package com.guestlogix.takehometest.config;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.nio.charset.StandardCharsets;
import java.util.ArrayList;
import java.util.List;
import java.util.Objects;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.core.io.ClassPathResource;

import com.guestlogix.takehometest.model.Airline;
import com.guestlogix.takehometest.model.Airport;
import com.guestlogix.takehometest.model.Route;
import com.guestlogix.takehometest.utils.DataUtils;

@Configuration
public class FilesConfig {

	private String basePath = "data/";

	@Bean
	public List<Airport> loadAirports() throws IOException {
		List<Airport> airportList = new ArrayList<>();
		BufferedReader csvReader = getCsvBufferedReader("airports.csv");
		String row = csvReader.readLine();
		while ((row = csvReader.readLine()) != null) {  
			String[] data = row.split(",");
			String name = data[0];
			String city = data[1];
			String country = data[2];
			String iata_3 = data[3];
			String latitute = data[4];
			String longitude = data[5];
			airportList.add(Airport.create(name, city, country, iata_3, latitute, longitude));
		}
		csvReader.close();
		return airportList;
	}

	@Bean
	public List<Airline> loadAirlines() throws IOException {
		List<Airline> airlinetList = new ArrayList<>();
		BufferedReader csvReader = getCsvBufferedReader("airlines.csv");
		String row = csvReader.readLine();
		while ((row = csvReader.readLine()) != null) {  
			String[] data = row.split(",");
			String name = data[0];
			String digitalCode2 = data[1];
			String digitalCode3 = data[2];
			String country = data[3];
			airlinetList.add(Airline.create(name, digitalCode2, digitalCode3, country));
		}
		csvReader.close();
		return airlinetList;
	}

	@Bean
	public List<Route> loadRoutes(List<Airline> airlineList, List<Airport> airportList) throws IOException {
		List<Route> routeList = new ArrayList<>();
		BufferedReader csvReader = getCsvBufferedReader("routes.csv");
		String row = csvReader.readLine();
		while ((row = csvReader.readLine()) != null) {  
			String[] data = row.split(",");
			String airlineId = data[0];
			String origin = data[1];
			String destination = data[2];
			Airline airline = DataUtils.retrieveAirline(airlineId, airlineList);
			Airport airportOrigin = DataUtils.retrieveAirport(origin, airportList);
			Airport airportDest = DataUtils.retrieveAirport(destination, airportList);
			if (Objects.nonNull(airline) &&
					Objects.nonNull(airportOrigin) &&
					Objects.nonNull(airportDest))
			routeList.add(Route.create(airline, airportOrigin, airportDest));
		}
		csvReader.close();
		return routeList;
	}

	private BufferedReader getCsvBufferedReader(final String file) throws IOException {
		ClassPathResource resource = new ClassPathResource(basePath.concat(file));
		InputStream inputStream = resource.getInputStream();
		InputStreamReader streamReader = new InputStreamReader(inputStream, StandardCharsets.UTF_8);
		BufferedReader csvReader = new BufferedReader(streamReader);
		return csvReader;
	}

}
