using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace nrundt.indexer
{
    public class Parser
    {
        private readonly string _filename;

        public Parser(string filename)
        {
            _filename = filename;
        }

        public IEnumerable<T> GetResult<T, TMap>() where TMap : CsvClassMap
        {
            var csvConfiguration = new CsvConfiguration
            {
                Delimiter = ";",
            };

            csvConfiguration.RegisterClassMap<TMap>();

            using (var stream = File.OpenText(_filename))
            {
                using (var csv = new CsvReader(stream, csvConfiguration))
                {
                    return csv.GetRecords<T>().ToList();
                }
            }
        }
    }
}