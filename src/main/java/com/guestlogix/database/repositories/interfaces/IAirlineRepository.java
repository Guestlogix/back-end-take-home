package com.guestlogix.database.repositories.interfaces;

import com.guestlogix.database.entities.Airline;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

/**
 * An interface that defines which methods any Airline repository will have to implement.
 *
 * @author Vanderson Assis
 * @since 4/23/2019
 */

@Repository
public interface IAirlineRepository extends CrudRepository<Airline, Long> {
    @Query("SELECT a FROM Airline a WHERE a.twoDigitCode=?1")
    Airline findByTwoDigitCode(String twoDigitCode);
}
