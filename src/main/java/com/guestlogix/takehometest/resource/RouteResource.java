package com.guestlogix.takehometest.resource;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import com.guestlogix.takehometest.service.PathDto;
import com.guestlogix.takehometest.service.RouteService;

@RequestMapping("/route")
@RestController
public class RouteResource {

	@Autowired
	private RouteService routeService;

	@GetMapping
	public PathDto retrieveBestRoute(@RequestParam(name = "origin") String origin, @RequestParam(name = "dest") String dest) {
		return routeService.retrieveBestRoute(origin, dest);
	}
	
}
