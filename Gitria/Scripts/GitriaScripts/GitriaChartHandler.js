function GenerateChartForId(repository, weeklyData, sixMonthData)
{
    var deletionsElement = document.getElementById(repository + "Deletions").getContext('2d');

    var totalDeletions = weeklyData[0].Deletions + weeklyData[1].Deletions;

    var myChart1 = new Chart(deletionsElement,
    {
        type: 'doughnut',
        data:
        {
            labels: ["Deletions"],
            datasets: [
            {
                backgroundColor: ["#F7464A", "#4C0000", "#FFFFFF"],
                data: [(weeklyData[0].Deletions / totalDeletions) * 75, (weeklyData[1].Deletions / totalDeletions) * 75, 25]
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

    var totalAdditions = weeklyData[0].Additions + weeklyData[1].Additions;

    var myChart2 = new Chart(additionsElement,
    {
        type: 'doughnut',
        data:
        {
            labels: ["Additions"],
            datasets: [
            {
                backgroundColor: ["#32CD32", "#004C00", "#FFFFFF"],
                data: [(weeklyData[0].Additions / totalAdditions) * 75, (weeklyData[1].Additions / totalAdditions) * 75, 25]
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

    var commitsElement = document.getElementById(repository + "CommitStats").getContext('2d');
    var monthNames = [ "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" ];
    
    var myChart3 = new Chart(commitsElement,
    {
        type: 'bar',
        data:
        {
            labels: ["Red", "Blue", "Yellow", "Green", "Purple", sixMonthData[0].Month],
            datasets: [{
                label: '# of Votes',
                data: [12, 19, 3, 5, 2, 3],
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                borderColor: [
                    'rgba(255,99,132,1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                ],
                borderWidth: 1
            }]
        },
        options:
        {
            responsive: true,
            maintainAspectRatio: false,
            legend:
            {
                display: false
            }
        }
    });
}