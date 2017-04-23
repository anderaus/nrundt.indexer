using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.IO;
using System.Linq;

namespace nrundt.indexer
{
    class Program
    {
        static void Main(string[] args)
        {
            Log("It's on!");

            IndexFile(@"..\resources\norge_rundt_statistikkmoro_2016_subset.csv");

            Log("...and we're done");
        }

        private static void IndexFile(string csvFile)
        {
            Log($"Working on file: {csvFile}");

            var csvConfiguration = new CsvConfiguration
            {
                Delimiter = ";"
            };

            csvConfiguration.RegisterClassMap<SequenceMap>();

            using (var stream = File.OpenText(csvFile))
            {
                using (var csv = new CsvReader(stream, csvConfiguration))
                {
                    var records = csv.GetRecords<Sequence>().ToList();
                    Log($"Got {records.Count} records");
                }
            }
        }

        private static void Log(string text)
        {
            Console.WriteLine(text);
        }
    }
}