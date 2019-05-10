package com.guestlogix.takehometest.algorithm.filter;

import java.util.List;

import com.guestlogix.takehometest.model.Airline;
import com.guestlogix.takehometest.model.Airport;

public class DataFilter {

	public static Airport filterAirport(String airportId, List<Airport> airportList) {
		return airportList.parallelStream()
							.filter(x -> airportId.equals(x.getIata_3()))
							.findFirst()
							.orElse(null);
	}

	public static Airline filterAirline(String airlineId, List<Airline> airlineList) {
		return airlineList.parallelStream()
				.filter(x -> airlineId.equals(x.getDigitalCode2()))
				.findFirst()
				.orElse(null);
	}

}
