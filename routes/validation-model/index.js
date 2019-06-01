var Joi = require('joi');

module.exports = {
  getRoute: {
    query: {
      origin: Joi.string().trim().length(3).required(),
      destination: Joi.string().trim().length(3).required()
    }
  }
};