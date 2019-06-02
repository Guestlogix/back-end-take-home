module.exports = {

  transformErrorToMeaningfull (err) {
    let errorMessage = []
    for (let errorDetail of err.errors) {
      if (errorDetail.messages[0] === '"destination" contains an invalid value') {
        errorMessage.push([`location: ${errorDetail.location}`, 'origin and destination cannot be same'])
      } else {
        errorMessage.push([`location: ${errorDetail.location}`, errorDetail.messages[0]])
      }
    }
    err.errors = errorMessage
    return err
  }
}
