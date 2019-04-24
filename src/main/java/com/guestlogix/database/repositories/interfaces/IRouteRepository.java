package com.guestlogix.database.repositories.interfaces;

import com.guestlogix.database.entities.Route;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

/**
 * Please visit my GitHub page https://github.com/VandersonAssis to get more info on this or any other project I implemented.
 *
 * @author Vanderson Assis
 * @since 4/23/2019
 */

@Repository
public interface IRouteRepository extends CrudRepository<Route, Long> {
}
