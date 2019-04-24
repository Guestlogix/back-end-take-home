package com.guestlogix.calculators;

import com.guestlogix.database.entities.Airport;
import com.guestlogix.database.entities.Route;
import com.guestlogix.services.RouteService;
import org.springframework.stereotype.Component;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

/**
 * This class have any methods required to do all sorts of route calculations.
 *
 * @author Vanderson Assis
 * @since 4/24/2019
 */

@Component
public class RouteCalculator {

    private RouteService routeService;

    public RouteCalculator(RouteService routeService) {
        this.routeService = routeService;
    }

    public List<Route> calculateShortestRoute(Airport origin, Airport destination) {
        List<Route> routes = new ArrayList<>();
        List<Route> routesWithOrigin = this.routeService.findByOriginAirportId(origin.getId());

        if(routesWithOrigin.size() <= 0) return routes; //If no routes with the specified origin has been found, then fail fast and return an empty array of routes.

        //Searching if there's any route straight from origin to destination with no connections.
        routes.addAll(this.calculateShortestRouteWithoutConnections(routesWithOrigin, origin, destination));
        if(routes.size() > 0)
            return routes;

        //Since there was no routes without connections on the above portion, then immediately return the bellow result.
        return this.calculateShortestRouteWithConnections(routesWithOrigin, destination);
    }

    private List<Route> calculateShortestRouteWithConnections(List<Route> routesWithOrigin, Airport destination) {
        List<Route> routes = new ArrayList<>();
        List<Route> routesWithDestination = this.routeService.findByDestinationAirportId(destination.getId());



        return routes;
    }

    private List<Route> calculateShortestRouteWithoutConnections(List<Route> routesWithOrigin, Airport origin, Airport destination) {
        List<Route> routes = new ArrayList<>();
        Optional<Route> anyRouteWithNoConnections = routesWithOrigin.stream().filter(r -> r.getDestinationAirport().getId() == destination.getId()).findAny();
        anyRouteWithNoConnections.ifPresent(routes::add);

        return routes;
    }

}
