using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.Modules.Interfaces;

namespace Routes.Infra.Data.Components
{
    /// <summary>
    ///     Load and Convert CSV to Class with Interface IImportCSV
    /// </summary>
    public class LoaderCsv
    {
        private const char Delimiter = ',';

        public static List<T> Load<T>(TextReader reader) where T : class, IImportCsv, new()
        {
            if (reader == null) return null;
            var lines = new List<string>();

            #region ReadLines
            string line;
            while ((line = reader.ReadLine()) != null)
                lines.Add(line);
            #endregion

            var _class = new T();
            var records = lines.Skip(1).Select(x => (T)_class.FromCsv(LineSplitter(x).ToArray())).ToList();

            return records;
        }

        private static IEnumerable<string> LineSplitter(string line)
        {
            var fieldStart = 0;
            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] == Delimiter)
                {
                    yield return line.Substring(fieldStart, i - fieldStart);
                    fieldStart = i + 1;
                }

                if (line[i] != '"') continue;

                for (i++; line[i] != '"'; i++) { }
            }
            if (fieldStart < line.Length)
                yield return line.Substring(fieldStart, line.Length - fieldStart);
        }
    }
}
