package com.guestlogix.resources;

import com.guestlogix.calculators.RouteCalculator;
import com.guestlogix.database.entities.Airport;
import com.guestlogix.database.entities.Route;
import com.guestlogix.services.AirportService;
import com.guestlogix.services.RouteService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

/**
 * This endpoint will have all the methods related to routes.
 *
 * @author Vanderson Assis
 * @since 4/22/2019
 */

@RestController
@RequestMapping("/route")
public class RouteResource {
    @Autowired
    private AirportService airportService;
    @Autowired
    private RouteService routeService;

    /**
     * Calculates the shortest route based on the origin and destination airport parameters.
     * @param originAirportIataCode the origin airport code to be used in the calculus
     * @param destinationAirportIataCode the destination airport code to be used in the calculus
     * @return
     */
    @GetMapping("/calculate/{origin}/{destination}")
    public String calculate(@PathVariable("origin") String originAirportIataCode, @PathVariable("destination") String destinationAirportIataCode) {
        Airport originAirport = this.airportService.findByIataThree(originAirportIataCode);
        Airport destinationAirport = this.airportService.findByIataThree(destinationAirportIataCode);


        List<Route> shortestRoute = new RouteCalculator(this.routeService).calculateShortestRoute(originAirport, destinationAirport);

        return originAirportIataCode + " " + destinationAirportIataCode;
    }

}
