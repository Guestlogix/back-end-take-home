package com.guestlogix.takehometest.algorithm;

import java.util.List;

import com.guestlogix.takehometest.model.Airport;
import com.guestlogix.takehometest.model.Route;

public class Node {

	private Node parent;
	private Airport airport;
	private List<Route> routeList;

	private boolean visited;

	private Node (Airport airport, List<Route> routeList) {
		this.airport = airport;
		this.routeList = routeList;
	}

	public static Node create(Airport airport, List<Route> routeList) {
		return new Node(airport, routeList);
	}

	@Override
	public String toString() {
		return "[Origin="
				.concat(airport.getIata_3())
				.concat("]");
	}
}
