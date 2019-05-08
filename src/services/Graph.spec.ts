import Graph from './Graph';
import csv from 'csvtojson';

describe('Graph Service', () => {
    let graph: Graph;

    beforeEach(async () => {
        graph = new Graph();

        // load the routes csv file and format it's content to json
        const routes: any[] = await csv().fromFile('data/routes.csv');

        /** Add each origin and destination as a node in our graph instance */
        routes.map(route => {
            graph.addNode(route['Origin']);
            graph.addNode(route['Destination']);
        });

        /** establish the relationship between the origin and destination routes */
        routes.map(route => {
            graph.addEdge(route['Origin'], route['Destination'], route['Airline Id']);
        });
    });

    test('should return an empty array if no connecting flights are found', async () => {
        // find the shortest path from the starting position
        const paths = graph.findShortestPath('AJ', 'NF');

        expect(paths.length).toEqual(0);
    });

    test('should return same route if origin & destination are the same and exists', async () => {
        // find the shortest path from the starting position
        const paths = graph.findShortestPath('ABJ', 'ABJ');

        expect(paths.length).toEqual(1);
    });

    test('should return same route if origin & destination are the same and exists', async () => {
        // find the shortest path from the starting position
        const paths = graph.findShortestPath('ABJ', 'CPH');

        expect(paths.length).toBeGreaterThan(0);
    });
});
