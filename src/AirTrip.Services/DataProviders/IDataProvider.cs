using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AirTrip.Services.DataProviders
{
    public interface IDataProvider<TResult>
    {
        Task<IReadOnlyCollection<TResult>> GetDataAsync(CancellationToken token);
    }
}