var createError = require('http-errors')
var express = require('express')
var cookieParser = require('cookie-parser')
var logger = require('morgan')

var travelRoutes = require('./routes/travelRoutes')

var app = express()

const errorMessagesHandler = require('./utils/errorMessagesHandler')
app.use(logger('dev'))
app.use(express.json())
app.use(express.urlencoded({ extended: false }))
app.use(cookieParser())

app.use('/', travelRoutes)

// catch 404 and forward to error handler
app.use(function (req, res, next) {
  next(createError(404))
})

// error handler
app.use(function (err, req, res, next) {
  // set locals, only providing error in development
  res.locals.message = err.message
  res.locals.error = req.app.get('env') === 'development' ? err : {}
  // render the error page
  res.status(err.status || 500)
  // build better error messages from JOI validations of Input
  if (err.status === 400) {
    const errToUser = errorMessagesHandler.transformErrorToMeaningfull(err)
    res.send(errToUser)
  } else {
    res.send(err)
  }
})

module.exports = app
