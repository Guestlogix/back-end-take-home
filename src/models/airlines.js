import csv from 'csvtojson';

export const listAllAirlines = async () => {

    const csvFilePath = './data/airlines.csv';

    const jsonObj = await csv().fromFile(csvFilePath);

    return jsonObj;
}
