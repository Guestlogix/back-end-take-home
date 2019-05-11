const { ERROR_CODES } = require('../lib/contants');

class BaseController {
    constructor(){
    }
    transformResponse(res, status, data, message){
        if (!status){
            if(ERROR_CODES[data]){
                return res.status(ERROR_CODES[data]).json({
                    status: status,
                    error: {
                        type: data,
                        message: message
                    },
                    data: {}
                })
            }else {
                return res.status(400).json({
                    status: status,
                    error: {
                        type: data,
                        message: message
                    },
                    data: {}
                })
            }
        }
        return res.json({
            status: status,
            message: message,
            data: data
        })
    }
}

module.exports = BaseController;