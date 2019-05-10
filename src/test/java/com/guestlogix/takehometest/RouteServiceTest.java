package com.guestlogix.takehometest;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertNotNull;
import static org.junit.Assert.assertTrue;
import static org.mockito.ArgumentMatchers.anyString;
import static org.mockito.Mockito.when;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.junit.MockitoJUnitRunner;

import com.guestlogix.takehometest.model.Airline;
import com.guestlogix.takehometest.model.Airport;
import com.guestlogix.takehometest.service.DataService;
import com.guestlogix.takehometest.service.Node;
import com.guestlogix.takehometest.service.PathDto;
import com.guestlogix.takehometest.service.RouteService;

@RunWith(MockitoJUnitRunner.class)
public class RouteServiceTest {

	@InjectMocks
	private RouteService routeService;

	@Mock
	private DataService dataService;

	@Test
	public void shouldRetrieveBestRouteWhenNotRouteFound() {
		Airport airport = Airport.create("name", "city", "country", "iata_3", "latitute", "longitude");
		Node node = Node.create(airport);
		when(dataService.generatePath(anyString(), anyString())).thenReturn(node);
		PathDto path = routeService.retrieveBestRoute("", "");
		assertNotNull(path);
		assertTrue(path.getPath().contains(airport.getIata_3()));
		assertEquals(path.getFlights(), 0);
	}

	@Test
	public void shouldRetrieveBestRouteWhenRouteFound() {
		Airport airport = Airport.create("name2", "city2", "country2", "iata_32", "latitute2", "longitude2");
		Node node = Node.create(airport);
		
		Airline airline = Airline.create("name", "digitalCode2", "digitalCode3", "country");
		Airport airport2 = Airport.create("name", "city", "country", "iata_3", "latitute", "longitude");
		Node node2 = Node.create(node, airport2, airline, 1);
		
		when(dataService.generatePath(anyString(), anyString())).thenReturn(node2);
		PathDto path = routeService.retrieveBestRoute("", "");
		assertNotNull(path);
		assertTrue(path.getPath().contains(airport.getIata_3()));
		assertTrue(path.getPath().contains(airport2.getIata_3()));
		assertEquals(path.getFlights(), 1);
	}

}
