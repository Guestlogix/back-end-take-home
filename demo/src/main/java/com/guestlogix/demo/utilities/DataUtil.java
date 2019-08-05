package com.guestlogix.demo.utilities;

import com.guestlogix.demo.model.*;
import org.springframework.core.io.ClassPathResource;

import java.io.*;
import java.nio.charset.StandardCharsets;
import java.util.ArrayList;
import java.util.List;
import java.util.Objects;
import java.util.Optional;
import java.util.stream.Stream;

import static java.util.stream.Collectors.toList;

public class DataUtil {

    public static List<Airline> airlines;
    public static List<Airport> airports;
    public static List<Route> routes;
    public static Graph graph;

    public static void readAirlineData(String fileName) {
        airlines = new ArrayList<>();

        ClassPathResource resource = new ClassPathResource(fileName);
        try (InputStream inputStream = resource.getInputStream()) {
            new BufferedReader(new InputStreamReader(inputStream, StandardCharsets.UTF_8))
                    .lines()
                    .skip(1)
                    .collect(toList())
                    .forEach(airlineData -> airlines.add(createAirline(airlineData)));
        } catch (IOException ex) {
            System.out.println(String.join("\n", Stream.of(ex.getStackTrace())
                    .map(StackTraceElement::toString)
                    .collect(toList())));

            throw new Error(ex.toString());
        }


    }

    private static Airline createAirline(String line) {

        String[] parts = line.split(",");
        if (parts.length == 4) {

            Airline airline = new Airline();
            airline.setName(parts[0]);
            airline.setId(parts[1]);
            airline.setCode(parts[2]);
            airline.setCountry(parts[3]);
            return airline;

        }
        return null;
    }

    public static void readAirportData(String fileName) {
        airports = new ArrayList<>();

        ClassPathResource resource = new ClassPathResource(fileName);
        try (InputStream inputStream = resource.getInputStream()) {
            new BufferedReader(new InputStreamReader(inputStream, StandardCharsets.UTF_8))
                    .lines()
                    .skip(1)
                    .collect(toList())
                    .forEach(data -> airports.add(createAirport(data)));
        } catch (IOException ex) {
            System.out.println(String.join("\n", Stream.of(ex.getStackTrace())
                    .map(StackTraceElement::toString)
                    .collect(toList())));

            throw new Error(ex.toString());
        }

    }


    private static Airport createAirport(String line) {

        String[] parts = line.split(",");
        if (parts.length == 6) {

            Airport airport = new Airport();
            airport.setName(parts[0]);
            airport.setCity(parts[1]);
            airport.setCountry(parts[2]);
            airport.setIata(parts[3]);
            airport.setLatitude(Float.valueOf(parts[4]));
            airport.setLongitude(Float.valueOf(parts[5]));

            return airport;

        }
        return null;
    }

    public static void readRouteData(String fileName) {
        routes = new ArrayList<>();


        ClassPathResource resource = new ClassPathResource(fileName);
        try (InputStream inputStream = resource.getInputStream()) {
            new BufferedReader(new InputStreamReader(inputStream, StandardCharsets.UTF_8))
                    .lines()
                    .skip(1)
                    .collect(toList())
                    .forEach(data -> routes.add(createRoute(data)));
        } catch (IOException ex) {
            System.out.println(String.join("\n", Stream.of(ex.getStackTrace())
                    .map(StackTraceElement::toString)
                    .collect(toList())));

            throw new Error(ex.toString());
        }


    }

    private static Route createRoute(String line) {

        String[] parts = line.split(",");
        if (parts.length == 3) {

            Route route = new Route();
            route.setAirlineId(parts[0]);
            route.setOrigin(parts[1]);
            route.setDestination(parts[2]);

            return route;
        }

        return null;
    }


    public static void createAdjacentMatrix() {
        List<Edge> edges = new ArrayList<>();

        routes.forEach(r -> {

            Optional<Airport> originAirport = airports.parallelStream()
                    .filter(Objects::nonNull)
                    .filter(a -> a.getIata().equals(r.getOrigin()))
                    .findFirst();

            Optional<Airport> destAirport = airports.parallelStream()
                    .filter(Objects::nonNull)
                    .filter(a -> a.getIata().equals(r.getDestination()))
                    .findFirst();

            if (originAirport.isPresent() && destAirport.isPresent()) {
                double distance = getDistance(destAirport.get(), originAirport.get());
                edges.add(new Edge(originAirport.get(), destAirport.get(), distance));
            }

        });

        graph = new Graph(airports, edges);

    }

    private static double getDistance(Airport destAirport, Airport originAirport) {

        return Math.sqrt(Math.pow(destAirport.getLatitude() - originAirport.getLatitude(), 2) +
                Math.pow(destAirport.getLongitude() - originAirport.getLongitude(), 2));
    }
}
