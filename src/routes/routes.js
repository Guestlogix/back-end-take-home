import { getShortestRoutes, getAllRoutes } from '../controllers/routes';

module.exports = (app) => {

    app.get('/routes', async (req, res) => {
        try {
            const allRoutes = await getAllRoutes();
            res.send(allRoutes);
        } catch (error) {
            res.status(500);
        }
    });

    app.get('/routes/:origin&:destination', async (req, res) => {
        try {
            const shortestRoutes = await getShortestRoutes(req.params.origin, req.params.destination);
            res.send(shortestRoutes);
        } catch (error) {
            res.status(500);
        }
    });
}