package com.guestlogix.services;

import com.guestlogix.database.entities.Airport;
import com.guestlogix.database.repositories.interfaces.IAirportRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

/**
 * This class interfaces any call to the Airport repository.
 *
 * @author Vanderson Assis
 * @since 4/24/2019
 */

@Service
public class AirportService {

    @Autowired
    private IAirportRepository airportRepository;

    public Airport findByIataThree(String iataThree) {
        return airportRepository.findByIataThree(iataThree);
    }

}
