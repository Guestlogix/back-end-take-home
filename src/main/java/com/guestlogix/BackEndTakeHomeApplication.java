package com.guestlogix;

import com.guestlogix.database.Persister;
import com.guestlogix.database.repositories.interfaces.*;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;

@SpringBootApplication
public class BackEndTakeHomeApplication {

	public static void main(String[] args) {
		SpringApplication.run(BackEndTakeHomeApplication.class, args);
	}

	//This portion of the code is executed upon this application's initialization.
	@Bean
	CommandLineRunner runner(IAirlineRepository airlineRepository, ICountryRepository countryRepository,
							 ICityRepository cityRepository, IAirportRepository airportRepository, IRouteRepository routeRepository) {
		return args -> new Persister(airlineRepository, countryRepository, cityRepository, airportRepository, routeRepository).persistInitialData();
	}

}
