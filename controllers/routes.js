const BaseController = require('./baseController');
let createGraph = require('ngraph.graph');
let graph = createGraph();
let path = require('ngraph.path');
const csv = require('csvtojson')
const WeightedGraph = require('../lib/graph')

class Routes extends BaseController {
    constructor(lib){
        super();
        this.lib = lib;
        this.mapdata = {
            allvertex: []
        }
    }

    async index(req, res, next) {
        const { origin, destination } = req.query
        if(typeof(origin) !== 'string' && typeof(destination) !== 'string'){
            next(this.transformResponse(res, false, 'BadRequest', `Invalid origin or destination`))
        }
        try{
            // populate airport
            this.mapdata.allvertex = [...await this.populateAirportData()]
            let graph = new WeightedGraph()
            for (let item of this.mapdata.allvertex){
                graph.addVertex(item.IATA_3)
            }
            // populate routes data
            const routes = await this.transformRouteData()
            for (let path of routes){
                graph.addEdge(path.from, path.to, path.weight)
            }
            const shortestRoutes = graph.dijkstra(origin, destination)
            if(shortestRoutes.length < 1){
                return this.transformResponse(res, true, shortestRoutes, `Sorry, there are currently no available routes from ${origin} to ${destination}. Please check back later.`)
            }
            const searchDetails = this.responseDetails(shortestRoutes)
            console.log(searchDetails)
            return this.transformResponse(res, true, searchDetails, 'Operation successful')         

        }catch(err){
            next(this.transformResponse(res, false, 'InternalServerError', err.message))
        }
    }

    async populateAirportData(){
        const airportData = await csv().fromFile('data/airports.csv')
        const nodesArr = airportData.reduce((theMap, theItem, i) => {
            if (!theMap['IATA 3']){
                theMap.push({
                    Id: i,
                    IATA_3: theItem['IATA 3'],
                    Name:theItem.Name,
                    City: theItem.City,
                    Country: theItem.Country,
                    Latitude: theItem.Latitute,
                    Longitude: theItem.Longitude
                })
                return theMap
            }
        },[])
        return nodesArr;
    }

    async transformRouteData(){
        const routeData = await csv().fromFile('data/routes.csv')
        let i = 0;
        let from;
        let to;
        let pathObj = {}
        const arryPlaceHolder = []
        const airlines = await csv().fromFile('data/airlines.csv')
        for (let item of routeData){
            // checking for valid airlines before adding to routes
            const isValidAirline = airlines.filter(x => x['2 Digit Code'] === item['Airline Id'])[0]
            if (isValidAirline){
                from = this.mapdata.allvertex.filter(y => y.IATA_3 === item.Origin)[0]
                to = this.mapdata.allvertex.filter(y => y.IATA_3 === item.Destination)[0]
                // We are only adding valid origins and destinations to our routes
                if(from && to){
                    let p1 = new this.lib.distanceCalculator.LatLon(from.Latitude, from.Longitude)
                    let p2 = new this.lib.distanceCalculator.LatLon(to.Latitude, to.Longitude)
                    let weight = p1.distanceTo(p2)
                    pathObj.Id = i
                    pathObj.from = from.IATA_3
                    pathObj.to = to.IATA_3,
                    pathObj.weight = weight
                    i++
                    arryPlaceHolder.push(pathObj)
                }
            }
        }
        return arryPlaceHolder
    }

    responseDetails(data){
        let resMap = data.reduce((theMap, theItem, i) => {
            const details = this.mapdata.allvertex.filter(x => x.IATA_3 === theItem)[0]
            theMap[i] = {
                Airport: details.Name,
                City: details.City,
                Country: details.Country,
                IATA_3: details.IATA_3,
                Latitude: details.Latitude,
                Longitude: details.Longitude
            }
            return theMap
        }, {})
        return resMap
    }
}


module.exports = Routes