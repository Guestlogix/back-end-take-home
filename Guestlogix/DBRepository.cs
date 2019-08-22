using System.Collections.Generic;
using System.Linq;

public class DBRepository : IDBRepository{
    private readonly DBContext _context;
    public DBRepository(DBContext context){
        _context = context;
    }

    public IEnumerable<Route> GetRoutes(){
        return _context.Route.ToList();
    }

    public IEnumerable<Airport> GetAirports(){
        return _context.Airport.ToList();
    }

    public IEnumerable<Airline> GetAirlines(){
        return _context.Airline.ToList();
    }

    public bool HasAirport(string airport){
        return _context.Airport.Where(a => a.Iata3 == airport).Count() != 0;
    }
}