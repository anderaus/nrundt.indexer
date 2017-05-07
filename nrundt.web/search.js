var inSearch = false;
var azureSearchQueryApiKey = "3CFB8CC288A0831949695ED96AD0AA61";
var selectedClothesFacet = '';

function execSearch() {
    if (inSearch) return;

    inSearch = true;
    var q = $("#query").val();
    console.log('search query: ' + q);

    var searchQuery = encodeURIComponent(q);
    if (q.length = 0)
        searchQuery = '*';
    if (searchQuery.length == 0)
        searchQuery = '*';

    if (selectedClothesFacet.length > 0)
        searchQuery += '&$filter=clothes/any(t: t eq \'' + encodeURIComponent(selectedClothesFacet) + '\')';

    var searchAPI = "https://norgerundt.search.windows.net/indexes/norgerundt/docs?$top=10&api-version=2016-09-01&facet=clothes&search=" + searchQuery;
    console.log('search url: ' + searchAPI);

    $.ajax({
        url: searchAPI,
        beforeSend: function (request) {
            request.setRequestHeader("api-key", azureSearchQueryApiKey);
            request.setRequestHeader("Content-Type", "application/json");
            request.setRequestHeader("Accept", "application/json; odata.metadata=none");
        },
        type: "GET",
        success: function (data) {
            console.log('success happened!');

            $("#mediaContainer").html('');
            $("#clothesFacetsContainer").html('');

            for (var item in data.value) {

                var title = data.value[item].title;
                var url = data.value[item].url;
                var date = new Date(data.value[item].date);
                var munic = data.value[item].municipality;
                var theme = data.value[item].mainTheme;

                var dateOptions = { year: 'numeric', month: 'long', day: 'numeric' };

                var divContent = '<div class="resultcontainer"><h4><a href="' + url + '" target="_blank">' + title + '</a></h4>';
                divContent += '<span>' + theme + '</span><br>';
                divContent += '<span class="date">' + date.toLocaleDateString('nb-NO', dateOptions) + '</span> - <strong>' + munic + '</strong>';

                var clothes = data.value[item].clothes;
                divContent += '<br><div class="facetscontainer">';
                for (var clothesItem in clothes) {
                    divContent += '<a href="javascript:void(0);" onclick="setClothesFacet(\'' + clothes[clothesItem] + '\');"><span class="label label-default">' + clothes[clothesItem] + '</span> </a>';
                }

                divContent += "</div></div>";
                $("#mediaContainer").append(divContent);
            }

            // Add Clothes facets
            var selectedClothesFacet = '';
            for (var item in data["@search.facets"].clothes) {
                if (selectedClothesFacet != data["@search.facets"].clothes[item].value) {
                    $("#clothesFacetsContainer").append('<li><a href="javascript:void(0);" onclick="setClothesFacet(\'' + data["@search.facets"].clothes[item].value + '\');">' + data["@search.facets"].clothes[item].value + ' (' + data["@search.facets"].clothes[item].count + ')</a></li>');
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

function setClothesFacet(facet) {
    // User clicked on a subject facet
    selectedClothesFacet = facet;
    if (facet != '')
        $("#currentClothesFacet").html(facet + '<a href="javascript:void(0);" onclick="setClothesFacet(\'\');"> [X]</a>');
    else
        $("#currentClothesFacet").html('');
    execSearch();
}

execSearch();