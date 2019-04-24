package com.guestlogix.database.repositories.interfaces.customs;

import com.guestlogix.database.entities.Airport;
import org.springframework.data.jpa.repository.Query;

/**
 * Please visit my GitHub page https://github.com/VandersonAssis to get more info on this or any other project I implemented.
 *
 * @author Vanderson Assis
 * @since 4/23/2019
 */

public interface IAirportRepositoryCustom {
    @Query("SELECT a FROM Airport a WHERE a.iataThree=?1")
    Airport findByIataThree(String iataThree);
}
