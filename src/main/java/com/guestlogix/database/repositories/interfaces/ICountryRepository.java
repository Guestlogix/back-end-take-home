package com.guestlogix.database.repositories.interfaces;

import com.guestlogix.database.entities.Country;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

/**
 * An interface that defines which methods any Country repository will have to implement.
 *
 * @author Vanderson Assis
 * @since 4/23/2019
 */

//By convention Java doesn't advice on using the I prefix on interface's name. But I got used on using the I prefix in C# and today is really weird not seeing it in interfaces' names.
//But of course, if the company I work for advises against it, I'll respect their decision.
@Repository
public interface ICountryRepository extends CrudRepository<Country, Long> {
    @Query("SELECT c FROM Country c WHERE c.name=?1")
    Country findByName(String name);
}
