package com.guestlogix.takehometest;

import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertNotNull;
import static org.junit.Assert.assertTrue;

import java.util.List;

import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

import com.guestlogix.takehometest.model.Airline;
import com.guestlogix.takehometest.model.Airport;
import com.guestlogix.takehometest.model.Route;
import com.guestlogix.takehometest.utils.DataUtils;

@RunWith(SpringRunner.class)
@SpringBootTest
public class FileConfigTests {

	@Autowired
	private List<Airport> airportList;

	@Autowired
	private List<Airline> airlineList;

	@Autowired
	private List<Route> routeList;

	private Airport airport;

	@Before
	public void start() {
		airport = Airport.create("Yellowknife Airport", "Yellowknife", "Canada", "YZF", "62.46279907", "-114.4400024");
	}

	@Test
	public void shouldLoadAirportFile() {
		assertFalse(airportList.isEmpty());
	}

	@Test
	public void shouldLoadAirlineFile() {
		assertFalse(airlineList.isEmpty());
	}

	@Test
	public void shouldLoadRouteFile() {
		assertFalse(routeList.isEmpty());
	}

	@Test
	public void shouldLoadLastLineOfAirportFile() {
		assertNotNull(DataUtils.retrieveAirport("\\N", airportList));
	}

	@Test
	public void shouldLoadLastLineOfAirlineFile() {
		assertNotNull(DataUtils.retrieveAirline("WS", airlineList));
	}

	@Test
	public void shouldLoadLastLineOfRoutes() {
		List<Route> routes = DataUtils.filterRoutesByAirport(airport, routeList);
		boolean lastLineLoaded = routes.stream().anyMatch(route -> route.getDestination().getIata_3().equals("YEG"));
		assertTrue(lastLineLoaded);
	}

}
