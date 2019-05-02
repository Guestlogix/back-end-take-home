package com.guestlogix.database.repositories.interfaces;

import com.guestlogix.database.entities.Airport;
import com.guestlogix.database.entities.Route;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;
import org.springframework.test.context.junit4.SpringRunner;

import java.util.List;

import static org.junit.Assert.*;

@RunWith(SpringRunner.class)
@DataJpaTest
public class IRouteRepositoryTest {

    @Autowired
    private IRouteRepository routeRepository;

    //Since we're testing Spring Boot, it's required to wait it start up and set up the database, so the test may take a little while to execute.
    @Test
    public void fetchRoutesByOriginAirportIdShouldReturnAtLeastOne() {
        List<Route> routes = this.routeRepository.findByOriginAirportId(1821L);
        assertTrue(routes != null && routes.size() > 0);
    }

    @Test
    public void fetchRoutesByDestinationAirportIdShouldReturnAtLeastOne() {
        List<Route> routes = this.routeRepository.findByDestinationAirportId(1391L);
        assertTrue(routes != null && routes.size() > 0);
    }

}