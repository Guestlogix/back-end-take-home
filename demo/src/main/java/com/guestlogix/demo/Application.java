package com.guestlogix.demo;

import com.guestlogix.demo.utilities.DataUtil;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication
public class Application {

    public static void main(String[] args) {
        SpringApplication.run(Application.class, args);

        DataUtil.readAirlineData("/data/airlines.csv");
        DataUtil.readAirportData("/data/airports.csv");
        DataUtil.readRouteData("/data/routes.csv");

        DataUtil.createAdjacentMatrix();

    }
}
