package com.guestlogix.takehometest;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertNotNull;
import static org.junit.Assert.assertTrue;

import java.util.List;

import org.assertj.core.util.Lists;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.test.context.junit4.SpringRunner;

import com.guestlogix.takehometest.model.Airline;
import com.guestlogix.takehometest.model.Airport;
import com.guestlogix.takehometest.model.Route;
import com.guestlogix.takehometest.utils.DataUtils;

@RunWith(SpringRunner.class)
public class DataUtilsTest {

	@Test
	public void shouldRetrieveAirport() {
		Airport airport = Airport.create("name", "city", "country", "iata_3", "latitute", "longitude");
		Airport airport2 = Airport.create("name2", "city2", "country2", "iata_32", "latitute2", "longitude2");
		List<Airport> airportList = Lists.newArrayList(airport, airport2);
		Airport airportRetrieved = DataUtils.retrieveAirport("iata_3", airportList);
		assertEquals(airport, airportRetrieved);
	}

	@Test
	public void shouldRetrieveAirline() {
		Airline airline = Airline.create("name", "digitalCode2", "digitalCode3", "country");
		Airline airline2 = Airline.create("name2", "digitalCode22", "digitalCode32", "country2");
		Airline airline3 = Airline.create("name3", "digitalCode23", "digitalCode33", "country3");
		List<Airline> airlineList = Lists.newArrayList(airline, airline2, airline3);
		Airline airlineRetrieved = DataUtils.retrieveAirline("digitalCode22", airlineList);
		assertEquals(airline2, airlineRetrieved);
	}

	@Test
	public void shouldRetrieveRoute() {
		Airline airline = Airline.create("name", "digitalCode2", "digitalCode3", "country");
		Airport airportOrigin = Airport.create("name1", "city1", "country1", "iata_31", "latitute1", "longitude1");
		Airport airportDest = Airport.create("name2", "city2", "country2", "iata_32", "latitute2", "longitude2");
		Route route1 = Route.create(airline, airportOrigin, airportDest);
		Airline airline2 = Airline.create("name2", "digitalCode2", "digitalCode3", "country");
		Airport airportDest2 = Airport.create("name4", "city4", "country4", "iata_34", "latitute4", "longitude4");
		Route route2 = Route.create(airline2, airportOrigin, airportDest2);
		List<Route> routeList = Lists.newArrayList(route1, route2);
		List<Route> routeListRetrieved = DataUtils.filterRoutesByAirport(airportOrigin, routeList);
		assertNotNull(routeListRetrieved);
		assertTrue(routeListRetrieved.contains(route1));
		assertTrue(routeListRetrieved.contains(route2));
	}

}
