package com.guestlogix.demo.utilities;

import com.guestlogix.demo.model.Airport;
import com.guestlogix.demo.model.Edge;
import com.guestlogix.demo.model.Graph;

import java.util.*;


public class DijkstraAlgorithm {

    private final List<Edge> edges;
    private Set<Airport> settledNodes;
    private Set<Airport> unSettledNodes;
    private Map<Airport, Airport> predecessors;
    private Map<Airport, Double> distance;

    public DijkstraAlgorithm(Graph graph) {
        // create a copy of the array so that we can operate on this array
        this.edges = new ArrayList<>(graph.getEdges());
    }

    public void execute(Airport source) {
        settledNodes = new HashSet<>();
        unSettledNodes = new HashSet<>();
        distance = new HashMap<>();
        predecessors = new HashMap<>();
        distance.put(source, (double) 0);
        unSettledNodes.add(source);
        while (unSettledNodes.size() > 0) {
            Airport node = getMinimum(unSettledNodes);
            settledNodes.add(node);
            unSettledNodes.remove(node);
            findMinimalDistances(node);
        }
    }

    private void findMinimalDistances(Airport node) {
        List<Airport> adjacentNodes = getNeighbors(node);
        for (Airport target : adjacentNodes) {
            if (getShortestDistance(target) > getShortestDistance(node)
                    + getDistance(node, target)) {
                distance.put(target, getShortestDistance(node)
                        + getDistance(node, target));
                predecessors.put(target, node);
                unSettledNodes.add(target);
            }
        }

    }

    private double getDistance(Airport node, Airport target) {
        for (Edge edge : edges) {
            if (edge.getSource().equals(node)
                    && edge.getDestination().equals(target)) {
                return edge.getDistance();
            }
        }
        throw new RuntimeException("Should not happen");
    }

    private List<Airport> getNeighbors(Airport node) {
        List<Airport> neighbors = new ArrayList<>();
        for (Edge edge : edges) {
            if (edge.getSource().equals(node)
                    && !isSettled(edge.getDestination())) {
                neighbors.add(edge.getDestination());
            }
        }
        return neighbors;
    }

    private Airport getMinimum(Set<Airport> vertexes) {
        Airport minimum = null;
        for (Airport vertex : vertexes) {
            if (minimum == null) {
                minimum = vertex;
            } else {
                if (getShortestDistance(vertex) < getShortestDistance(minimum)) {
                    minimum = vertex;
                }
            }
        }
        return minimum;
    }

    private boolean isSettled(Airport vertex) {
        return settledNodes.contains(vertex);
    }

    private double getShortestDistance(Airport destination) {
        Double d = distance.get(destination);
        if (d == null) {
            return Double.MAX_VALUE;
        } else {
            return d;
        }
    }

    /*
     * This method returns the path from the source to the selected target and
     * NULL if no path exists
     */
    public LinkedList<Airport> getPath(Airport target) {
        LinkedList<Airport> path = new LinkedList<>();
        Airport step = target;
        // check if a path exists
        if (predecessors.get(step) == null) {
            return null;
        }
        path.add(step);
        while (predecessors.get(step) != null) {
            step = predecessors.get(step);
            path.add(step);
        }
        // Put it into the correct order
        Collections.reverse(path);
        return path;
    }

}