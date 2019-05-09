const express = require('express');
const app = express();
const keys = require('./config/keys');
const server = require('./server/server');

server({
    app,
    port: keys.port
})