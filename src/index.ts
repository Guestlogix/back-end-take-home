import app from './app';
import config from './config';

app.listen({ port: config.port }, function () {
    console.log(`App available on http://localhost:${config.port}`);
});
