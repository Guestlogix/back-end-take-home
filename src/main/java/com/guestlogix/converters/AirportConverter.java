package com.guestlogix.converters;

import com.guestlogix.database.entities.Airport;
import com.guestlogix.database.repositories.interfaces.ICityRepository;
import com.guestlogix.database.repositories.interfaces.ICountryRepository;

import java.io.*;
import java.util.ArrayList;
import java.util.List;
import java.util.function.Function;
import java.util.regex.Pattern;
import java.util.stream.Collectors;

/**
 * This class helps on converting any data (in this case, csv) into Airport entity
 *
 * @author Vanderson Assis
 * @since 4/23/2019
 */

public class AirportConverter {

    private ICityRepository cityRepository;
    private ICountryRepository countryRepository;

    public AirportConverter(ICityRepository cityRepository, ICountryRepository countryRepository) {
        this.cityRepository = cityRepository;
        this.countryRepository = countryRepository;
    }

    /**
     * Loads all the Airport data from the csv file and convert it to a list of Airport entities
     * @param filePath the path of the file to be used on loading the airports
     * @return a list of the airlines fetched from the csv file
     */
    public List<Airport> createFromCsvFile(String filePath) {
        List<Airport> airports = new ArrayList<>();

        try {
            File inputFile = new File(filePath);
            InputStream inputStream = new FileInputStream(inputFile);
            BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(inputStream));

            //Skipping CSV header and then loading all the objects.
            airports = bufferedReader.lines().skip(1).map(this.mapToAirport).collect(Collectors.toList());
        } catch(FileNotFoundException ex) {
            ex.printStackTrace();
        }

        return airports;
    }

    /**
     * A function to map Airport's field in a comma separated String to an Airport entity.
     */
    private Function<String, Airport> mapToAirport = (line) -> {
        String[] p = line.split(Pattern.quote("||"));
        double latitude = Double.parseDouble(p[4].trim());
        double longitude = Double.parseDouble(p[5].trim());

        return new Airport(p[0].trim(), p[3].trim(), latitude, longitude, this.cityRepository.findByName(p[1].trim()), this.countryRepository.findByName(p[2].trim()));
    };

}
