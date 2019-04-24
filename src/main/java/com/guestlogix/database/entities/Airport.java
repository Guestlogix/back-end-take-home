package com.guestlogix.database.entities;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.ManyToOne;

/**
 * This class entities an Airport structure for this application.
 *
 * @author Vanderson Assis
 * @since 4/22/2019
 */

@Entity
public class Airport {

    @Id @GeneratedValue
    private int id;
    private String name;
    private String iataThree;
    private double latitude;
    private double longitude;

    public Airport() {
    }

    public Airport(String name, String iataThree, double latitude, double longitude, City city, Country country) {
        this.name = name;
        this.iataThree = iataThree;
        this.latitude = latitude;
        this.longitude = longitude;
        this.city = city;
        this.country = country;
    }

    @ManyToOne
    private City city;

    @ManyToOne
    private Country country;

    //getters and setters (whenever you see this comment in my code, means that bellow methods are nothing more than default getters and setters, so no need to analyze them)

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getIataThree() {
        return iataThree;
    }

    public void setIataThree(String iataThree) {
        this.iataThree = iataThree;
    }

    public double getLatitude() {
        return latitude;
    }

    public void setLatitude(double latitude) {
        this.latitude = latitude;
    }

    public double getLongitude() {
        return longitude;
    }

    public void setLongitude(double longitude) {
        this.longitude = longitude;
    }

    public City getCity() {
        return city;
    }

    public void setCity(City city) {
        this.city = city;
    }

    public Country getCountry() {
        return country;
    }

    public void setCountry(Country country) {
        this.country = country;
    }
}
