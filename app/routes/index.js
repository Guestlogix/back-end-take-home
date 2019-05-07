const flightRoutes = require('./route')

module.exports = (router) => {
  flightRoutes(router);

  router.route('/*')
    .get((req, res) => {
      res.send(`Welcome to Guestlogix Flights API, visit "${req.path}/api/route?source=xxx&destination=YYY" to find routes`);
    });
};