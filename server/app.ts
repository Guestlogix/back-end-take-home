import * as PATH from 'path';
import * as http from 'http';
import * as express from 'express';
import { AddressInfo } from 'net';
import { ShortestPathCalculator } from '../shared/shortest-path';
import { formatResult } from './format-result';
import { Store } from './store';

const app = express();
const viewPath = PATH.normalize(PATH.join(__dirname, '..', 'browser'));
app.use(express.static(PATH.join(viewPath)));
app.get('/', (req, res) => {
	res.render('index.html');
});

// Inlining the route calculation endpoint here
const folder = 'test';
// const folder = 'full';
const store = Store({
	airlinesCsv: `./data/${folder}/airlines.csv`,
	airportsCsv: `./data/${folder}/airports.csv`,
	routesCsv: `./data/${folder}/routes.csv`
});
const pathCalculator = ShortestPathCalculator({ store });
app.get('/route', (req, res) => {
	const results = pathCalculator.calculate(req.query);
	const message = formatResult(results);
	res.send(message);
});

app.use((req, res) => {
	res.status(404).json({ error: 'Not found' });
});

const httpServer = http.createServer(app);
httpServer.on('listening', () => {
	const address = httpServer.address() as AddressInfo;
	console.log(`server is up and running at port: ${address.port}`);
});
const PORT = 3030;
httpServer.listen(PORT);