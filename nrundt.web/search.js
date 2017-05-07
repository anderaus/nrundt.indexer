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
            // $("#subjectsContainer").html('');
            // $("#contributorsContainer").html('');

            for (var item in data.value) {

                var title = data.value[item].title;
                var url = data.value[item].url;
                var date = new Date(data.value[item].date);
                var munic = data.value[item].municipality;
                var theme = data.value[item].mainTheme;

                var dateOptions = { year: 'numeric', month: 'long', day: 'numeric' };

                var divContent = '<h4><a href="' + url + '" target="_blank">' + title + '</a></h4>';
                divContent += '<p>' + theme + '</p>';
                divContent += '<span class="date">' + date.toLocaleDateString('nb-NO', dateOptions) + '</span> - <strong>' + munic + '</strong>';

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

execSearch();