package com.guestlogix.database.repositories.interfaces;

import com.guestlogix.database.entities.Route;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

/**
 * An interface that defines which methods any Route repository will have to implement.
 *
 * @author Vanderson Assis
 * @since 4/24/2019
 */
@Repository
public interface IRouteRepository extends CrudRepository<Route, Long> {
    @Query("SELECT r FROM Route r WHERE r.originAirport.id=?1")
    List<Route> findByOriginAirportId(Long originAirportId);

    @Query("SELECT r FROM Route r WHERE r.destinationAirport.id=?1")
    List<Route> findByDestinationAirportId(Long destinationAirportId);
}
