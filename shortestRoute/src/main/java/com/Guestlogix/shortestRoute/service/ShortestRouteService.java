package com.Guestlogix.shortestRoute.service;

import com.Guestlogix.shortestRoute.domain.jpa.Airline;
import com.Guestlogix.shortestRoute.domain.network.response.DbListResponse;
import com.Guestlogix.shortestRoute.exceptions.ApiException;

/**
 * 
 * @author nchopra
 *
 */
public interface ShortestRouteService {

	DbListResponse<Airline> findConnectingAirlines(String origin, String destination) throws ApiException;

}
