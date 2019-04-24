package com.guestlogix.database.entities;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;

/**
 * This class entities a Country structure for this application.
 *
 * @author Vanderson Assis
 * @since 4/22/2019
 */

@Entity
public class Country {

    @Id @GeneratedValue
    private Long id;
    private String name;

    public Country() {
    }

    public Country(String name) {
        this.name = name;
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
}
