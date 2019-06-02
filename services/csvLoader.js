const csv = require('csvtojson/v2')
const path = require('path')
module.exports = {

  async loadCSVToJSON (filePath) {
    let result = await csv().fromFile(path.join(__dirname, filePath))
    return result
  }
}
