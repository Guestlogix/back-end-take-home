package com.guestlogix.converters;

import com.guestlogix.database.entities.City;
import org.junit.Test;

import java.util.List;

import static org.junit.Assert.*;

public class CityConverterTest {

    @Test
    public void shouldReturnListOfCitiesFromCsvFile() {
        List<City> cities = new CityConverter().createFromCsvFile("data/cities.csv");
        assertTrue(cities != null && cities.size() > 0);
    }

}