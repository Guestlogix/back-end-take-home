package com.Guestlogix.shortestRoute.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import com.Guestlogix.shortestRoute.domain.jpa.Airline;
import com.Guestlogix.shortestRoute.domain.network.response.DbListResponse;
import com.Guestlogix.shortestRoute.service.ShortestRouteService;

/**
 * 
 * @author nchopra
 *
 */
@RestController
public class ShortestRouteController {

	@Autowired
	private ShortestRouteService shortestRouteService;

	@RequestMapping(value = "/shortest-route", method = { RequestMethod.GET })
	public ResponseEntity<?> findConnectingAirlines(@RequestParam("origin") String origin,
			@RequestParam("destination") String destination) {
		DbListResponse<Airline> resp = shortestRouteService.findConnectingAirlines(origin, destination);
		return ResponseEntity.ok(resp);
	}
}
