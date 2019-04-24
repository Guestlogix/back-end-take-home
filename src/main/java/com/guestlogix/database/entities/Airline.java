package com.guestlogix.database.entities;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.ManyToOne;

/**
 * This class entities an Airline structure for this application.
 *
 * @author Vanderson Assis
 * @since 4/22/2019
 */

@Entity
public class Airline {

    @Id @GeneratedValue
    private Long id;
    private String name;
    private String twoDigitCode;
    private String threeDigitCode;

    @ManyToOne
    private Country country;

    public Airline() {
    }

    public Airline(String name, String twoDigitCode, String threeDigitCode, Country country) {
        this.name = name;
        this.twoDigitCode = twoDigitCode;
        this.threeDigitCode = threeDigitCode;
        this.country = country;
    }

    //getters and setters (whenever you see this comment in my code, means that bellow methods are nothing more than default getters and setters, so no need to analyze them)
    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getTwoDigitCode() {
        return twoDigitCode;
    }

    public void setTwoDigitCode(String twoDigitCode) {
        this.twoDigitCode = twoDigitCode;
    }

    public String getThreeDigitCode() {
        return threeDigitCode;
    }

    public void setThreeDigitCode(String threeDigitCode) {
        this.threeDigitCode = threeDigitCode;
    }

    public Country getCountry() {
        return country;
    }

    public void setCountry(Country country) {
        this.country = country;
    }
}
