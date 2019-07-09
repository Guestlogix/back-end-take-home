using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Database.Helpers
{
    public static class ReaderHelper
    {
        public static List<String> ReadCSV(string path)
        {
            var result = File.ReadLines(path).ToList();
            return result;
        }

    }
}
