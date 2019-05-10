package com.guestlogix.takehometest.algorithm;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.guestlogix.takehometest.model.Airport;

@Service
public class RouteService {

	@Autowired
	private DataService dataService;

	public List<Airport> getAirports() {
		return null;
	}
}
