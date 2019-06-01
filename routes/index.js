var express = require('express')
var router = express.Router()
const validator = require('express-validation')
const modelValidation = require('../routes/validation-model/index')

/* GET regular */
router.get('/', validator(modelValidation.getRoute), function (req, res, next) {
  res.send('respond with a resource')
  
})

module.exports = router
