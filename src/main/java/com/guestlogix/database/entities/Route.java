package com.guestlogix.database.entities;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.ManyToOne;

/**
 * This class models a Route for this application.
 *
 * @author Vanderson Assis
 * @since 4/22/2019
 */

@Entity
public class Route {
    @Id @GeneratedValue
    private Long id;

    public Route() {
    }

    public Route(Airline airline, Airport originAirport, Airport destinationAirport) {
        this.airline = airline;
        this.originAirport = originAirport;
        this.destinationAirport = destinationAirport;
    }

    @ManyToOne private Airline airline;
    @ManyToOne private Airport originAirport;
    @ManyToOne private Airport destinationAirport;

    //getters and setters (whenever you see this comment in my code, means that bellow methods are nothing more than default getters and setters, so no need to analyze them)
    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Airline getAirline() {
        return airline;
    }

    public void setAirline(Airline airline) {
        this.airline = airline;
    }

    public Airport getOriginAirport() {
        return originAirport;
    }

    public void setOriginAirport(Airport originAirport) {
        this.originAirport = originAirport;
    }

    public Airport getDestinationAirport() {
        return destinationAirport;
    }

    public void setDestinationAirport(Airport destinationAirport) {
        this.destinationAirport = destinationAirport;
    }
}
