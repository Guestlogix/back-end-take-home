

var csv = require("fast-csv");
const lib = require('../lib');
const bodyParser = require('body-parser');
const fs = require('fs');
const dir = './data/';
const dataFolder = './data';
let collectionNames = fs.readFileSync(dir + 'collectionName.txt', 'utf8').toString().split('\n');

module.exports = (options) => {
    if(!options.app){
        throw (new Error('The app has not been started.'));
    }

    const PORT = process.env.PORT || options.port;

    const { app } = options;
    app.use(bodyParser.json());
    
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

    this.lib.db.model('Store')      

    // starting server on configured port
    app.listen(PORT, () => {
        console.log(`Server started successfully on ${PORT}`);
        lib.db.connect(err => {
            if(err) console.log(`Error trying to connect to database: ${err}`);
            console.log('Database service successfully started');
            // init db once
            init();
        })
    });

    let init = async function () {
        let id = 0;
        fs.readFileSync(dataFolder).forEach((file, i) => {
            if(file[0] !== '.' && file !== 'collectionName.txt'){
                var stream = fs.createReadStream(file[i]);
                let model = await lib.db.model(collectionNames[id]).find()
                if(!model){
                    const csvStream = csv()
                    .on("data", function(data) {
                        let newData = extractDataFromCsv(collectionNames[id], data)
                        model = this.lib.db.model(collectionNames[id])(body)
                    })
                }
                id += 1
            }
        })
    };

    function extractDataFromCsv(collection, data) {
        switch(collection){
            case 
        }
    }
}