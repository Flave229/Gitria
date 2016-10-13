function CheckForUpdates()
{
    var lastUpdate = document.getElementById("lastUpdate");
    var mvcAction = document.getElementById("apiController");
    var response = $.ajax({
        type: "GET", url: mvcAction.value + "?lastUpdated=" + lastUpdate.value, success: function (response)
        {
            var json = JSON.parse(response);

            if (!jQuery.isEmptyObject(json))
            {
                UpdatePageWithNewCommits(json);
            }
        }
    });
}

function UpdatePageWithNewCommits(response)
{
    document.getElementById('lastUpdate').value = response.NewCommits[0].Date.replace(/:/g, '-').replace('T', '-').replace('Z', '');

    var weekCountString = document.getElementById("weekCount").innerHTML;
    var monthCountString = document.getElementById("monthCount").innerHTML;

    var weekCount = parseInt(weekCountString) + response.Count;
    var monthCount = parseInt(monthCountString) + response.Count;

    document.getElementById("weekCount").innerHTML = weekCount;
    document.getElementById("monthCount").innerHTML = monthCount;

    for (var i = 0; i < response.NewCommits.length; i++)
    {
        $("#commit-panel").prepend('<div id="new-commit" style="display:none">'
            + '<a id="new-commit-panel" href="' + response.NewCommits[i].Url + '" class="list-group-item" style="background-color: #00FF00">'
            + '<table class="table-condensed" border="0">'
            + '<tr>'
            + '<td>'
            + '<table>'
            + '<tr>'
            + '<td class="img-size-20">'
            + '<img src="' + response.NewCommits[i].Author.AvatarLink + '" class="img-circle img-scale" alt="' + response.NewCommits[i].Author.Name + '" />'
            + '</td>'
            + '<td>'
            + '<h3>'
            + '<i class="fa fa-code fa-fw"></i>'
            + ' ' + response.NewCommits[i].Author.Name + ' committed to ' + response.NewCommits[i].RepositoryName
            + '</h3>'
            + '</td>'
            + '</tr>'
            + '</table>'
            + '</td>'
            + '</tr>'
            + '<tr>'
            + '<td valign="top">'
            + '<table border="0">'
            + '<tr>'
            + '<td>'
            + '<span class="text-muted small">' + response.NewCommits[i].TimeAgo + '</span>'
            + '</td>'
            + '</tr>'
            + '<tr>'
            + '<td>'
            + '<span>' + response.NewCommits[i].Message + '</span>'
            + '</td>'
            + '</tr>'
            + '</table>'
            + '</td>'
            + '</tr>'
            + '</table>'
            + '</a>'
            + '</div>');

        $('#new-commit')
        .slideDown(3000);

        $('#new-commit-panel')
        .animate(
        {
            backgroundColor: "#FFF"
        }, 20000);
    }
}