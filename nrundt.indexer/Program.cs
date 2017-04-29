using System;
using System.Linq;

namespace nrundt.indexer
{
    class Program
    {
        static void Main(string[] args)
        {
            Log("It's on!");

            // Parse CSV file
            var parser = new Parser(@"..\resources\norge_rundt_statistikkmoro_2016_subset.csv");
            var result = parser.GetResult<Sequence, SequenceMap>().ToList();
            Log($"Got {result.Count} sequences");

            // TODO: Index each episode segment

            Log("...and we're done");
        }

        private static void Log(string text)
        {
            Console.WriteLine(text);
        }
    }
}