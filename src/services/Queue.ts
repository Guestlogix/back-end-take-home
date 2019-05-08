export default class PriorityQueue {
    constructor(private collection: any[] = []) {}

    /**
     * Since we are giving all other routes aside the origin route a weight of 1,
     * we can simply push the current element into the collection array without worrying about order
     *
     * @param element An array of value and it's weight ['ABJ', 1]
     */
    enqueue(element: any[]) {
        this.collection.push(element);
    }

    /** Removes and return the next item in the queue */
    dequeue() {
        const value = this.collection.shift();
        return value;
    }

    /** Checks if the queue is empty */
    isEmpty() {
        return (this.collection.length === 0);
    }
}
