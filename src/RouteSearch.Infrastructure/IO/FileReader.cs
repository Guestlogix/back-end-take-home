using System.Collections.Generic;
using System.IO;
using RouteSearch.Infrastructure.Interfaces;

namespace RouteSearch.Infrastructure.IO
{
    public class FileReader
    {
        public static IEnumerable<string> Read(string filePath)
        {
            var lines = new List<string>();

            var isFirstLine = true;

            using(StreamReader file = new StreamReader(filePath))
            {
                string line = string.Empty;
                
                while((line = file.ReadLine()) != null)
                {
                    if (isFirstLine)
                    {
                        isFirstLine = false;
                        continue;
                    }

                    lines.Add(line);                
                }
            }

            return lines;
        }
    }
}