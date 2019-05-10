package com.guestlogix.takehometest.resource;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.guestlogix.takehometest.algorithm.RouteService;
import com.guestlogix.takehometest.model.Airport;

@RequestMapping("/route")
@RestController
public class RouteResource {

	@Autowired
	private RouteService routeService;

	@GetMapping
	public List<Airport> retrieveBestRoute() {
		return routeService.getAirports();
	}
	
}
