var azureSearchQueryApiKey = "3CFB8CC288A0831949695ED96AD0AA61";
var dateOptions = { year: 'numeric', month: 'long', day: 'numeric' };

var app = new Vue({
    el: '#searchapp',
    data: {
        results: [],
        clothesFacets: [],
        activeClothesFacet: '',
        tagsFacets: [],
        activeTagsFacet: '',
        searchString: '',
        isSearching: false
    },
    methods: {
        clothesFacetClick: function (clickedFacet) {
            this.activeClothesFacet = clickedFacet;
            this.execSearch();
        },
        tagsFacetClick: function (clickedFacet) {
            this.activeTagsFacet = clickedFacet;
            this.execSearch();
        },        
        execSearch: function () {
            if (this.isSearching) return;

            var self = this;
            self.isSearching = true;

            var textQuery = self.searchString || '*';
            var searchQuery = textQuery;

            // TODO: These won't work. Need a way to add multiple filters without reusing the $filter param (which is not allowed)
            var filter = '';
            if (self.activeClothesFacet) {
                filter += 'clothes/any(t: t eq \'' + encodeURIComponent(self.activeClothesFacet) + '\')';
            }
            if (self.activeTagsFacet) {
                if (filter) filter += ' and ';
                filter += 'tags/any(t: t eq \'' + encodeURIComponent(self.activeTagsFacet) + '\')';
            }
            if (filter) {
                searchQuery += '&$filter=' + filter;
            }

            console.log(searchQuery);

            var searchAPI = "https://norgerundt.search.windows.net/indexes/norgerundt/docs?$top=10&api-version=2016-09-01&facet=clothes&facet=tags&search=" + searchQuery;

            $.ajax({
                url: searchAPI,
                beforeSend: function (request) {
                    request.setRequestHeader("api-key", azureSearchQueryApiKey);
                    request.setRequestHeader("Content-Type", "application/json");
                    request.setRequestHeader("Accept", "application/json; odata.metadata=none");
                },
                type: "GET",
                success: function (data) {
                    self.results.splice(0, self.results.length);
                    for (var item in data.value) {
                        self.results.push({
                            title: data.value[item].title,
                            url: data.value[item].url,
                            date: (new Date(data.value[item].date)).toLocaleDateString('nb-NO', dateOptions),
                            munic: data.value[item].municipality,
                            theme: data.value[item].mainTheme,
                            clothes: data.value[item].clothes,
                            tags: data.value[item].tags
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

                    // Add Tags facets
                    self.tagsFacets.splice(0, self.tagsFacets.length);
                    for (var item in data["@search.facets"].tags) {
                        if (self.activeTagsFacet != data["@search.facets"].tags[item].value) {
                            self.tagsFacets.push({
                                title: data["@search.facets"].tags[item].value,
                                count: data["@search.facets"].tags[item].count
                            });
                        }
                    }
                }
            }).done(function (data) {
                self.isSearching = false;
                // Check if the user changed the search term since this completed
                if (self.searchString && textQuery !== self.searchString)
                    self.execSearch();
            });
        }
    }
});

app.execSearch();