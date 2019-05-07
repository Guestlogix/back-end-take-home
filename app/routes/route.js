const routeCtrl = require("../controllers/route.ctrl");

module.exports = router => {
  router.route('/api/routes').get(routeCtrl.fetchShortestRoute);
}