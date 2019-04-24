package com.guestlogix.converters;

import com.guestlogix.database.entities.Country;

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

public class CountryConverter {
    /**
     * Loads all the Country data from the csv file and convert it to a list of Country entities
     * @param filePath the path of the file to be used on loading the countries
     * @return a list of the airlines fetched from the csv file
     */
    public List<Country> createFromCsvFile(String filePath) {
        List<Country> countries = new ArrayList<>();

        try {
            File inputFile = new File(filePath);
            InputStream inputStream = new FileInputStream(inputFile);
            BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(inputStream));

            //Skipping CSV header and then loading all the objects.
            countries = bufferedReader.lines().skip(1).map(this.mapToCountry).collect(Collectors.toList());
        } catch(FileNotFoundException ex) {
            ex.printStackTrace();
        }

        return countries;
    }

    /**
     * A function to convert a String in a Country entity.
     */
    private Function<String, Country> mapToCountry = Country::new;

}
