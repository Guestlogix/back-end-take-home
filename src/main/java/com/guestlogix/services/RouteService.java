package com.guestlogix.services;

import com.guestlogix.database.entities.Route;
import com.guestlogix.database.repositories.interfaces.IRouteRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

/**
 * This class interfaces any call to the Route repository.
 *
 * @author Vanderson Assis
 * @since 4/24/2019
 */

@Service
public class RouteService {

    @Autowired
    private IRouteRepository routeRepository;

    /**
     * Executes the Route's repository query to fetch all the routes departing from the origin Airport.
     * @param originAirportId the origin Airport id to be used in this query
     * @return a list of flight routes departing from this origin
     */
    public List<Route> findByOriginAirportId(Long originAirportId) {
        return this.routeRepository.findByOriginAirportId(originAirportId);
    }

    /**
     * Executes the Route's repository query to fetch all the routes for the destination Airport.
     * @param destinationAirportId the destination Airport id to be used in this query
     * @return a list of routes for this destination Airport.
     */
    public List<Route> findByDestinationAirportId(Long destinationAirportId) {
        return this.routeRepository.findByDestinationAirportId(destinationAirportId);
    }

}
