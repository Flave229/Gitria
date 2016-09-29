function CheckForUpdates()
{
    var mvcAction = document.getElementById("apiController");
    var lastUpdated = document.getElementById("lastUpdated");
    $.ajax({ type: "POST", url: mvcAction.value + '/' + lastUpdated.value, success: function () { } });
}