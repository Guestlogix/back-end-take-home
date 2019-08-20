package com.Guestlogix.shortestRoute.repository.jpa;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;

import com.Guestlogix.shortestRoute.domain.jpa.Route;

/**
 * 
 * @author nchopra
 *
 */
public interface RouteRepository extends JpaRepository<Route, Long> {
	
	List<Route> findByOrigin(String origin);
	
	List<Route> findByDestination(String destination);
	
	List<Route> findByOriginAndDestination(String origin, String destination);

}