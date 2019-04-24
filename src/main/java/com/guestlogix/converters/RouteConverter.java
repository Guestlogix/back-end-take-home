package com.guestlogix.converters;

import com.guestlogix.database.entities.Route;
import com.guestlogix.database.repositories.interfaces.IAirlineRepository;
import com.guestlogix.database.repositories.interfaces.IAirportRepository;

import java.io.*;
import java.util.ArrayList;
import java.util.List;
import java.util.function.Function;
import java.util.stream.Collectors;

/**
 * This class helps on converting any data (in this case, csv) into Route entity
 *
 * @author Vanderson Assis
 * @since 4/23/2019
 */

public class RouteConverter {

    private IAirlineRepository airlineRepository;
    private IAirportRepository airportRepository;

    public RouteConverter(IAirlineRepository airlineRepository, IAirportRepository airportRepository) {
        this.airlineRepository = airlineRepository;
        this.airportRepository = airportRepository;
    }

    /**
     * Loads all the Route data from the csv file and convert it to a list of Route entities
     * @param filePath the path of the file to be used on loading the routes
     * @return a list of the routes fetched from the csv file
     */
    public List<Route> createFromCsvFile(String filePath) {
        List<Route> routes = new ArrayList<>();

        try {
            File inputFile = new File(filePath);
            InputStream inputStream = new FileInputStream(inputFile);
            BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(inputStream));

            //Skipping CSV header and then loading all the objects.
            routes = bufferedReader.lines().skip(1).map(this.mapToRoute).collect(Collectors.toList());
        } catch(FileNotFoundException ex) {
            ex.printStackTrace();
        }

        return routes;
    }

    /**
     * A function to map Route's field in a comma separated String to a Route entity.
     */
    private Function<String, Route> mapToRoute = (line) -> {
        String[] p = line.split(",");
        return new Route(this.airlineRepository.findByTwoDigitCode(p[0].trim()), this.airportRepository.findByIataThree(p[1].trim()), this.airportRepository.findByIataThree(p[2].trim()));
    };

}
