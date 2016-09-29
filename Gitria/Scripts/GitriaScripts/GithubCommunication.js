function CheckForUpdates()
{
    var mvcAction = document.getElementById("apiController");
    $.ajax({ type: "GET", url: mvcAction.value, success: function () { } });
}