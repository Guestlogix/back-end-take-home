module.exports = {
    "GET": {
        "validate": 'params',
        "schema": {
            "type": "object",
            "properties": {
               "origin": {
                    "type": 'string'
                },
                "destination": {
                    "type": 'string'
                }
            },
            "required": ['origin', 'destination']
        }
    }
}