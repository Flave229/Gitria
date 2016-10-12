var ctx1 = document.getElementById("appDeletions").getContext('2d');

var myChart1 = new Chart(ctx1,
{
    type: 'doughnut',
    data:
    {
        labels: ["Deletions"],
        legend: false,
        datasets: [
        {
            backgroundColor: ["#F7464A", "#4D5360"],
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
        legend: false,
        datasets: [
        {
            barThickness: 5,
            backgroundColor: ["#32CD32", "#4D5360"],
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