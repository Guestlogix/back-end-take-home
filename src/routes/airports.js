import { getAllAirports } from '../controllers/airports';

module.exports = (app) => {

    app.get('/airports', async (req, res) => {
        try {
            const allAirports = await getAllAirports();
            res.send(allAirports);
        } catch (error) {
            res.status(500).send(error.message);
        }
    });
    
}