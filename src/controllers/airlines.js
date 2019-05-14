import { listAllAirlines } from '../models/airlines';

export const getAllAirlines = () => {
    return listAllAirlines().then(result => result); 
};