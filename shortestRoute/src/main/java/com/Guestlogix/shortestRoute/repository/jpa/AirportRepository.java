package com.Guestlogix.shortestRoute.repository.jpa;

import org.springframework.data.jpa.repository.JpaRepository;

import com.Guestlogix.shortestRoute.domain.jpa.Airport;

/**
 * 
 * @author nchopra
 *
 */
public interface AirportRepository extends JpaRepository<Airport, Long> {

}