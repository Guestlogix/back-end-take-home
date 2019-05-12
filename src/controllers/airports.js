import { listAllAirports } from '../models/airports';

export const getAllAirports = (origin, destination) => {

    return listAllAirports().then(result => {
        return result;
    }).catch(error => {
        console.log(error);
        return error;
    });
 
};