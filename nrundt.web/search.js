var inSearch = false;
var azureSearchQueryApiKey = "3CFB8CC288A0831949695ED96AD0AA61";
var selectedClothesFacet = '';
var searchResults = [];
var dateOptions = { year: 'numeric', month: 'long', day: 'numeric' };
var clothesFacetsOutside = [];

var app = new Vue({
    el: '#searchapp',
    data: {
        results: searchResults,
        clothesFacets: clothesFacetsOutside,
        activeClothesFacet: ''
    },
    methods: {
        facetClick: function (clickedFacet) {
            this.activeClothesFacet = clickedFacet;
            selectedClothesFacet = clickedFacet;
            execSearch();
        }
    }
});

function execSearch() {
    if (inSearch) return;

    inSearch = true;
    var q = $("#query").val();

    var searchQuery = encodeURIComponent(q);
    if (q.length = 0)
        searchQuery = '*';
    if (searchQuery.length == 0)
        searchQuery = '*';

    if (selectedClothesFacet.length > 0)
        searchQuery += '&$filter=clothes/any(t: t eq \'' + encodeURIComponent(selectedClothesFacet) + '\')';

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
            clothesFacetsOutside.splice(0, clothesFacetsOutside.length);
            for (var item in data["@search.facets"].clothes) {
                if (selectedClothesFacet != data["@search.facets"].clothes[item].value) {
                    clothesFacetsOutside.push({
                        title: data["@search.facets"].clothes[item].value,
                        count: data["@search.facets"].clothes[item].count
                    });
                }
            }
        }
    }).done(function (data) {
        inSearch = false;
        // Check if the user changed the search term since this completed
        if (q != $("#query").val())
            execSearch();
    });
}

execSearch();