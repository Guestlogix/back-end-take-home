using System.Collections.Generic;

namespace RouteSearch.Infrastructure.Interfaces
{
    public interface IFileReader
    {
         IEnumerable<string> Read(string filePath);
    }
}