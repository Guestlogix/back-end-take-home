package com.guestlogix.takehometest;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertNotNull;
import static org.junit.Assert.assertTrue;

import java.util.List;

import org.assertj.core.util.Lists;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.test.context.junit4.SpringRunner;

import com.guestlogix.takehometest.model.Airline;
import com.guestlogix.takehometest.model.Airport;
import com.guestlogix.takehometest.model.Route;
import com.guestlogix.takehometest.utils.DataUtils;

@RunWith(SpringRunner.class)
public class DataUtilsTest {

	private Airport airport;
	private Airport airport2;
	private Airport airport3;
	private Airline airline;
	private Airline airline2;
	private Airline airline3;

	@Before
	public void start() {
		airport = Airport.create("name", "city", "country", "iata_3", "latitute", "longitude");
		airport2 = Airport.create("name2", "city2", "country2", "iata_32", "latitute2", "longitude2");
		airport3 = Airport.create("name3", "city3", "country3", "iata_33", "latitute3", "longitude3");
		airline = Airline.create("name", "digitalCode2", "digitalCode3", "country");
		airline2 = Airline.create("name2", "digitalCode22", "digitalCode32", "country2");
		airline3 = Airline.create("name3", "digitalCode23", "digitalCode33", "country3");
	}

	@Test
	public void shouldRetrieveAirport() {
		List<Airport> airportList = Lists.newArrayList(airport, airport2);
		Airport airportRetrieved = DataUtils.retrieveAirport("iata_3", airportList);
		assertEquals(airport, airportRetrieved);
	}

	@Test
	public void shouldRetrieveAirline() {
		List<Airline> airlineList = Lists.newArrayList(airline, airline2, airline3);

		Airline airlineRetrieved = DataUtils.retrieveAirline("digitalCode22", airlineList);

		assertEquals(airline2, airlineRetrieved);
	}

	@Test
	public void shouldRetrieveRoute() {
		Route route1 = Route.create(airline, airport, airport2);
		Route route2 = Route.create(airline2, airport, airport3);
		List<Route> routeList = Lists.newArrayList(route1, route2);

		List<Route> routeListRetrieved = DataUtils.filterRoutesByAirport(airport, routeList);

		assertNotNull(routeListRetrieved);
		assertTrue(routeListRetrieved.contains(route1));
		assertTrue(routeListRetrieved.contains(route2));
	}

}
