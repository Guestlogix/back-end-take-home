import csv from 'csvtojson';

export const listAllRoutes = async () => {

    const csvFilePath = './data/routes.csv';

    const jsonObj = await csv().fromFile(csvFilePath);

    return jsonObj;
}