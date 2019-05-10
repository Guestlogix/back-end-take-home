package com.guestlogix.takehometest.algorithm;

import java.util.List;
import java.util.stream.Collectors;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.guestlogix.takehometest.algorithm.filter.DataFilter;
import com.guestlogix.takehometest.model.Airport;
import com.guestlogix.takehometest.model.Route;

@Service
public class DataService {

	@Autowired
	private List<Airport> airportList;

	@Autowired
	private List<Route> routeList;

	public List<Route> createGraph(String origin) {
		Airport airportOrigin = DataFilter.filterAirport(origin, airportList);
		List<Route> adjacentRoutes = routeList.parallelStream()
				.filter(r -> r.getOrigin().equals(airportOrigin))
				.collect(Collectors.toList());
		Node node = Node.create(airportOrigin, routeList);
		return adjacentRoutes;
	}

	
}
