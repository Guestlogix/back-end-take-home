const flightRoutes = require('./route')

module.exports = (router) => {
  flightRoutes(router);

  router.route('/*')
    .get((req, res) => {
      res.send(`Welcome to Guestlogix Flights API, route "${req.path}" isn't implemented`);
    });
};