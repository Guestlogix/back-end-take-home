module.exports = function(db){
    return {
        "Airline": require('./airline')(db),
        "Airport": require('./airport')(db),
        "Route": require('./route')(db),
    }
}