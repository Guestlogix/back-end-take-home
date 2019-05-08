import csv from 'csvtojson';
import { Request, Response } from 'express';
import { File, Graph } from '../services';
import config from '../config';

export const shortestPath = async (req: Request, res: Response) => {
    try {
        const { origin, destination } = req.query;

        if (!origin || !destination) {
            return res.status(400).json({
                code: 'E_BAD_REQUEST',
                message: 'You must supply both an origin and a destination route',
            });
        }

        // load the routes csv file and format it's content to json
        const routes: any[] = await csv().fromFile('data/routes.csv');

        // instantiate our graph util
        const graph = new Graph();

        /** Add each origin and destination as a node in our graph instance */
        routes.map(route => {
            graph.addNode(route['Origin']);
            graph.addNode(route['Destination']);
        });

        /** establish the relationship between the origin and destination routes */
        routes.map(route => {
            graph.addEdge(route['Origin'], route['Destination'], route['Airline Id']);
        });

        // find the shortest path from the starting position
        const paths = graph.findShortestPath(origin, destination);

        // if no connecting paths found, just return a meaningful message
        if (paths.length === 0) {
            return res.status(404).json({
                code: 'E_NOT_FOUND',
                message: 'No connecting route exists between the supplied origin and destination',
            });
        }

        // get and parse the content of our airports csv file
        const fileInstance = await File.getInstance().loadFile('data/airports.csv');
        const airports = await fileInstance.parseContent();

        const data: string[] = []; // variable to hold our array of connecting flights

        // get the airport for each found connecting route
        paths.map(path => data.push(airports[path].name));

        return res.status(200).send({
            code: 'OK',
            message: 'Connecting routes found',
            data,
        });
    } catch (error) {
        return res.status(500).send({
            code: 'E_INTERNAL_SERVER',
            message: error.message || 'Something seriously went wrong.',
            error: config.environment === 'development' ? error : null,
        });
    }
}
