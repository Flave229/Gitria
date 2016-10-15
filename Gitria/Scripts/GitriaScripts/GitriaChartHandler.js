function GenerateChartForId(repository, data)
{
    var deletionsElement = document.getElementById(repository + "Deletions").getContext('2d');

    var totalDeletions = data[0].Deletions + data[1].Deletions;

    var myChart1 = new Chart(deletionsElement,
    {
        type: 'doughnut',
        data:
        {
            labels: ["Deletions"],
            datasets: [
            {
                backgroundColor: ["#F7464A", "#4C0000", "#FFFFFF"],
                data: [(data[0].Deletions / totalDeletions) * 75, (data[1].Deletions / totalDeletions) * 75, 25]
            }]
        },
        options:
        {
            tooltips:
            {
                enabled: false
            },
            legend:
            {
                display: false
            },
            cutoutPercentage: 80
        }
    });

    var additionsElement = document.getElementById(repository + "Additions").getContext('2d');

    var totalAdditions = data[0].Additions + data[1].Additions;

    var myChart2 = new Chart(additionsElement,
    {
        type: 'doughnut',
        data:
        {
            labels: ["Additions"],
            datasets: [
            {
                backgroundColor: ["#32CD32", "#004C00", "#FFFFFF"],
                data: [(data[0].Additions / totalAdditions) * 75, (data[1].Additions / totalAdditions) * 75, 25]
            }]
        },
        options:
        {
            tooltips:
            {
                enabled: false
            },
            legend:
            {
                display: false
            },
            cutoutPercentage: 80
        }
    });
}