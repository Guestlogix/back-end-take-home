package com.guestlogix.takehometest.model;

public class Route {

	private String airlineId;
	private String origin;
	private String destination;

	private Route(String airlineId, String origin, String destination) {
		super();
		this.airlineId = airlineId;
		this.origin = origin;
		this.destination = destination;
	}

	public String getAirlineId() {
		return airlineId;
	}

	public String getOrigin() {
		return origin;
	}

	public String getDestination() {
		return destination;
	}

	public static Route create(String airlineId, String origin, String destination) {
		return new Route(airlineId, origin, destination);
	}

}
