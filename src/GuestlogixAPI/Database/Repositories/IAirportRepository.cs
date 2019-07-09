using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Model;

namespace Database.Repositories
{
    public interface IAirportRepository
    {
        List<Airport> GetAll();
    }
}
