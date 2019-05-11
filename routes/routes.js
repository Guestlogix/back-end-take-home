const { Routes } = require('../controllers');

module.exports = (lib, app) => {
    app.get('/routes?', async (req, res, next) =>{
        new Routes(lib).index(req, res, next)
    });
}