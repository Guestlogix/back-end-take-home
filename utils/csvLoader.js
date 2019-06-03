const csv = require('csvtojson/v2')
const path = require('path')
module.exports = {
/**
 * Load the CSV file.
 *
 * @param {string} filePath - file path to load file frome
 * @return {JSON} CSV to JSON
 *
 */
  async loadCSVToJSON (filePath) {
    const result = await csv().fromFile(path.join(__dirname, filePath))
    return result
  }
}
