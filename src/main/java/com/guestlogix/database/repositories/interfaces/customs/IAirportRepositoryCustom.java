package com.guestlogix.database.repositories.interfaces.customs;

import com.guestlogix.database.entities.Airport;
import org.springframework.data.jpa.repository.Query;

/**
 * An interface that defines which methods any Airport repository will have to implement. This interface exists for the sole purpose of custom methods creation.
 *
 * @author Vanderson Assis
 * @since 4/23/2019
 */

public interface IAirportRepositoryCustom {
    @Query("SELECT a FROM Airport a WHERE a.iataThree=?1")
    Airport findByIataThree(String iataThree);
}
