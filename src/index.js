import app from './config/express-config';

app.get('/', (req, res) => {
    res.json({ info: 'Guestlogix Technical Assessment' });
});