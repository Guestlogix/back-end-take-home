﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core;
using AirTrip.Core.Models;

namespace AirTrip.Services.Services
{
    public interface IRouteService
    {
        Task<IReadOnlyCollection<Route>> GetAllRoutesAsync(CancellationToken cancellationToken);
    }
}