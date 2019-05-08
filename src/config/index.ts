import Config from './config.interface';

/** Get the current node environment. Defaults to development environemt */
const env = process.env.NODE_ENV || 'development';

/** Defaults configuration options */
const baseConfig: Config = {
    environment: env,
};

/** Load the correct configuration options based on current environment */
const envConfig: Config = require(`./environment/${env}`).default;

export default { ...baseConfig, ...envConfig };
