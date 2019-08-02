using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace GuestLogixRouteAPI.Models
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext() : base()
        {

        }
        public DbSet<AirportRoute> routeAirports { get; set; }
    }
}