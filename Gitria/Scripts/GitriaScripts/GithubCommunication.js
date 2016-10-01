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
    document.getElementById('lastUpdate').value = response[0].commit.author.date.replace(/:/g, '-').replace('T', '-').replace('Z', '');
    $("#commit-panel").prepend('<a href="' + response[0].html_url + '" class="list-group-item">'
        + '<table class="table-condensed" border="0">' 
        + '<tr>'
        + '<td class="img-size-7">'
        + '<img src="' + response[0].committer.avatar_url + '" class="img-circle img-scale" alt="' + response[0].committer.login + '" />'
        + '</td>'
        + '<td valign="top">'
        + '<table border="0">'
        + '<tr>'
        + '<td>'
        + '<h3>'
        + '<i class="fa fa-code fa-fw"></i>'
        + 'New Commit to ' + response[0].repo_name
        + '</h3>'
        + '</td>'
        + '</tr>'
        + '<tr>'
        + '<td>'
        + '<span>' + response[0].commit.message + '</span>'
        + '</td>'
        + '</tr>'
        + '</table>'
        + '</td>'
        + '<td valign="top">'
        + '<span class="pull-right text-muted small">'
        + '<em>' + response[0].time_ago + '</em>'
        + '</span>'
        + '</td>'
        + '</tr>'
        + '</table>'
        + '</a>')
}