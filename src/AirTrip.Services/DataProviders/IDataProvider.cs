using System.Collections.Generic;

namespace AirTrip.Services.DataProviders
{
    public interface IDataProvider<out TResult>
    {
        IReadOnlyCollection<TResult> GetData();
    }
}