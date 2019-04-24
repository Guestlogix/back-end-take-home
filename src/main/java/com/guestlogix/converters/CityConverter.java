package com.guestlogix.converters;

import com.guestlogix.database.entities.City;

import java.io.*;
import java.util.ArrayList;
import java.util.List;
import java.util.function.Function;
import java.util.stream.Collectors;

/**
 * Please visit my GitHub page https://github.com/VandersonAssis to get more info on this or any other project I implemented.
 *
 * @author Vanderson Assis
 * @since 4/23/2019
 */

public class CityConverter {

    /**
     * Loads all the Airline data from the csv file and convert it to a list of Airline entities
     * @param filePath the path of the file to be used on loading the airlines
     * @return a list of the airlines fetched from the csv file
     */
    public List<City> createFromCsvFile(String filePath) {
        List<City> cities = new ArrayList<>();

        try {
            File inputFile = new File(filePath);
            InputStream inputStream = new FileInputStream(inputFile);
            BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(inputStream));

            //Skipping CSV header and then loading all the objects.
            cities = bufferedReader.lines().skip(1).map(this.mapToCity).collect(Collectors.toList());
        } catch(FileNotFoundException ex) {
            ex.printStackTrace();
        }

        return cities;
    }

    /**
     * A function to map Airline's field in a comma separated String to an Airline entity.
     */
    private Function<String, City> mapToCity = (line) -> new City(line.replace("\"", ""));

}
