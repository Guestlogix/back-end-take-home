/**
 * Config Interface
 * 
 * Contract for our environment configuration
 */
export default interface Config {
    // the server port
    port?: number | string;

    // current node environment
    environment?: string;
}
