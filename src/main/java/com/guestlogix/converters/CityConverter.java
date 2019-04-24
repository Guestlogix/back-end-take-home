package com.guestlogix.converters;

import com.guestlogix.database.entities.City;

import java.io.*;
import java.util.ArrayList;
import java.util.List;
import java.util.function.Function;
import java.util.stream.Collectors;

/**
 * This class helps on converting any data (in this case, csv) into City entity
 *
 * @author Vanderson Assis
 * @since 4/23/2019
 */

public class CityConverter {

    /**
     * Loads all the City data from the csv file and convert it to a list of City entities
     * @param filePath the path of the file to be used on loading the cities
     * @return a list of the cities fetched from the csv file
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
     * A function to map Airline's field in a comma separated String to a City entity.
     */
    private Function<String, City> mapToCity = (line) -> new City(line.replace("\"", ""));

}
