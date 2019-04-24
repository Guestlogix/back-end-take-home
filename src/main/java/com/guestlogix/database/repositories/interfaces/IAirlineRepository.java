package com.guestlogix.database.repositories.interfaces;

import com.guestlogix.database.entities.Airline;
import com.guestlogix.database.repositories.interfaces.customs.IAirlineRepositoryCustom;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

/**
 * Please visit my GitHub page https://github.com/VandersonAssis to get more info on this or any other project I implemented.
 *
 * @author Vanderson Assis
 * @since 4/23/2019
 */

@Repository
public interface IAirlineRepository extends CrudRepository<Airline, Long>, IAirlineRepositoryCustom {
}
