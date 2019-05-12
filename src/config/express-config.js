import express from 'express';
import routes from '../routes/routes';
import airlines from '../routes/airlines';
import airports from '../routes/airports';
import { json, urlencoded } from 'body-parser';

const app = express();
const port = 3000;

app.use(json());
app.use(
  urlencoded({
    extended: true,
  })
);

routes(app);
airlines(app);
airports(app);

app.listen(port, () => {
    console.log(`App running on port ${port}.`);
});

export default app;