const BaseController = require('./baseController');

class Routes extends BaseController {
    constructor(lib){
        super();
        this.lib = lib;
    }

    async index(req, res, next) {

    }
}

module.exports = Routes