package com.guestlogix.takehometest.algorithm;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.guestlogix.takehometest.model.Airline;
import com.guestlogix.takehometest.model.Airport;
import com.guestlogix.takehometest.model.Route;

@Service
public class DataService {

	@Autowired
	private List<Airport> airportList;

	@Autowired
	private List<Airline> airlineList;

	@Autowired
	private List<Route> routeList;

}
