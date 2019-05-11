package com.guestlogix.takehometest.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.guestlogix.takehometest.exception.ParametersNotFoundException;
import com.guestlogix.takehometest.utils.DataUtils;

@Service
public class RouteService {

	@Autowired
	private DataService dataService;

	public PathDto retrieveBestRoute(String origin, String dest) throws ParametersNotFoundException {
		Node fewerPath = dataService.generatePath(origin, dest);
		return PathDto.create(DataUtils.showPath(fewerPath), fewerPath.flightsTaken());
	}

}
