﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core;

namespace AirTrip.Services.Services
{
    internal interface IAirportService
    {
        Task<IReadOnlyCollection<Airport>> GetAirportsAsync(CancellationToken token);
    }
}