package com.guestlogix.demo.model;

public class Edge {
    private Airport source;
    private Airport destination;
    private double distance;

    public Edge(Airport source, Airport destination, double distance) {
        this.source = source;
        this.destination = destination;
        this.distance = distance;
    }

    public Airport getDestination() {
        return destination;
    }

    public Airport getSource() {
        return source;
    }

    public double getDistance() {
        return distance;
    }

    @Override
    public String toString() {
        return source + " " + destination;
    }


}