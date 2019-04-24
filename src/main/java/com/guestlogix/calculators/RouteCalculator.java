package com.guestlogix.calculators;

import com.guestlogix.database.entities.Airport;
import com.guestlogix.database.entities.Route;
import com.guestlogix.services.RouteService;
import com.guestlogix.vos.KeyValueVo;
import org.springframework.stereotype.Component;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

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

    /**
     * This method calculates the shortest path to the destinationAirport Airport
     * @param originAirport where the user is departing
     * @param destinationAirport where the user wants to go
     * @return a KeyValueVo containing the quantity of connections (if required) and the list of routes to the destination Airport
     */
    public KeyValueVo<Integer, List<Route>> calculateShortestRoute(Airport originAirport, Airport destinationAirport) {
        //This solution is not the best one, but with the time running short, it's the only one I could come up with. I do believe that for this problem a recursive method or a tree would be better.
        //Even if I get rejected, I'll improve this code anyway, knowledge is never too much! =}

        KeyValueVo<Integer, List<Route>> routesPair = new KeyValueVo<>(0, new ArrayList<>());
        List<Route> routesWithOrigin = this.routeService.findByOriginAirportId(originAirport.getId());

        if(routesWithOrigin.size() <= 0) return routesPair; //If no routes with the specified originAirport has been found, then fail fast and return an empty array of routes.

        //Searching if there's any route straight from originAirport to destinationAirport with no connections.
        routesPair.getValue().addAll((this.calculateShortestRouteWithoutConnections(routesWithOrigin, destinationAirport)));
        if(routesPair.getValue().size() > 0)
            //If the code reached here, it means that there's a Straight route to the user's destination
            return routesPair;

        //Since there was no routes without connections on the above portion, then immediately return the bellow result.
        return this.calculateShortestRouteWithConnections(originAirport, destinationAirport);
    }

    /**
     * This method calculates the shortest path with connections for the user's destination.
     * @param destinationAirport the destination Airport (where the user wants to go)
     * @return a KeyValueVo containing the quantity of connections and the list of routes to the destination
     */
    private KeyValueVo<Integer, List<Route>> calculateShortestRouteWithConnections(Airport originAirport, Airport destinationAirport) {
        KeyValueVo<Integer, List<Route>> routesPair = new KeyValueVo<>(0, new ArrayList<>());

        //A list of routes that has the same destinationAirport as the one that the user requires.
        List<Route> routesWithUserRequiredDestination = this.routeService.findByDestinationAirportId(destinationAirport.getId());

        //A list of origin airports from the above destinations' list.
        List<Airport> originAirportsForUserRequiredDestination = routesWithUserRequiredDestination.stream().map(Route::getOriginAirport).collect(Collectors.toList());

        //The bellow list will have all the routes for all the origin Airports for the list that has all the routes for the user required destination
        List<Route> routesWithDestinationsForTheOrigins = new ArrayList<>();
        try {
            originAirportsForUserRequiredDestination.forEach(a -> routesWithDestinationsForTheOrigins.addAll(this.routeService.findByDestinationAirportId(a.getId())));
        } catch(NullPointerException ex) { //Didn't want to add thi catch here, but time is running short! And I can't make it more elegant
            ex.printStackTrace();
            return routesPair;
        }

        for(Route route : routesWithDestinationsForTheOrigins) {
            if(route.getOriginAirport().getId() == originAirport.getId()) {
                //If code reached here, it means there is a 1 connection flight to the user's destination.
                routesPair.setKey(1); //One connection
                routesPair.getValue().add(route);
                Route secondRoute = routesWithUserRequiredDestination.stream().filter(r -> r.getOriginAirport().getId() == route.getDestinationAirport().getId()).collect(Collectors.toList()).get(0);
                routesPair.getValue().add(secondRoute);
                break;
            }
        }

        return routesPair;
    }

    /**
     * This method will run algorithms to find if there's any route without any connection.
     * @param routesWithOrigin a list of routes pre-fetched that has the same origin as the user's request.
     * @param destination the destination to which the user wants to go
     * @return a list of routes created for the user's request
     */
    private List<Route> calculateShortestRouteWithoutConnections(List<Route> routesWithOrigin, Airport destination) {
        List<Route> routes = new ArrayList<>();
        Optional<Route> anyRouteWithNoConnections = routesWithOrigin.stream().filter(r -> r.getDestinationAirport().getId() == destination.getId()).findAny();
        anyRouteWithNoConnections.ifPresent(routes::add);

        return routes;
    }

}
