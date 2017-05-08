using nrundt.indexer.utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace nrundt.indexer
{
    class Program
    {
        static void Main(string[] args)
        {
            VerifyArguments(args);

            Log("It's on!");

            // Init AutoMapper
            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<Sequence, SequenceIndexItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Base64.SafeUrlEncode(src.Title))));

            // Parse CSV file
            var parser = new Parser(@"../resources/norge_rundt_statistikkmoro_2016.csv");
            var sequences = parser.GetResult<Sequence, SequenceMap>().ToList();
            Log($"Got {sequences.Count} sequences");

            // Create search index and add result to index
            using (var indexClient = new AzureIndexer(args[0], args[1]))
            {
                indexClient.IndexDocuments("norgerundt", AutoMapper.Mapper.Map<IReadOnlyList<SequenceIndexItem>>(sequences));
            }

            Log("...and we're done");
        }

        private static void VerifyArguments(IReadOnlyCollection<string> args)
        {
            if (args == null || args.Count != 2)
            {
                Console.WriteLine("Error: Pass in Azure Search instance name and admin keys as arguments");
                Environment.Exit(1);
            }
        }

        private static void Log(string text)
        {
            Console.WriteLine(text);
        }
    }
}