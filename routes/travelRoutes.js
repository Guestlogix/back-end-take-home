var express = require('express')
var router = express.Router()
const validator = require('express-validation')
const modelValidation = require('./validation-model/travelRoutes')
const Routes = require('../models/routes')
Routes.loadItems()

/* GET regular */
router.get('/', validator(modelValidation.getRoute), async (req, res, next) => {
  let results = Routes.findShortestPath(req.query.origin, req.query.destination)
  console.log('[===========')
  console.log(results)
  console.log('[===========')
  return res.json(results)
})

module.exports = router
