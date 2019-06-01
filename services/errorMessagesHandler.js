module.exports = {

  transformErrorToMeaningfull (err) {
    console.log(JSON.stringify(err))
    let errorMessage = []
    for (let errorDetail of err.errors) {
      errorMessage.push([ `location: ${errorDetail.location}`, errorDetail.messages[0] ])
    }
    err.errors = errorMessage
    return err
  }
}
