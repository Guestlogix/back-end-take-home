var app = require("./config/express")();
const config = require("./config/config");
const port = process.env.PORT || 3000;

app.listen(port, function() {
  console.log("App running live on: " + port);
});

module.exports = app;
