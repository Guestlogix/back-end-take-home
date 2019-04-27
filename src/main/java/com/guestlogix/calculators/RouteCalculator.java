package com.guestlogix.calculators;

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
     * This method calculates the shortest path to the destinationAirport Airport
     * @param originAirport where the user is departing
     * @param destinationAirport where the user wants to go
     * @return a KeyValueVo containing the quantity of connections (if required) and the list of routes to the destination Airport
     */
    public KeyValueVo<Integer, List<Route>> calculateShortestRoute(Airport originAirport, Airport destinationAirport) {
        KeyValueVo<Integer, List<Route>> routesPair = new KeyValueVo<>(0, new ArrayList<>());
        List<Route> routesWithOrigin = this.routeService.findByOriginAirportId(originAirport.getId());

        if(routesWithOrigin.size() <= 0) return routesPair; //If no routes with the specified originAirport has been found, then fail fast and return an empty array of routes.

        KeyValueVo<Integer, List<Route>> connectingRoutesFound = new KeyValueVo<>(-1, new ArrayList<>());
        connectingRoutesFound.getValue().addAll(routesWithOrigin);

        return this.calculateShortestRouteRecursively(destinationAirport, connectingRoutesFound);
    }

    /**
     * Calculates the shortest route to the user's destination using a recursive approach.
     * @param destinationAirport the airport the user wants to get to
     * @param connectingRoutesFound the routes that was found in the previous recursive execution
     * @return a KeyValueVo containing the number of connections and the list of routes
     */
    private KeyValueVo<Integer, List<Route>> calculateShortestRouteRecursively(Airport destinationAirport, KeyValueVo<Integer, List<Route>> connectingRoutesFound) {
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

    public KeyValueVo<Integer, List<Route>> calculateShortestRouteBrute(Airport originAirport, Airport destinationAirport) {
        //This solution is not the best one, but with the time running short, it's the only one I could come up with. I do believe that for this problem a recursive method or a
        //tree would be better. Even if I get rejected, I'll keep improving this code anyways, knowledge is never too much. But please hire me! =}

        KeyValueVo<Integer, List<Route>> routesPair = new KeyValueVo<>(0, new ArrayList<>());
        List<Route> routesWithOrigin = this.routeService.findByOriginAirportId(originAirport.getId());

        if(routesWithOrigin.size() <= 0) return routesPair; //If no routes with the specified originAirport has been found, then fail fast and return an empty array of routes.

        //Searching if there's any route straight from originAirport to destinationAirport with no connections.
        routesPair.getValue().addAll((this.calculateShortestRouteWithoutConnections(routesWithOrigin, destinationAirport)));
        if(routesPair.getValue().size() > 0)
            //If the code reached here, it means that there's a Straight route to the user's destination
            return routesPair;

        //Since there was no routes without connections on the above portion, then immediately return the bellow result.

        KeyValueVo<Integer, List<Route>> lastRoutesFound = new KeyValueVo<>(-1, new ArrayList<>());
        lastRoutesFound.getValue().addAll(routesWithOrigin);

        return this.calculateShortestRoute(routesWithOrigin, destinationAirport);
    }

    /**
     * This method calculates the shortest route to the user's requirement.
     * @param routesWithOrigin routes that originates from the user's origin airport.
     * @param destinationAirport the airport the user wants to arrive.
     * @return a KeyValueVo containing the number of connections and routes
     */
    private KeyValueVo<Integer, List<Route>> calculateShortestRoute(List<Route> routesWithOrigin, Airport destinationAirport) {
        KeyValueVo<Integer, List<Route>> routesFound = new KeyValueVo<>(0, new ArrayList<>());

        if(routesWithOrigin == null || routesWithOrigin.size() == 0) return routesFound; //If invalid parameter, then fail fast empty routes.
        Airport originAirport = routesWithOrigin.get(0).getOriginAirport();//All the routes in this array has the same origin (the user's required origin)

        Optional<Route> routeWithoutConnection = routesWithOrigin.stream().filter(r -> r.getDestinationAirport().getId().equals(destinationAirport.getId())).findAny();
        if(routeWithoutConnection.isPresent()) {
            //Here the route without connection is calculated
            routesFound.getValue().add(routeWithoutConnection.get());
            return routesFound;

        } else {
            //In here all routes with the required destinationAirport (but not the required origin) are loaded in memory.
            //And then, all the routes with destinations to the origins of the routesWithRequiredDestination is loaded (trying to make the first connection with where the user is departing)
            List<Route> routesWithRequiredDestination = this.routeService.findByDestinationAirportId(destinationAirport.getId());
            List<Route> firstLevelRoutes = new ArrayList<>(); //First level routes, are the ones that has one connection to the required destination

            masterLoop: //An alias to this for loop, so we're able to break it in the inner loop
            for(Route destinationRoute : routesWithRequiredDestination) {
                firstLevelRoutes.addAll(this.routeService.findByDestinationAirportId(destinationRoute.getOriginAirport().getId()));

                //Going to first level
                for(Route firstLevelRoute : firstLevelRoutes) {
                    try {
                        //If found, then add it to the routesFound and update the number of connections.
                        if (firstLevelRoute.getOriginAirport().getId().equals(originAirport.getId())) {
                            routesFound.getValue().add(firstLevelRoute); //Adding the destinationRoute with the same origin as the user's
                            routesFound.getValue().add(destinationRoute);
                            routesFound.setKey(1);
                            break masterLoop;
                        }
                    } catch(NullPointerException ex) {
                        //There are some routes with no airports information, so just suppress them and keep going.
                    }
                }

                //Going to second level
                for(Route firstLevelRoute : firstLevelRoutes) {
                    List<Route> secondLevelRoutes = this.routeService.findByDestinationAirportId(firstLevelRoute.getOriginAirport().getId());

                    for(Route secondLevelRoute : secondLevelRoutes) {
                        //If found, then add it to the routesFound and update the number of connections.
                        try {
                            if (secondLevelRoute.getOriginAirport().getId().equals(originAirport.getId())) {
                                routesFound.getValue().add(secondLevelRoute);
                                routesFound.getValue().add(firstLevelRoute);
                                routesFound.getValue().add(destinationRoute);
                                routesFound.setKey(2);
                                break masterLoop;
                            }
                        } catch(NullPointerException ex) {
                            //There are some routes with no airports information, so just suppress them and keep going.
                        }
                    }
                }

                //Going to third level
                List<Route> thirdLevelRoutes = new ArrayList<>();
                for(Route firstLevelRoute : firstLevelRoutes) {
                    List<Route> secondLevelRoutes = this.routeService.findByDestinationAirportId(firstLevelRoute.getOriginAirport().getId());

                    for(Route secondLevelRoute : secondLevelRoutes) {
                        thirdLevelRoutes.addAll(this.routeService.findByDestinationAirportId(secondLevelRoute.getOriginAirport().getId()));

                        for(Route thirdLevelRoute : thirdLevelRoutes) {
                            //If found, then add it to the routesFound and update the number of connections.
                            try {
                                if(thirdLevelRoute.getOriginAirport().getId().equals(originAirport.getId())) {
                                    routesFound.getValue().add(thirdLevelRoute);
                                    routesFound.getValue().add(secondLevelRoute);
                                    routesFound.getValue().add(firstLevelRoute);
                                    routesFound.getValue().add(destinationRoute);
                                    routesFound.setKey(3);
                                    break masterLoop;
                                }
                            } catch(NullPointerException ex) {
                                //There are some routes with no airports information, so just suppress them and keep going.
                            }
                        }
                    }
                }
            }

            return routesFound;
        }
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
