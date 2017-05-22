var azureSearchQueryApiKey = "3CFB8CC288A0831949695ED96AD0AA61";
var searchResults = [];
var dateOptions = { year: 'numeric', month: 'long', day: 'numeric' };

var app = new Vue({
    el: '#searchapp',
    data: {
        results: searchResults,
        clothesFacets: [],
        activeClothesFacet: '',
        inSearch: false
    },
    methods: {
        facetClick: function (clickedFacet) {
            this.activeClothesFacet = clickedFacet;
            this.execSearch();
        },
        execSearch: function () {
            if (this.isSearching) return;

            var self = this;
            self.isSearching = true;
            var q = $("#query").val();

            var searchQuery = encodeURIComponent(q);
            if (q.length = 0)
                searchQuery = '*';
            if (searchQuery.length == 0)
                searchQuery = '*';

            if (self.activeClothesFacet)
                searchQuery += '&$filter=clothes/any(t: t eq \'' + encodeURIComponent(self.activeClothesFacet) + '\')';

            var searchAPI = "https://norgerundt.search.windows.net/indexes/norgerundt/docs?$top=20&api-version=2016-09-01&facet=clothes&search=" + searchQuery;

            $.ajax({
                url: searchAPI,
                beforeSend: function (request) {
                    request.setRequestHeader("api-key", azureSearchQueryApiKey);
                    request.setRequestHeader("Content-Type", "application/json");
                    request.setRequestHeader("Accept", "application/json; odata.metadata=none");
                },
                type: "GET",
                success: function (data) {
                    searchResults.splice(0, searchResults.length);
                    for (var item in data.value) {
                        searchResults.push({
                            title: data.value[item].title,
                            url: data.value[item].url,
                            date: (new Date(data.value[item].date)).toLocaleDateString('nb-NO', dateOptions),
                            munic: data.value[item].municipality,
                            theme: data.value[item].mainTheme,
                            clothes: data.value[item].clothes
                        });
                    }

                    // Add Clothes facets
                    self.clothesFacets.splice(0, self.clothesFacets.length);
                    for (var item in data["@search.facets"].clothes) {
                        if (self.activeClothesFacet != data["@search.facets"].clothes[item].value) {
                            self.clothesFacets.push({
                                title: data["@search.facets"].clothes[item].value,
                                count: data["@search.facets"].clothes[item].count
                            });
                        }
                    }
                }
            }).done(function (data) {
                self.isSearching = false;
                // Check if the user changed the search term since this completed
                if (q != $("#query").val())
                    self.execSearch();
            });
        }
    }
});

app.execSearch();