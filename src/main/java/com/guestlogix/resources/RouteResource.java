package com.guestlogix.resources;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

/**
 * This endpoint will have all the methods related to routes.
 *
 * @author Vanderson Assis
 * @since 4/22/2019
 */

@RestController
@RequestMapping("/route")
public class RouteResource {

    @GetMapping("/calculate/{origin}/{destination}")
    public String calculate(@PathVariable("origin") String originAirportCode, @PathVariable("destination") String destinationAirportCode) {
        return originAirportCode + " " + destinationAirportCode;
    }

}
