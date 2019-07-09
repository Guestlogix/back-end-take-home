using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Helpers;
using Database.Model;

namespace Database.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private string _path;
        public AirportRepository(string path)
        {
            _path = path;
        }
        public List<Airport> GetAll()
        {
            var errorMessage = String.Empty;
            try
            {
                errorMessage = "Error reading CSV";
                var lines = ReaderHelper.ReadCSV(_path);
                errorMessage = "Error Converting CSV to Routes";
                var airports = Airport.ConvertToList(lines, ',');
                return airports;
            }
            catch (Exception e)
            {
                throw new Exception(errorMessage, e);
            }
        }
    }
}
