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
    public class RouteRepository
    {
        private string _path;
        public RouteRepository(string path)
        {
            _path = path;
        }
        public List<Route> GetAll()
        {
            var errorMessage = String.Empty;
            try
            {
                errorMessage = "Error reading CSV";
                var lines = ReaderHelper.ReadCSV(_path);
                errorMessage = "Error Converting CSV to Routes";
                var routes = Route.ConvertList(lines, ',');
                return routes;
            }
            catch (Exception e)
            {
                throw new Exception(errorMessage, e);
            }
        }
    }
}
