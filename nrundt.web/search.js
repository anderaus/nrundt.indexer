var inSearch = false;
var azureSearchQueryApiKey = "3CFB8CC288A0831949695ED96AD0AA61";

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

    var searchAPI = "https://norgerundt.search.windows.net/indexes/norgerundt/docs?$top=10&api-version=2016-09-01&search=" + searchQuery;
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
            $("#subjectsContainer").html('');
            $("#contributorsContainer").html('');

            for (var item in data.value) {

                var title = data.value[item].title;
                var url = data.value[item].url;

                var divContent = '<h2><a href="' + url + '">' + title + '</a></h2>';

                $("#mediaContainer").append(divContent);
            }
        }
    }).done(function (data) {
        inSearch = false;
        // Check if the user changed the search term since this completed
        if (q != $("#query").val())
            execSearch();
    });
}