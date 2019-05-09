

var csv = require("fast-csv");
const lib = require('../lib');
const bodyParser = require('body-parser');
const fs = require('fs');
const dir = './data/';
const dataFolder = './data'
const { APP_DATA } = require('../lib/contants');

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
   
    // starting server on configured port
    app.listen(PORT, () => {
        console.log(`Server started successfully on ${PORT}`);
        lib.db.connect(err => {
            if(err) console.log(`Error trying to connect to database: ${err}`);
            console.log('Database service successfully started');
            init();
        })
    });

    let init = async function () {
        
        fs.readdirSync(dataFolder).forEach(async (file) => {
            let [fileName, extention] = file.split('.');
            if(file[0] !== '.'){
                try{
                    fileName = fileName.toUpperCase()
                    collectionName = APP_DATA[fileName];
                    let model = await lib.db.model(collectionName).find()
                    // This ensure the database is populated once
                    if(model.length === 0){
                        var stream = fs.createReadStream(dir + file)
                        const csvStream = csv()
                        .on("data", async function(data) {
                            if (fileName === 'AIRLINES'){
                                let newData = {
                                    Name: data[0],
                                    Two_Digit_Code: data[1],
                                    Three_Digit_Code: data[2],
                                    Country: data[3]
                                }
                                model = await lib.db.model('Airline')(newData)
                                model.save()
                            }else if (fileName === 'AIRPORTS'){
                               let newData = {
                                    Name: data[0],
                                    City: data[1],
                                    Country: data[2],
                                    IATA_3: data[3],
                                    Latitude: data[4],
                                    Longitude: data[5]
                                }
                                model = await lib.db.model('Airport')(newData)
                                model.save()
                            } else if (fileName === 'ROUTES'){
                                let newData = {
                                    Airline_Id: data[0],
                                    Origin: data[1],
                                    Destination: data[2]
                                }
                                model = await lib.db.model('Route')(newData)
                                model.save()
                            }else {
                                throw new Error('Collection name is invalid')
                            }
                        })
                        .on("end", function() {
                            console.log(`End of file import: ${fileName}`);
                        })
                        stream.pipe(csvStream);
                    }
                }catch(err){
                    console.log('Err here', err)
                }
            }
        })
    };

}