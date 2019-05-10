package com.guestlogix.takehometest.service;

import java.util.List;
import java.util.PriorityQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.guestlogix.takehometest.model.Airport;
import com.guestlogix.takehometest.model.Route;
import com.guestlogix.takehometest.utils.DataUtils;

@Service
public class DataService {

	private Logger logger = LoggerFactory.getLogger(DataService.class);
	
	private int LIMIT_OF_POSSIBLE_PATHS = 10;

	@Autowired
	private List<Airport> airportList;

	@Autowired
	private List<Route> routeList;

	private int routesFound;
	private PriorityQueue<Airport> airportTaken = new PriorityQueue<>();
	private Node shorterPath;

	public Node generatePath(String origin, String dest) {
		Airport airportDest = DataUtils.retrieveAirport(dest, airportList);
		Airport airportOrigin = DataUtils.retrieveAirport(origin, airportList);
		logger.info("### SEARCHING PATH FROM [{}] TO [{}]", airportOrigin.getIata_3(), airportDest.getIata_3());
		routesFound = 0;
		airportTaken.clear();
		shorterPath = Node.create(airportOrigin);
		airportTaken = new PriorityQueue<>(airportList.size());
		airportTaken.add(airportOrigin);
		findFewerFlightsUntilDest(shorterPath, airportDest);
		logger.info("### FINAL FEWER PATH - {}", DataUtils.showPath(shorterPath));
		return shorterPath;
	}

	private void findFewerFlightsUntilDest(Node node, Airport dest) {
		Airport currentAirport = node.getCurrentAirport();
		logger.debug("### CURRENT AIRPORT IN ".concat(currentAirport.toString()));
		List<Route> closestRoutes = DataUtils.filterRoutesByAirport(currentAirport, routeList);
		for (Route route : closestRoutes) {
			if (route.getDestination().equals(dest)) {
				routesFound++;
				Node newNode = Node.create(node, route.getDestination(), route.getAirline(), node.flightsTaken() + 1);
				shorterPath = newNode;
				logger.info("### ARRIVED AT DESTINATION! - Flights taken={}", newNode.flightsTaken());
				logger.debug("### PATH TO DESTINATION {}", DataUtils.showPath(newNode));
			} else if (!airportTaken.contains(route.getDestination())) {
				if (routesFound > 0) 
					if (!hasShorterPath(node) ||
							routesFound > LIMIT_OF_POSSIBLE_PATHS)
						continue;
				
				Node newNode = Node.create(node, route.getDestination(), route.getAirline(), node.flightsTaken() + 1);
				airportTaken.add(route.getDestination());
				findFewerFlightsUntilDest(newNode, dest);
			} else {
				logger.debug("### AIRPORT ALREADY VISITED - {}", route.getDestination());
			}
		}
	}

	private boolean hasShorterPath(Node node) {
		return null == shorterPath || shorterPath.flightsTaken() > node.flightsTaken() + 1;
	}

}
