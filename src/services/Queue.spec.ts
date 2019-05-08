import PriorityQueue from './Queue';

describe('Queue Service', () => {
    let pq: PriorityQueue;

    beforeEach(async () => {
        pq = new PriorityQueue();
    });

    test('queue should be empty', async () => {
        expect(pq.isEmpty()).toBeTruthy();
    });

    test('should enqueue an array of value and it\'s weight[\'ABJ\', 1]', async () => {
        pq.enqueue(['ABJ', 0]);
        expect(pq.isEmpty()).toBeFalsy();
    });

    test('should dequeue the first element in the queue', async () => {
        pq.dequeue();
        expect(pq.isEmpty()).toBeTruthy();
    });
});
