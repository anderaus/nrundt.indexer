using System;

namespace nrundt.indexer
{
    public class Sequence
    {
        public DateTime Date { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Municipality { get; set; }
        public string MainTheme { get; set; }
        public string[] Special { get; set; }
        public string[] Clothes { get; set; }
        //public string[] Experience { get; set; }  // Removed due to lack of value/relevance
        public string[] Construction { get; set; }
        public string[] Historical { get; set; }
        //public string[] NatureAndSports { get; set; } // Currently empty in dataset source
        public string[] ArtsAndCrafts { get; set; }
        public string[] Sports { get; set; }
        public string[] EnthusiastThemes { get; set; }
        //public string[] Science { get; set; }   // Currently empty in dataset source
        public string[] Relationships { get; set; }
        public string[] Musicians { get; set; }
        public string[] Foods { get; set; }
        public string[] Animals { get; set; }
        public string[] NatureThemes { get; set; }
        public string[] CommericalThemes { get; set; }
        public string[] FarmingAndFishing { get; set; }
        public string[] PublicServices { get; set; }
        public string[] PoliticalThemes { get; set; }

        public int NumberOfMen { get; set; }
        public int NumberOfWomen { get; set; }
    }
}

////Un-mapped fields to do:
//medvirkende_hovedperson1_kjonn
//medvirkende_hovedperson2_kjonn
//medvirkende_hovedperson3_kjonn
//medvirkende_hovedperson1_alder
//medvirkende_hovedperson2_alder
//medvirkende_hovedperson3_alder