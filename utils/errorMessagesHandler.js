module.exports = {

  transformJoiErrorToMeaningfull (err) {
    let errorMessage = []
    for (let errorDetail of err.errors) {
      if (errorDetail.messages[0] === '"destination" contains an invalid value') {
        errorMessage.push([`location: ${errorDetail.location}`, 'origin and destination cannot be same'])
      } else {
        errorMessage.push([`location: ${errorDetail.location}`, errorDetail.messages[0]])
      }
    }
    err.errors = errorMessage
    delete err.statusText
    return err
  },

  transformRouteErrorToMeaningfull (err) {
    if (err === 'Destination node is not in the graph') {
      return ({ status: '404', errors: ['Cannot find the destination provided'] })
    }

    if (err === 'Source node is not in the graph') {
      return ({ status: '404', errors: ['Cannot find the origin provided'] })
    }
  }
}
