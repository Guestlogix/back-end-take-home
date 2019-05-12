import csv from 'csvtojson';

export const listAllAirports = async () => {

    const csvFilePath = './data/airports.csv';

    const jsonObj = await csv().fromFile(csvFilePath);
    
    return jsonObj;
}