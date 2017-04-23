using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Linq;

namespace nrundt.indexer
{
    public sealed class SequenceMap : CsvClassMap<Sequence>
    {
        public SequenceMap()
        {
            Map(m => m.Date).Name("dato");
            Map(m => m.Url).Name("innslag_nettadresse");
            Map(m => m.Title).Name("innslag_tittel");
            Map(m => m.Municipality).Name("innslag_opptak_kommune");
            Map(m => m.MainTheme).Name("innslag_hovedtema");
            Map(m => m.Special).ConvertUsing(CommaSeparatedStringToArray("Innslag_spesielt"));
            Map(m => m.Clothes).ConvertUsing(CommaSeparatedStringToArray("medvirkende_antrekk"));
        }

        private static Func<ICsvReaderRow, string[]> CommaSeparatedStringToArray(string name)
        {
            return row => row
                .GetField<string>(name)
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .ToArray();
        }
    }
}