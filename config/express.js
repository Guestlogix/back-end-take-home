const path = require("path");

const mongoose = require("mongoose");
const express = require("express");
const router = express.Router();
const routes = require("../app/routes/index");
const config = require("./config");

mongoose.connect(config.database, { useNewUrlParser: true });

module.exports = () => {
  const app = express();

  app.use("/", router);

  routes(router);

  return app;
};
