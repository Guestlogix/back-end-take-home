package com.guestlogix.database;

import com.guestlogix.converters.*;
import com.guestlogix.database.entities.*;
import com.guestlogix.database.repositories.interfaces.*;

import java.util.List;

/**
 * This class will help on persisting the csv and any other initial data for this application.
 *
 * @author Vanderson Assis
 * @since 4/23/2019
 */

public class Persister {

    private IAirlineRepository airlineRepository;
    private ICountryRepository countryRepository;
    private ICityRepository cityRepository;
    private IAirportRepository airportRepository;
    private IRouteRepository routeRepository;

    public Persister(IAirlineRepository airlineRepository, ICountryRepository countryRepository, ICityRepository cityRepository,
                     IAirportRepository airportRepository, IRouteRepository routeRepository) {

        this.airlineRepository = airlineRepository;
        this.countryRepository = countryRepository;
        this.cityRepository = cityRepository;
        this.airportRepository = airportRepository;
        this.routeRepository = routeRepository;
    }

    /**
     * Executes all the persisting of initial data. Calling this method is MANDATORY for this application to work.
     * @return
     */
    public boolean persistInitialData() {
        try {
            this.persistCountries();
            this.persistCities();
            this.persistAirlines();
            this.persistAirports();
            this.persistRoutes();
            return true;
        } catch(Exception ex) {
            ex.printStackTrace();
            return false;
        }
    }

    /**
     * Persists all the airlines that are in the airlines.csv file.
     */
    private void persistAirlines() {
        List<Airline> airlines = new AirlineConverter(this.countryRepository).createFromCsvFile("data/airlines.csv");
        airlines.forEach(a -> this.airlineRepository.save(a));
    }

    /**
     * Persists all the cities that are in the cities.csv file.
     */
    private void persistCities() {
        List<City> cities = new CityConverter().createFromCsvFile("data/cities.csv");
        cities.forEach(c -> this.cityRepository.save(c));
    }

    /**
     * Persists all the countries that are in the countries.csv file.
     */
    private void persistCountries() {
        List<Country> countries = new CountryConverter().createFromCsvFile("data/countries.csv");
        countries.forEach(c -> this.countryRepository.save(c));
    }

    /**
     * Persists all the airports that are in the airports.csv file.
     */
    private void persistAirports() {
        List<Airport> airports = new AirportConverter(this.cityRepository, this.countryRepository).createFromCsvFile("data/airports_new_delimiter.csv");
        airports.forEach(a -> this.airportRepository.save(a));
    }

    /**
     * Persists all the routes that are in the routes.csv file.
     */
    private void persistRoutes() {
        List<Route> routes = new RouteConverter(this.airlineRepository, this.airportRepository).createFromCsvFile("data/routes.csv");
        routes.forEach(r -> this.routeRepository.save(r));
    }

}
