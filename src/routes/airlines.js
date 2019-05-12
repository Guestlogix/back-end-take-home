import { getAllAirlines } from '../controllers/airlines';

module.exports = (app) => {

    app.get('/airlines', async (req, res) => {
        try {
            const allAirlines = await getAllAirlines();
            res.send(allAirlines);
        } catch (error) {
            res.status(500);
        }
    });

}