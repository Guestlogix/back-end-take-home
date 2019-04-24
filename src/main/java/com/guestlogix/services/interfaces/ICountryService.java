package com.guestlogix.services.interfaces;

import com.guestlogix.database.entities.Country;

/**
 * A Spring service interface defining all the methods that the Country method will have to implement.
 *
 * @author Vanderson Assis
 * @since 4/23/2019
 */

public interface ICountryService {
    Country findByName(String name);
}
