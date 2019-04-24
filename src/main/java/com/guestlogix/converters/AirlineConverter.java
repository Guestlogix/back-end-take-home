package com.guestlogix.converters;

import com.guestlogix.database.entities.Airline;
import com.guestlogix.database.repositories.interfaces.ICountryRepository;

import java.io.*;
import java.util.ArrayList;
import java.util.List;
import java.util.function.Function;
import java.util.stream.Collectors;

/**
 * This class helps on converting any data (in this case, csv) into the generic entity provided
 *
 * @author Vanderson Assis
 * @since 4/23/2019
 */

public class AirlineConverter {
    private ICountryRepository countryRepository;

    public AirlineConverter(ICountryRepository countryRepository) {
        this.countryRepository = countryRepository;
    }

    /**
     * Loads all the Airline data from the csv file and convert it to a list of Airline entities
     * @param filePath the path of the file to be used on loading the airlines
     * @return a list of the airlines fetched from the csv file
     */
    public List<Airline> createFromCsvFile(String filePath) {
        List<Airline> airlines = new ArrayList<>();

        try {
            File inputFile = new File(filePath);
            InputStream inputStream = new FileInputStream(inputFile);
            BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(inputStream));

            //Skipping CSV header and then loading all the objects.
            airlines = bufferedReader.lines().skip(1).map(this.mapToAirline).collect(Collectors.toList());
        } catch(FileNotFoundException ex) {
            ex.printStackTrace();
        }

        return airlines;
    }

    /**
     * A function to map Airline's field in a comma separated String to an Airline entity.
     */
    private Function<String, Airline> mapToAirline = (line) -> {
        String[] p = line.split(",");
        return new Airline(p[0], p[1], p[2], this.countryRepository.findByName(p[3]));
    };
}
