import Config from '../config.interface';

const config: Config = {
    port: process.env.SERVICE_PORT || 4100,
};

export default config;
