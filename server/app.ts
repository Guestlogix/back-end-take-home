import * as PATH from 'path';
import * as http from 'http';
import * as express from 'express';
import { AddressInfo } from 'net';

const app = express();
const viewPath = PATH.normalize(PATH.join(__dirname, '..', 'browser'));
app.use(express.static(PATH.join(viewPath)));
app.get('/', (req, res) => {
	res.render('index.html');
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