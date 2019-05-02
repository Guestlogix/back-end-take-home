package com.guestlogix.calculators.tree;

import com.guestlogix.database.entities.Airport;
import com.guestlogix.database.entities.Route;
import com.guestlogix.services.RouteService;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

/**
 * Please visit my GitHub page https://github.com/VandersonAssis to get more info on this or any other project I implemented.
 *
 * @author Vanderson Assis
 * @since 4/30/2019
 */

public class RouteTree {
    private RouteTreeNode root;
    private RouteService routeService;
    private Airport originAirport;
    private Airport destinationAirport;

    public RouteTree(RouteService routeService, Airport originAirport, Airport destinationAirport, List<Route> connectingRoutes) {
        this.routeService = routeService;
        this.originAirport = originAirport;
        this.destinationAirport = destinationAirport;
        Route route = new Route(null, originAirport, destinationAirport);
        this.root = new RouteTreeNode(route, connectingRoutes, null, null, true);
    }

    /**
     * Creates a new node in the Tree and returns it.
     * @param route this new node's route
     * @param connectingRoutes the connecting routes for this node's Route
     * @param parentNode the parent Node of this node
     * @param siblingNode the sibling Node of this node
     * @return the new node created
     */
    private RouteTreeNode createNewNode(Route route, List<Route> connectingRoutes, RouteTreeNode parentNode, RouteTreeNode siblingNode) {
        return new RouteTreeNode(route, connectingRoutes, parentNode, siblingNode, false);
    }

    /**
     * This method will go back to the root node and then go down in the tree searching for any route for the destinationAirport, if it doesn't find any, then it will load the data
     * for the nodes bellow and keep trying until it finds or exhaust all the possibilities.
     * @return a List containing the routes found
     */
    public List<Route> findConnectingRoutes() {
        List<Route> routesFound = new ArrayList<>();
        RouteTreeNode current = this.root;

        //Firstly trying to find a route without any connection. If it finds, than returns it.
        Route routeWithoutConnection = current.getConnectingRoutes().stream().filter(r -> r.getDestinationAirport().getId().equals(this.destinationAirport.getId())).findAny().orElse(null);
        if(routeWithoutConnection != null) {
            routesFound.add(routeWithoutConnection);
            return routesFound;
        }

        //If execution reached here, then no route without connection was found, therefore, load data into the Tree and keep trying.
        RouteTreeNode childNode;
        RouteTreeNode siblingNode = null;
        boolean useSibling = false;

        for(int i = 0; i < current.getConnectingRoutes().size(); i++) {
            Route connectingRoute = current.getConnectingRoutes().get(i);

            if(connectingRoute.getOriginAirport() == null || connectingRoute.getDestinationAirport() == null) continue; //There are some invalid routes in the file.

            //This list bellow will have all routes that originates from the destination (making the connections).
            List<Route> originatingRoutesWithDestination = this.routeService.findByOriginAirportId(connectingRoute.getDestinationAirport().getId());
            childNode = this.createNewNode(connectingRoute, originatingRoutesWithDestination, current, siblingNode);
            siblingNode = childNode; //Setting the sibling here, so in the next loop, the sibling will be filled.

            Route routeForDestinationFound = this.find(this.destinationAirport, childNode.getConnectingRoutes());

            //If found a route that has the same destination as what the user wants, then add it to the array, then also add the childNode's route
            //and afterwards go up in the tree loading all the connecting routes.
            if(routeForDestinationFound != null) {
                routesFound.add(routeForDestinationFound);
                routesFound.add(childNode.getRoute());
                RouteTreeNode node = childNode;

                boolean keepTraversingTree = true;
                while(keepTraversingTree) {
                    node = node.getParentNode();

                    if(!node.isRoot())
                        routesFound.add(node.getRoute());
                    else
                        keepTraversingTree = false;
                }

                break;
            }

            //If the algorithm finished looping all these routes and didn't find the route required by the user, then, point to this Node's
            //sibling or this node's child and keep trying until all the possibilities are exhausted.
            if(i == (current.getConnectingRoutes().size() - 1)) {

                if(useSibling && current.hasSibling()) {
                    current = current.getSiblingNode();
                } else {
                    current = childNode;
                    useSibling = true;
                }

                i = 0; //Setting the counter back to 0 so the algorithm iterated the Child's (or Sibling) Node.
            }
        }

        //The above algorithm adds the routes found in a Stack structure, so the first route will be last in the stack. That's why the inversion bellow is needed.
        Collections.reverse(routesFound);
        return routesFound;
    }

    /**
     * This method will loop all the routes looking for any route that has the destination the same as the destinationAirport.
     * @param destinationAirport the user's required destination
     * @param routes a list of routes to be iterated looking for that destination in particular
     * @return the route found (if any).
     */
    private Route find(Airport destinationAirport, List<Route> routes) {
        Route routeFound = null;

        for(Route route : routes) {
            if(route.getOriginAirport() == null || route.getDestinationAirport() == null) continue; //There are some invalid routes in the file.

            if(route.getDestinationAirport().getId().equals(destinationAirport.getId())) {
                routeFound = route;
                break;
            }
        }

        return routeFound;
    }
}
