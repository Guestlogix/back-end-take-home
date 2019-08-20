package com.Guestlogix.shortestRoute.domain.network.request;

import java.io.Serializable;

import javax.validation.constraints.NotEmpty;
import javax.validation.constraints.NotNull;

/**
 * @author nchopra
 */
public class ShortestRouteRequest implements Serializable {

	private static final long serialVersionUID = -253207988057358728L;

	@NotNull(message = "origin is required!")
	@NotEmpty(message = "origin is required!")
	private String origin;

	@NotNull(message = "destination is required!")
	@NotEmpty(message = "destination is required!")
	private String destination;

	public ShortestRouteRequest() {
		super();
	}

	public ShortestRouteRequest(String origin, String destination) {
		this.origin = origin;
		this.destination = destination;
	}

	public String getOrigin() {
		return origin;
	}

	public void setOrigin(String origin) {
		this.origin = origin;
	}

	public String getDestination() {
		return destination;
	}

	public void setDestination(String destination) {
		this.destination = destination;
	}

}