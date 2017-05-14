using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace nrundt.indexer
{
    [SerializePropertyNamesAsCamelCase]
    public class SequenceIndexItem
    {
        [Key]
        public string Id { get; set; }

        public string Url { get; set; }

        [IsFilterable, IsSortable]
        public DateTime Date { get; set; }

        [IsSearchable]
        [Analyzer(AnalyzerName.AsString.NbMicrosoft)]
        public string Title { get; set; }

        [IsSearchable, IsFilterable, IsFacetable]
        public string Municipality { get; set; }

        [IsSearchable, IsFilterable, IsFacetable]
        [Analyzer(AnalyzerName.AsString.NbMicrosoft)]
        public string MainTheme { get; set; }

        [IsSearchable, IsFilterable, IsFacetable]
        [Analyzer(AnalyzerName.AsString.NbMicrosoft)]
        public string[] Special { get; set; }

        [IsSearchable, IsFilterable, IsFacetable]
        [Analyzer(AnalyzerName.AsString.NbMicrosoft)]
        public string[] Clothes { get; set; }

        [IsSearchable, IsFilterable, IsFacetable]
        [Analyzer(AnalyzerName.AsString.NbMicrosoft)]
        public string[] Construction { get; set; }

        [IsSearchable, IsFilterable, IsFacetable]
        [Analyzer(AnalyzerName.AsString.NbMicrosoft)]
        public string[] Historical { get; set; }

        [IsSearchable, IsFilterable, IsFacetable]
        [Analyzer(AnalyzerName.AsString.NbMicrosoft)]
        public string[] ArtsAndCrafts { get; set; }

        [IsSearchable, IsFilterable, IsFacetable]
        [Analyzer(AnalyzerName.AsString.NbMicrosoft)]
        public string[] Sports { get; set; }

        [IsSearchable, IsFilterable, IsFacetable]
        [Analyzer(AnalyzerName.AsString.NbMicrosoft)]
        public string[] EnthusiastThemes { get; set; }

        [IsSearchable, IsFilterable, IsFacetable]
        [Analyzer(AnalyzerName.AsString.NbMicrosoft)]
        public string[] Relationships { get; set; }

        [IsSearchable, IsFilterable, IsFacetable]
        [Analyzer(AnalyzerName.AsString.NbMicrosoft)]
        public string[] Musicians { get; set; }

        [IsSearchable, IsFilterable, IsFacetable]
        [Analyzer(AnalyzerName.AsString.NbMicrosoft)]
        public string[] Foods { get; set; }

        [IsSearchable, IsFilterable, IsFacetable]
        [Analyzer(AnalyzerName.AsString.NbMicrosoft)]
        public string[] Animals { get; set; }

        [IsSearchable, IsFilterable, IsFacetable]
        [Analyzer(AnalyzerName.AsString.NbMicrosoft)]
        public string[] NatureThemes { get; set; }

        [IsSearchable, IsFilterable, IsFacetable]
        [Analyzer(AnalyzerName.AsString.NbMicrosoft)]
        public string[] CommericalThemes { get; set; }

        [IsSearchable, IsFilterable, IsFacetable]
        [Analyzer(AnalyzerName.AsString.NbMicrosoft)]
        public string[] FarmingAndFishing { get; set; }

        [IsSearchable, IsFilterable, IsFacetable]
        [Analyzer(AnalyzerName.AsString.NbMicrosoft)]
        public string[] PublicServices { get; set; }

        [IsSearchable, IsFilterable, IsFacetable]
        [Analyzer(AnalyzerName.AsString.NbMicrosoft)]
        public string[] PoliticalThemes { get; set; }


        [IsFilterable, IsFacetable]
        public int NumberOfMen { get; set; }

        [IsFilterable, IsFacetable]
        public int NumberOfWomen { get; set; }
    }
}