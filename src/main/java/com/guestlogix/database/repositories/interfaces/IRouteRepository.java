package com.guestlogix.database.repositories.interfaces;

import com.guestlogix.database.entities.Route;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

/**
 * An interface the extends from Spring's CrudRepository class, which has all the most commons data access's methods implemented. Any custom method is to be created in the custom Interface.
 *
 * @author Vanderson Assis
 * @since 4/23/2019
 */

@Repository
public interface IRouteRepository extends CrudRepository<Route, Long> {
}
