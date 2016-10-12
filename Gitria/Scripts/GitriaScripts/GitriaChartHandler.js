var ctx1 = document.getElementById("appDeletions").getContext('2d');

var myChart1 = new Chart(ctx1,
{
    type: 'doughnut',
    data:
    {
        labels: ["Deletions"],
        datasets: [
        {
            backgroundColor: ["#F7464A", "#FFFFFF"],
            data: [20, 80]
        }]
    },
    options:
    {
        legend:
        {
            display: false
        },
        cutoutPercentage: 80
    }
});

var ctx2 = document.getElementById("appAdditions").getContext('2d');

var myChart2 = new Chart(ctx2,
{
    type: 'doughnut',
    data:
    {
        labels: ["Additions"],
        datasets: [
        {
            backgroundColor: ["#32CD32", "#FFFFFF"],
            data: [45, 55]
        }]
    },
    options:
    {
        legend:
        {
            display: false
        },
        cutoutPercentage: 80
    }
});