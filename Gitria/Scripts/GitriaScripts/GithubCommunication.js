﻿function CheckForUpdates()
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
    debugger;
    for (var i = 0; i < response.length; i++)
    {
        $("#commit-panel").prepend('<div id="new-commit" style="display:none">'
            + '<a href="' + response[i].html_url + '" class="list-group-item">'
            + '<table class="table-condensed" border="0">'
            + '<tr>'
            + '<td class="img-size-7">'
            + '<img src="' + response[i].committer.avatar_url + '" class="img-circle img-scale" alt="' + response[i].committer.login + '" />'
            + '</td>'
            + '<td valign="top">'
            + '<table border="0">'
            + '<tr>'
            + '<td>'
            + '<h3>'
            + '<i class="fa fa-code fa-fw"></i>'
            + 'New Commit to ' + response[i].repo_name
            + '</h3>'
            + '</td>'
            + '</tr>'
            + '<tr>'
            + '<td>'
            + '<span>' + response[i].commit.message + '</span>'
            + '</td>'
            + '</tr>'
            + '</table>'
            + '</td>'
            + '<td valign="top">'
            + '<span class="pull-right text-muted small">'
            + '<em>' + response[i].time_ago + '</em>'
            + '</span>'
            + '</td>'
            + '</tr>'
            + '</table>'
            + '</a>'
            + '</div>');

        $('#new-commit')
        .slideDown(3000);
    }
}