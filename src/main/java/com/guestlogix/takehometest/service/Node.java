package com.guestlogix.takehometest.service;

import com.guestlogix.takehometest.model.Airline;
import com.guestlogix.takehometest.model.Airport;

public class Node {

	private Node parent;
	private Airport airport;
	private Airline airline;
	private int flights = 0;

	private Node (Airport airport) {
		this.airport = airport;
	}

	private Node (Node parent, Airport airport, Airline airline, int flights) {
		this(airport);
		this.parent = parent;
		this.airline = airline;
		this.flights = flights;
	}

	public static Node create(Airport airport) {
		return new Node(airport);
	}

	public static Node create(Node parent, Airport airport, Airline airline, int flights) {
		return new Node(parent, airport, airline, flights);
	}

	public Node getParent() {
		return this.parent;
	}

	public boolean isGoal(Airport dest) {
		return this.airport.equals(dest);
	}

	public int flightsTaken() {
		return this.flights;
	}

	public Airport getCurrentAirport() {
		return this.airport;
	}

	public Airline getAirline() {
		return airline;
	}

	@Override
	public String toString() {
		return "[Origin="
				.concat(airport.getIata_3())
				.concat("]");
	}
}
