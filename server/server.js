
const lib = require('../lib');
const bodyParser = require('body-parser');

module.exports = (options) => {
    if(!options.app){
        throw (new Error('The app has not been started.'));
    }

    const PORT = process.env.PORT || options.port;

    const { app } = options;
    app.use(bodyParser.json());
    app.use(bodyParser.urlencoded({ extended: false }));
    
    // this is validating json request schema
    app.use((req, res, next) => {
        let results = lib.schemaValidator.validateRequest(req);
        if(results.valid){
            return next();
        }
        res.status(400).send(results);
    })

    // Setting up routes
    require('../routes')(lib, app);
   
    // starting server on configured port
    app.listen(PORT, () => {
        console.log(`Server started successfully on ${PORT}`);
    });

}