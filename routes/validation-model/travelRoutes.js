var Joi = require('joi')

module.exports = {
  getRoute: {
    query: Joi.object().keys({
      origin: Joi.string().trim().length(3).required(),
      destination: Joi.string().trim().length(3).required().disallow(Joi.ref('origin'))
    })
  }
}
