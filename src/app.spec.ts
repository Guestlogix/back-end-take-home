import request from 'supertest';
import app from './app';

describe('App Root', () => {
    test('should handle error if route NOT found', async () => {
        const response = await request(app).get('/fakePath');
        expect(response.status).toBe(404);
        expect(response.notFound).toBeTruthy();
        expect(response.body.message).toBe('Resource not found. Could be available at a later time');
    });
});
