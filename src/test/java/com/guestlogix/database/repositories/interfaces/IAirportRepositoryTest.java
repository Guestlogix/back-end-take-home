package com.guestlogix.database.repositories.interfaces;

import com.guestlogix.database.entities.Airport;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;
import org.springframework.test.context.junit4.SpringRunner;

import static org.junit.Assert.*;

@RunWith(SpringRunner.class)
@DataJpaTest
public class IAirportRepositoryTest {

    @Autowired
    private IAirportRepository airportRepository;

    //Since we're testing Spring Boot, it's required to wait it start up and set up the database, so the test may take a little while to execute.
    @Test
    public void fetchByAirportIataCodeShouldNotReturnNull() {
        Airport airport = this.airportRepository.findByIataThree("CMH");
        assertNotNull(airport);
    }

}