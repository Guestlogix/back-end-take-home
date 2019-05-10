package com.guestlogix.takehometest.config;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import com.guestlogix.takehometest.model.Airline;
import com.guestlogix.takehometest.model.Airport;
import com.guestlogix.takehometest.model.Route;

@Configuration
public class FilesConfig {

	private String basePath = System.getProperty("user.dir") + "//data//";

	@Bean
	public List<Airport> loadAirports() throws IOException {
		List<Airport> airportList = new ArrayList<>();
		BufferedReader csvReader = new BufferedReader(new FileReader(basePath.concat("airports.csv")));
		String row = null;
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
		BufferedReader csvReader = new BufferedReader(new FileReader(basePath.concat("airlines.csv")));
		String row = null;
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
	public List<Route> loadRoutes() throws IOException {
		List<Route> routeList = new ArrayList<>();
		BufferedReader csvReader = new BufferedReader(new FileReader(basePath.concat("routes.csv")));
		String row = null;
		while ((row = csvReader.readLine()) != null) {  
			String[] data = row.split(",");
			String airlineId = data[0];
			String origin = data[1];
			String destination = data[2];
			routeList.add(Route.create(airlineId, origin, destination));
		}
		csvReader.close();
		return routeList;
	}
}
