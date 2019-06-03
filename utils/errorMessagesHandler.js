module.exports = {

  /**
 * Error message handler from JOI validations.
 *
 * @param {string} err - Error from JOI
 * @return {JSON} error in human readable format
 *
 */
  transformJoiErrorToMeaningfull (err) {
    const errorMessage = []
    for (const errorDetail of err.errors) {
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
  /**
 * Error message handler from internal.
 *
 * @param {string} err - Error message from internal
 * @return {JSON} error in human readable format
 *
 */
  transformRouteErrorToMeaningfull (err) {
    switch (err) {
      case 'Destination node is not in the graph' :
        return ({ status: '404', errors: ['Cannot find the destination provided'] })
      case 'Source node is not in the graph' :
        return ({ status: '404', errors: ['Cannot find the origin provided'] })
      default:
        return ({ status: '500', errors: [err] })
    }
  }
}
