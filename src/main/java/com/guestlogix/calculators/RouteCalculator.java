package com.guestlogix.calculators;

import com.guestlogix.calculators.tree.RouteTree;
import com.guestlogix.database.entities.Airport;
import com.guestlogix.database.entities.Route;
import com.guestlogix.services.RouteService;
import com.guestlogix.utils.RouteCalculusStatus;
import com.guestlogix.vos.KeyValueVo;
import org.springframework.stereotype.Component;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

import static com.guestlogix.utils.RouteCalculusStatus.*;

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
     * This method calculates the shortest path to the destinationAirport Airport using a Tree data structure. This is fast and can calculate any number of connections as
     * far as there are any flight.
     * @param originAirport where the user is departing
     * @param destinationAirport where the user wants to go
     * @return a KeyValueVo containing the quantity of connections and the list of routes to the destination Airport
     */
    public KeyValueVo<Integer, List<Route>> calculateShortestRouteTree(Airport originAirport, Airport destinationAirport) {
        List<Route> routesWithOrigin = this.routeService.findByOriginAirportId(originAirport.getId());

        RouteTree routeTree = new RouteTree(this.routeService, originAirport, destinationAirport, routesWithOrigin);
        List<Route> routes = routeTree.findConnectingRoutes();
        return new KeyValueVo<>(routes.size() - 1, routes);
    }

    /**
     * This method calculates the shortest path to the destinationAirport Airport
     * @param originAirport where the user is departing
     * @param destinationAirport where the user wants to go
     * @return a KeyValueVo containing the quantity of connections (if required) and the list of routes to the destination Airport
     */
    public KeyValueVo<Integer, List<Route>> calculateShortestRouteRecursive(Airport originAirport, Airport destinationAirport) {

        //======================WARNING======================
        //This method has not been finished, I just kept it here so you can see (if you want) where I was going with it.

        KeyValueVo<Integer, List<Route>> routesPair = new KeyValueVo<>(0, new ArrayList<>());
        List<Route> routesWithOrigin = this.routeService.findByOriginAirportId(originAirport.getId());

        if(routesWithOrigin.size() <= 0) return routesPair; //If no routes with the specified originAirport has been found, then fail fast and return an empty array of routes.

        KeyValueVo<Integer, List<Route>> connectingRoutesFound = new KeyValueVo<>(-1, new ArrayList<>());
        connectingRoutesFound.getValue().addAll(routesWithOrigin);

        return this.calculateShortestRouteRecursively(destinationAirport, connectingRoutesFound);
    }

    /**
     * Calculates the shortest route to the user's destination using a recursive approach. This algorithm was created for experimenting only, it's not finished.
     * @param destinationAirport the airport the user wants to get to
     * @param connectingRoutesFound the routes that was found in the previous recursive execution
     * @return a KeyValueVo containing the number of connections and the list of routes
     */
    private KeyValueVo<Integer, List<Route>> calculateShortestRouteRecursively(Airport destinationAirport, KeyValueVo<Integer, List<Route>> connectingRoutesFound) {

        //======================WARNING======================
        //This method has not been finished, I just kept it here so you can see (if you want) where I was going with it.

        KeyValueVo<Integer, List<Route>> routesFound = new KeyValueVo<>(0, new ArrayList<>());

        if(connectingRoutesFound.getValue().size() == 0) return routesFound; //If invalid parameter, then fail fast empty routes.

        RouteCalculusStatus routeCalculusStatus = KEEP_TRYING;

        //By the end of this method the connection's number will increase (upon success of finding a route or not), so, increase it right away.
        routesFound.setKey(routesFound.getKey() + 1);
        List<Route> routes = new ArrayList<>();

        masterLoop:
        for(Route connectingRoute : connectingRoutesFound.getValue()) {
            //Loading routes with origin on the destination of this last route found (making the connections here)
            routes.addAll(this.routeService.findByOriginAirportId(connectingRoute.getDestinationAirport().getId()));

            //If no connection whatsoever found, then break the loop and inform the user no route has been found for his flight. Then clear any previous data
            //that may be in the connectingRoutesFound's list and stop trying to find a route.
            if(routes.size() == 0) {
                routesFound.getValue().clear();
                routeCalculusStatus = NOT_FOUND;
                break masterLoop;
            }

            for(Route lastRoute : routes) {
                try {
                    if(lastRoute.getDestinationAirport().getId().equals(destinationAirport.getId())) {
                        //In this portion of the code a lastRoute has been found
                        routesFound.getValue().add(connectingRoute);
                        routesFound.getValue().add(lastRoute);
                        routeCalculusStatus = FOUND;
                        break masterLoop;
                    }
                } catch(NullPointerException ex) {
                    //There are some routes with no airports information, so just suppress them and keep trying.
                }
            }
        }

        //If no route found but the algorithm "thinks" there's still places to try, the do it! =}
        if(routeCalculusStatus == KEEP_TRYING) {
            //Even though these routes don't have the destination the user needs to go, they do have connection with from the user is departing.
            //So add them on the lastRoutes found and recursively call this method to go one level deeper on the search.
            connectingRoutesFound.getValue().addAll(routes);
            return calculateShortestRouteRecursively(destinationAirport, connectingRoutesFound);
        }

        return routesFound;
    }

}
