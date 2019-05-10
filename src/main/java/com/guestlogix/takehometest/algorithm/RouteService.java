package com.guestlogix.takehometest.algorithm;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.guestlogix.takehometest.model.Route;

@Service
public class RouteService {

	@Autowired
	private DataService dataService;

	public List<Route> retrieveBestRoute(String origin, String dest) {
		return dataService.createGraph(origin);
	}
}
