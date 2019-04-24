package com.guestlogix.database.repositories.interfaces.customs;

import com.guestlogix.database.entities.Airline;
import org.springframework.data.jpa.repository.Query;

/**
 * An interface that defines which method any Airline repository will have to implement.
 *
 * @author Vanderson Assis
 * @since 4/23/2019
 */

public interface IAirlineRepositoryCustom {
    @Query("SELECT a FROM Airline a WHERE a.twoDigitCode=?1")
    Airline findByTwoDigitCode(String twoDigitCode);
}
