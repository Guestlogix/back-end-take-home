package com.Guestlogix.shortestRoute.repository.jpa;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import com.Guestlogix.shortestRoute.domain.jpa.Airline;

/**
 * 
 * @author nchopra
 *
 */
public interface AirlineRepository extends JpaRepository<Airline, Long> {
	
	@Query(value="select * from airlines where 2digit_code in :twodigitCodes", nativeQuery=true)
	List<Airline> findBytwodigitCodes(@Param("twodigitCodes") List<String> twodigitCodes);
	
}