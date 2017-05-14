using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Globalization;
using System.Linq;

namespace nrundt.indexer
{
    public sealed class SequenceMap : CsvClassMap<Sequence>
    {
        public SequenceMap()
        {
            Map(m => m.Date).Name("dato").TypeConverterOption(new CultureInfo("nb-NO"));
            Map(m => m.Url).Name("innslag_nettadresse");
            Map(m => m.Title).Name("innslag_tittel");
            Map(m => m.Municipality).Name("innslag_opptak_kommune");
            Map(m => m.MainTheme).Name("innslag_hovedtema");

            Map(m => m.Special).ConvertUsing(CommaSeparatedStringToArray("Innslag_spesielt"));
            Map(m => m.Clothes).ConvertUsing(CommaSeparatedStringToArray("medvirkende_antrekk"));
            //Map(m => m.Experience).ConvertUsing(CommaSeparatedStringToArray("innslag_opplevelse"));   // Removed due to lack of value/relevance
            Map(m => m.Construction).ConvertUsing(CommaSeparatedStringToArray("innslag_type_bygg_og_industri"));
            Map(m => m.Historical).ConvertUsing(CommaSeparatedStringToArray("innslag_type_historisk"));
            //Map(m => m.NatureAndSports).ConvertUsing(CommaSeparatedStringToArray("innslag_type_natur_og_idrett"));  // Currently empty in dataset source
            Map(m => m.ArtsAndCrafts).ConvertUsing(CommaSeparatedStringToArray("innslag_type_kunst_og_håndverk"));
            Map(m => m.Sports).ConvertUsing(CommaSeparatedStringToArray("innslag_type_idrett_og_ fysisk_aktivitet"));
            Map(m => m.EnthusiastThemes).ConvertUsing(CommaSeparatedStringToArray("Innslag_tema_samlere_entusiaster_og_oppfinnere"));
            //Map(m => m.Science).ConvertUsing(CommaSeparatedStringToArray("innslag_tema_vitenskap"));    // Currently empty in dataset source
            Map(m => m.Relationships).ConvertUsing(CommaSeparatedStringToArray("medvirkende_relasjoner"));
            Map(m => m.Musicians).ConvertUsing(CommaSeparatedStringToArray("innslag_type_musikere"));
            Map(m => m.Foods).ConvertUsing(CommaSeparatedStringToArray("innslag_type_mat"));
            Map(m => m.Animals).ConvertUsing(CommaSeparatedStringToArray("innslag_type_dyr"));
            Map(m => m.NatureThemes).ConvertUsing(CommaSeparatedStringToArray("innslag_tema_natur_og_friluftsliv"));
            Map(m => m.CommericalThemes).ConvertUsing(CommaSeparatedStringToArray("innslag_tema_kjop_og_salg"));
            Map(m => m.FarmingAndFishing).ConvertUsing(CommaSeparatedStringToArray("innslag_type_landbruk_og_fiske"));
            Map(m => m.PublicServices).ConvertUsing(CommaSeparatedStringToArray("Innslag_type_offentlige_tjenester_og veldedighet"));
            Map(m => m.PoliticalThemes).ConvertUsing(CommaSeparatedStringToArray("tema_politikk_og_media"));

            Map(m => m.NumberOfMen).Name("medvirkende_antall_menn").Default(0);
            Map(m => m.NumberOfWomen).Name("medvirkende_antall_kvinner").Default(0);
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