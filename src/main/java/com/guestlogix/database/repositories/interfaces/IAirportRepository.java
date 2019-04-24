package com.guestlogix.database.repositories.interfaces;

import com.guestlogix.database.entities.Airport;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

/**
 * An interface that defines which methods any Airport repository will have to implement.
 *
 * @author Vanderson Assis
 * @since 4/23/2019
 */

@Repository
public interface IAirportRepository extends CrudRepository<Airport, Long> {
    @Query("SELECT a FROM Airport a WHERE a.iataThree=?1")
    Airport findByIataThree(String iataThree);
}
