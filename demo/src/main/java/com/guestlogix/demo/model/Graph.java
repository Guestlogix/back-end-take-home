package com.guestlogix.demo.model;


import java.util.List;

public class Graph {
    private final List<Airport> vertexes;
    private final List<Edge> edges;

    public Graph(List<Airport> vertexes, List<Edge> edges) {
        this.vertexes = vertexes;
        this.edges = edges;
    }

    public List<Airport> getVertexes() {
        return vertexes;
    }

    public List<Edge> getEdges() {
        return edges;
    }



}