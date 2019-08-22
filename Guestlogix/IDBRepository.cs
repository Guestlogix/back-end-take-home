using System.Collections.Generic;

public interface IDBRepository{
    IEnumerable<Route> GetRoutes();
    IEnumerable<Airport> GetAirports();
    IEnumerable<Airline> GetAirlines();
    bool HasAirport(string airport);
}