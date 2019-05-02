package com.guestlogix.resources.exceptions;

/**
 * If the user enters an invalid Airport for destination or origin, then this exception will be thrown.
 *
 * @author Vanderson Assis
 * @since 4/24/2019
 */

public class AirportNotFoundException extends RuntimeException {

    public AirportNotFoundException(String airportIataCode) {
        super("Could not find Airport for the " + airportIataCode + " IATA Code.");
    }

}
