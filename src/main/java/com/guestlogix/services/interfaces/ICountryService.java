package com.guestlogix.services.interfaces;

import com.guestlogix.database.entities.Country;

/**
 * Please visit my GitHub page https://github.com/VandersonAssis to get more info on this or any other project I implemented.
 *
 * @author Vanderson Assis
 * @since 4/23/2019
 */

public interface ICountryService {
    public Country findByName(String name);
}
