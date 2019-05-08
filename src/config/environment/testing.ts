import Config from '../config.interface';

const config: Config = {
    port: process.env.SERVICE_PORT || 5190,
};

export default config;
