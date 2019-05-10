package com.guestlogix.takehometest.service;

public class PathDto {

	private String path;
	private int flights;

	private PathDto(String path, int flights) {
		this.path = path;
		this.flights = flights;
	}

	public static PathDto create(String path, int flights) {
		return new PathDto(path, flights);
	}

	public String getPath() {
		return path;
	}

	public int getFlights() {
		return flights;
	}

}
