package com.guestlogix.resources;

import com.guestlogix.calculators.RouteCalculator;
import com.guestlogix.database.entities.Airport;
import com.guestlogix.database.entities.Route;
import com.guestlogix.resources.exceptions.AirportNotFoundException;
import com.guestlogix.services.AirportService;
import com.guestlogix.services.RouteService;
import com.guestlogix.vos.KeyValueVo;
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
     * This method calculates the shortest route based on the origin and destination airport parameters. It uses a Tree algorithm approach, which I think was
     * the best approach for the problem at hand. This algorithm will calculate a possible route with no limitation of connections number.
     * @param originAirportIataCode the origin airport code to be used in the calculus
     * @param destinationAirportIataCode the destination airport code to be used in the calculus
     * @return a String containing the message that will be displayed to the user
     */
    @GetMapping("/calculate/{origin}/{destination}")
    public String calculate(@PathVariable("origin") String originAirportIataCode, @PathVariable("destination") String destinationAirportIataCode) {
        Airport originAirport = this.airportService.findByIataThree(originAirportIataCode);
        Airport destinationAirport = this.airportService.findByIataThree(destinationAirportIataCode);

        //Whenever the exception bellow is thrown, Spring will make use of the AirportNotFoundAdvice class to return a meaningful information of what happened.
        if(originAirport == null) throw new AirportNotFoundException(originAirportIataCode);
        if(destinationAirport == null) throw new AirportNotFoundException(destinationAirportIataCode);

        KeyValueVo<Integer, List<Route>> shortestRoute = new RouteCalculator(this.routeService).calculateShortestRouteTree(originAirport, destinationAirport);

        return this.createRouteMessage(shortestRoute);
    }

    /**
     * This method calculates in a recursive approach the shortest route based on the origin and destination airport parameters.
     *
     * @param originAirportIataCode the origin airport code to be used in the calculus
     * @param destinationAirportIataCode the destination airport code to be used in the calculus
     * @return a String containing the message that will be displayed to the user
     */
    @GetMapping("/calculate-recursive/{origin}/{destination}")
    public String calculateRecursive(@PathVariable("origin") String originAirportIataCode, @PathVariable("destination") String destinationAirportIataCode) {
        Airport originAirport = this.airportService.findByIataThree(originAirportIataCode);
        Airport destinationAirport = this.airportService.findByIataThree(destinationAirportIataCode);

        //Whenever the exception bellow is thrown, Spring will make use of the AirportNotFoundAdvice class to return a meaningful information of what happened.
        if(originAirport == null) throw new AirportNotFoundException(originAirportIataCode);
        if(destinationAirport == null) throw new AirportNotFoundException(destinationAirportIataCode);

        KeyValueVo<Integer, List<Route>> shortestRoute = new RouteCalculator(this.routeService).calculateShortestRouteRecursive(originAirport, destinationAirport);

        return this.createRouteMessage(shortestRoute);
    }

    private String createRouteMessage(KeyValueVo<Integer, List<Route>> shortestRoute) {
        StringBuilder message = new StringBuilder();
        //Condition for no route found.
        if(shortestRoute.getValue().size() == 0) {
            message.append("Dear user, we hope you're doing great today!<br /> Unfortunately we couldn't find any routes for your flight.<br />");
            message.append("Our systems are always updated with the latest flights in the market! So please try again in some hours and maybe we can find a flight for you!<br /><br />");
            message.append("Yours sincerely, GuestLogix =}");
        } else if(shortestRoute.getValue().size() > 1) { //Condition for route with more than one connection
            message.append("Dear user, we hope you're doing great today! If not, then cheer up because we found a flight for you! More details bellow:<br />");
            message.append("Number of connections: " + shortestRoute.getKey() + "<br /><br />");

            int routeIndex = 1;
            for(Route route : shortestRoute.getValue()) {
                message.append("Route " + routeIndex + ": ");
                message.append("<b>Airline:</b> " + route.getAirline().getName() +
                              ", <b>Origin Airport:</b> " + route.getOriginAirport().getName() + "[" + route.getOriginAirport().getIataThree() + "]" +
                              ", <b>Destination Airport:</b> " + route.getDestinationAirport().getName() + "[" + route.getDestinationAirport().getIataThree() + "]" + "<br />");

                ++routeIndex;
            }
        } else { //If the code reached here, then the route will have no connection, straight flight!
            Route route = shortestRoute.getValue().get(0);
            message.append("Dear user, we hope you're doing great today! If not, then cheer up because we found a flight for you with no connection! More details bellow:<br />");
            message.append("<b>Airline:</b> " + route.getAirline().getName() +
                          ", <b>Origin Airport:</b> " + route.getOriginAirport().getName() + "[" + route.getOriginAirport().getIataThree() + "]" +
                          ", <b>Destination Airport:</b> " + route.getDestinationAirport().getName() + "[" + route.getDestinationAirport().getIataThree() + "]");
        }

        return message.toString();
    }

}
