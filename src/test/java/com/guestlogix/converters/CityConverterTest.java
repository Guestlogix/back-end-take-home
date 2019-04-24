package com.guestlogix.converters;

import com.guestlogix.database.entities.City;
import org.junit.Test;

import java.util.List;

class CityConverterTest {

    //I didn't have the time to code automated tests for the whole application, so I decided to put at least one, here, just to say there's a test in place. ={ ...
    //If I had the time, I'd test the application using mockito and Rest Assured (for the web services)
    @Test
    void shouldReturnListOfCitiesFromCsvFile(String csvFilePath) {
        List<City> cities = new CityConverter().createFromCsvFile("data/cities.csv");
        if(cities == null || cities.size() == 0)
            throw new AssertionError("No city was returned from the csv file convertion!");
    }

}