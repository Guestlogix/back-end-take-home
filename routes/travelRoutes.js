var express = require('express')
var router = express.Router()
const validator = require('express-validation')
const modelValidation = require('./validation-model/travelRoutes')

/* GET regular */
router.get('/', validator(modelValidation.getRoute), async (req, res, next) => {
  res.status(200).send('ok')
})

module.exports = router
