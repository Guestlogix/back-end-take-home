using System.Collections.Generic;
using System.Linq;

public class RouteService : IRouteService{
    private IEnumerable<Route> _repo;

    public RouteService(IDBRepository repo){
        _repo = repo.GetRoutes();
    }

    public string GetShortestRoute(string origin, string destination){
        if(_repo.Where(r => r.Destination == destination).Count() == 0)
            return "There are no flights available to " + destination;

        if(_repo.Where(r => r.Origin == origin).Count() == 0)
            return "There are no flights available from " + origin;

        var routeList = new List<(string key, string value)> ();
        var routes = _repo.Where(r => r.Destination == destination);

        bool foundShortest = false;
        bool moreRoutes = true; 
        var currentRoute = 0;
        var result = "";
        while(!foundShortest && moreRoutes){

            if(routeList.Count() == 0){
                foreach(Route r in routes){
                    routeList.Add((r.Origin, r.Origin + " => " + r.Destination));
                    if(r.Origin == origin){
                        foundShortest = true;
                        result = r.Origin + " => " + r.Destination;
                    }
                }
            }
            else{
                var airport = routeList[currentRoute].key;
                var pathSoFar = routeList[currentRoute].value;

                if(airport == origin){
                    foundShortest = true;
                    result = pathSoFar;
                }
                else if(pathSoFar != null){
                    routes = _repo.Where(r => r.Destination == airport);
                    foreach(Route r in routes){
                        if(r.Origin == origin){
                            routeList.Add((r.Origin, r.Origin + " => " + pathSoFar));
                            result = r.Origin + " => " + pathSoFar;
                            foundShortest = true;
                        }
                        else if(r.Origin == destination)
                            routeList.Add((r.Origin, null));
                        else{
                            routeList.Add((r.Origin, r.Origin + " => " + pathSoFar));
                        }
                    }
                }

                currentRoute++;
                if(currentRoute >= routeList.Count())
                    moreRoutes = false;
            }
        }

        if(foundShortest)
            return result;
        else
            return "There are no flights available from " + origin + " to " + destination;
    }
}