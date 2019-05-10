package com.guestlogix.takehometest.utils;

import java.util.List;
import java.util.stream.Collectors;

import com.guestlogix.takehometest.model.Airline;
import com.guestlogix.takehometest.model.Airport;
import com.guestlogix.takehometest.model.Route;
import com.guestlogix.takehometest.service.Node;

public class DataUtils {

	public static Airport retrieveAirport(String airportId, List<Airport> airportList) {
		return airportList.parallelStream()
							.filter(x -> airportId.equals(x.getIata_3()))
							.findFirst()
							.orElse(null);
	}

	public static Airline retrieveAirline(String airlineId, List<Airline> airlineList) {
		return airlineList.parallelStream()
				.filter(x -> airlineId.equals(x.getDigitalCode2()))
				.findFirst()
				.orElse(null);
	}

	public static List<Route> filterRoutesByAirport(Airport airport, List<Route> routeList) {
		return routeList.parallelStream()
				.filter(x -> airport.equals(x.getOrigin()))
				.collect(Collectors.toList());
	}

	public static String showPath(Node lastNode) {
		return printPath(lastNode);
	}

	private static String printPath(Node node) {
		return null != node.getParent() ? printPath(node.getParent()).concat(formatText(node)) : formatText(node);
	}

	private static String formatText(Node node) {
		String airline = "";
		if (null != node.getAirline()) {
			airline = "(".concat(node.getAirline().getDigitalCode2()).concat(")").concat("->");
		}
		return airline.concat(node.getCurrentAirport().getIata_3());
	}

}
