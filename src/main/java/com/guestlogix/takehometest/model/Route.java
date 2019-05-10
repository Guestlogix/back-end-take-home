package com.guestlogix.takehometest.model;

public class Route {

	private Airline airline;
	private Airport origin;
	private Airport destination;

	private Route(Airline airline, Airport origin, Airport destination) {
		super();
		this.airline = airline;
		this.origin = origin;
		this.destination = destination;
	}

	public Airline getAirline() {
		return airline;
	}

	public Airport getOrigin() {
		return origin;
	}

	public Airport getDestination() {
		return destination;
	}

	public static Route create(Airline airline, Airport origin, Airport destination) {
		return new Route(airline, origin, destination);
	}

}
