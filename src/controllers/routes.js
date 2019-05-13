import { listAllRoutes } from '../models/routes';
import { getAllAirports } from './airports';
import { distanceCalculation } from '../helpers/distance-calc';
import Graph from 'graph-data-structure';

export const getShortestRoutes = async (origin, destination, routes) => {
 
    const graph = await buildRoutesGraph(routes);

    const shortestPath = graph.shortestPath(origin, destination);

    return shortestPath;
};

export const getAllRoutes = () => {

    const allRoutes = listAllRoutes().then(result => {
        return result;
    }).catch(error => {
        console.log(error);
        return error;
    });
    
    return allRoutes;
};

const buildRoutesGraph = (routes) => {
    let graph = new Graph();

    routes.forEach(route => {
        // let routeWeight = findRouteWeight(route.Origin, route.Destination).then(weight => weight);

        // console.log(routeWeight);

        graph.addNode(route.Origin);
        graph.addEdge(route.Origin, route.Destination);
    });

    return graph;
}

const findRouteWeight = async (origin, destination) => {
    const allAirports = await getAllAirports();

    const airportOrigin = allAirports.filter(airport => {
        if (airport["IATA 3"] == origin) {
            return airport;
        }
    });

    const airportDestination = allAirports.filter(airport => {
        if (airport["IATA 3"] == destination) {
            return airport;
        }
    });

    return distanceCalculation(airportOrigin.Latitute, airportOrigin.Longitude, airportDestination.Latitute, airportDestination.Longitude);
} 