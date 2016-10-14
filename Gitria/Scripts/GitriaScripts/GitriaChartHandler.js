function GenerateChartForId(repository)
{
    var deletionsElement = document.getElementById(repository + "Deletions").getContext('2d');

    var myChart1 = new Chart(deletionsElement,
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

    var additionsElement = document.getElementById(repository + "Additions").getContext('2d');

    var myChart2 = new Chart(additionsElement,
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
}