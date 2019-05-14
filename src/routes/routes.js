import { getShortestRoute, getAllRoutes } from '../controllers/routes';

module.exports = (app) => {

    app.get('/routes', async (req, res) => {
        try {
            const allRoutes = await getAllRoutes();
            res.send(allRoutes);
        } catch (error) {
            res.status(500).send(error.message);
        }
    });

    app.get('/routes/:origin&:destination', async (req, res) => {
        try {
            const allRoutes = await getAllRoutes();
            const shortestRoute = await getShortestRoute(req.params.origin, req.params.destination, allRoutes);
            res.send(shortestRoute);
        } catch (error) {
            res.status(500).send(error.message);
        }
    });
    
}