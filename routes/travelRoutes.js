var express = require('express')
var router = express.Router()
const validator = require('express-validation')
const modelValidation = require('./validation/travelRoutes')
const errorMessagesHandler = require('./../utils/errorMessagesHandler')
const Routes = require('../models/routes')
const Airlines = require('../models/airlines')
const Airports = require('../models/airports')
// Explicit load functions for extensibility in the future

Routes.loadItems()
Airlines.loadItems()
Airports.loadItems()

/**
 * @api {get} / Gets the shortest path from given origin and destination
 * @apiName GetRoutes
 * @apiGroup TravelRoutes
 * @apiDescription Provided origin and destination, it finds the shortest path between the two locations.
 *
 *
 * @apiParam (url) {String}   origin
 *                               3 character code for the origin. E.g YYZ
 * @apiParam (url) {String}   destination
 *                               3 character code for the destination. E.g YYZ
 *
 * @apiSuccess {String}       The shortest path with no details E.g "DEN=>YVR=>YQZ"
 * @apiError   {String}       The error in human readable format
 *
 */
router.get('/', validator(modelValidation.getRoute), async (req, res, next) => {
  try {
    const results = Routes.findShortestPath(req.query.origin, req.query.destination)
    return res.json(results.join('=>'))
  } catch (err) {
    const result = errorMessagesHandler.transformRouteErrorToMeaningfull(err.message)
    return res.status(result.status).send(result)
  }
},
router.get('/details', validator(modelValidation.getRoute), async (req, res, next) => {
  try {
    // Find shortest Path
    const summary = Routes.findShortestPath(req.query.origin, req.query.destination)
    // Unitize Routes
    let responseDetails = Routes.splitMultipleRoutesToUnits(summary)
    // Add Airline Details
    responseDetails = Airlines.getAirlineDetails(responseDetails)
    // Add Airport Details
    responseDetails = Airports.getAirportDetails(responseDetails)
    const finalResponse = { summary: summary.join('=>'), details: responseDetails }
    res.send(finalResponse)
  } catch (err) {
    console.log(err)
    // let result = errorMessagesHandler.transformRouteErrorToMeaningfull(err)
    // return res.status(result.status).send(errorMessagesHandler.message)
  }
})
)

module.exports = router
