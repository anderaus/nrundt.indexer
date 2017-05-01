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
    }
}