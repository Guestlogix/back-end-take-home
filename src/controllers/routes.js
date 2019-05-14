import { listAllRoutes } from '../models/routes';
import { getAllAirports } from './airports';
import { distanceCalculation } from '../helpers/distance-calc';
import Graph from 'graph-data-structure';

export const getAllRoutes = () => {
    return listAllRoutes().then(result => result);
};

export const getShortestRoute = async (origin, destination, routes) => {
    const allAirports = await getAllAirports();

    const graph = await buildRoutesGraph(routes, allAirports);

    const shortestPath = graph.shortestPath(origin, destination);
    
    return shortestPath;
};

const buildRoutesGraph = (routes, airports) => {
    let graph = new Graph();

    routes.forEach(route => {
        let routeWeight = findRouteWeight(route.Origin, route.Destination, airports);

        graph.addNode(route.Origin);
        graph.addEdge(route.Origin, route.Destination, routeWeight);
    });

    return graph;
}

const findRouteWeight = (origin, destination, airports) => {
    
    let latituteOrigin = 0;
    let latituteDestination = 0;
    let longitudeOrigin = 0;
    let longitudeDestination = 0;

    airports.filter(airport => {
        if (airport["IATA 3"] == origin) {
            latituteOrigin = airport.Latitute;
            longitudeOrigin = airport.Longitude;
        }
    });

    airports.filter(airport => {
        if (airport["IATA 3"] == destination) {
            latituteDestination = airport.Latitute;
            longitudeDestination = airport.Longitude;
        }
    });

    return distanceCalculation(latituteOrigin, longitudeOrigin, latituteDestination, longitudeDestination);
} 
