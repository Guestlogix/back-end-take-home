import { listAllAirports } from '../models/airports';

export const getAllAirports = () => {
    return listAllAirports().then(result => result);
};