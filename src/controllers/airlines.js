import { listAllAirlines } from '../models/airlines';

export const getAllAirlines = (origin, destination) => {

    return listAllAirlines().then(result => {
        return result;
    }).catch(error => {
        console.log(error);
        return error;
    });
 
};