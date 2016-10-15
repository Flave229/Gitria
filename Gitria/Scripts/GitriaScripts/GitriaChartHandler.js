function GenerateChartForId(repository, data)
{
    var deletionsElement = document.getElementById(repository + "Deletions").getContext('2d');
    debugger;
    var myChart1 = new Chart(deletionsElement,
    {
        type: 'doughnut',
        data:
        {
            labels: ["Deletions"],
            datasets: [
            {
                backgroundColor: ["#F7464A", "#FFFFFF"],
                data: [data[0].Deletions, data[1].Deletions]
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
                data: [data[0].Additions, data[1].Additions]
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