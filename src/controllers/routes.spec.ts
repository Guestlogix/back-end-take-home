import request from 'supertest';
import app from '../app';

describe('Route Controller', () => {
    test('should THROW an error if no origin or destination is supplied in query', async () => {
        const response = await request(app).get('/');
        expect(response.status).toBe(400);
        expect(response.badRequest).toBeTruthy();
        expect(response.body.message).toBe('You must supply both an origin and a destination route');
    });

    test('should return an empty array if no connecting flights are found', async () => {
        const response = await request(app).get('/?origin=FAK&destination=CPH');

        expect(response.status).toBe(404);
        expect(response.notFound).toBeTruthy();
        expect(response.body.message).toBe('No connecting route exists between the supplied origin and destination');
    });

    test('should return an empty connecting flights', async () => {
        const response = await request(app).get('/?origin=ABJ&destination=CPH');

        expect(response.status).toBe(200);
        expect(response.ok).toBeTruthy();
        expect(response.body.data.length).toBeGreaterThan(0);
    });
});
