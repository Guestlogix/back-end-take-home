package com.guestlogix;

import com.guestlogix.database.Persister;
import com.guestlogix.database.repositories.interfaces.*;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;

/**
 * This class is the starting point of this application. This is where all the magics starts to happen! =}
 */
@SpringBootApplication
public class BackEndTakeHomeApplication {

	public static void main(String[] args) {
		SpringApplication.run(BackEndTakeHomeApplication.class, args);
	}

	/**
	 * A Spring's runner that's used to initialize anything this application needs.
	 *
	 * @param airlineRepository the repository to deal with Airline's database interaction
	 * @param countryRepository the repository to deal with Country's database interaction
	 * @param cityRepository the repository to deal with City's database interaction
	 * @param airportRepository the repository to deal with Airport's database interaction
	 * @param routeRepository the repository to deal with Route's database interaction
	 * @return a CommandLineRunner that will be used internally by Spring Technologies
	 */
	@Bean
	CommandLineRunner runner(IAirlineRepository airlineRepository, ICountryRepository countryRepository,
							 ICityRepository cityRepository, IAirportRepository airportRepository, IRouteRepository routeRepository) {
		return args -> new Persister(airlineRepository, countryRepository, cityRepository, airportRepository, routeRepository).persistInitialData();
	}

}
