function CheckForUpdates()
{
    var lastUpdated = document.getElementById("lastUpdated");
    $.ajax({ type: "POST", url: 'CheckForUpdates/' + lastUpdated.value, success: function () { } });
}