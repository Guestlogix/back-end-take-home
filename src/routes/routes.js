import { getShortestRoutes } from '../controllers/routes';

module.exports = (app) => {
    app.get('/', (req, res) => {
        res.json({ info: 'Guestlogix Technical Assessment' });
    });

    app.get('/routes/:origin&:destination', async (req, res) => {
        try {
            const shortestRoutes = await getShortestRoutes(req.params.origin, req.params.destination);
            res.send(shortestRoutes);
        } catch (err) {
            res.status(500);
        }
    });
}