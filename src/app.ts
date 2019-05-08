import express from 'express';
import config from './config';
import { shortestPath } from './controllers';

// Create a new express application instance
const app: express.Application = express();

/** Primary routes definition */
app.get('/', shortestPath);

// 404 Handler
app.use((req, res, next) => {
    res.status(404).send({
        code: 'E_NOT_FOUND',
        message: 'Resource not found. Could be available at a later time',
    });
});

// Error Handling
app.use((err: any, req: express.Request, res: express.Response) => {
    res.status(500).send({
        code: 'E_INTERNAL_SERVER',
        message: err.message || 'Something seriously went wrong.',
        error: config.environment === 'development' ? err : null,
    });
});

// handle all uncaught promise rejections
process.on('unhandledRejection', error => {
    console.error('Uncaught Error', error);
    process.exit(1);
});

export default app;
