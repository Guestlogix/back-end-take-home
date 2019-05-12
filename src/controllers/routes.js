import { listAllRoutes } from '../models/routes';

export const getShortestRoutes = (origin, destination) => {

    
 
};

filterShortestRoutes = (origin, destination, shortRoutes) => {

    // const allRoutes = listAllRoutes().then(result).allRoutes.then(routes);

    
}

export const getAllRoutes = (origin, destination) => {

    return listAllRoutes().then(result => {
        return result;
    }).catch(error => {
        console.log(error);
        return error;
    });
 
};