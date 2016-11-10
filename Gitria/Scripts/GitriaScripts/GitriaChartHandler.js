function GenerateChartForId(repository, weeklyData, sixMonthData)
{
    var deletionsElement = document.getElementById(repository + "Deletions").getContext('2d');

    var lastWeekDeletions = weeklyData[1].Deletions;
    var thisWeekDeletions = weeklyData[0].Deletions;

    var lastWeekDeletionsPercentage = weeklyData[1].Deletions;
    var thisWeekDeletionsPercentage = weeklyData[0].Deletions;

    if (lastWeekDeletions === 0 && thisWeekDeletions === 0)
    {
        lastWeekDeletionsPercentage = 100;
        thisWeekDeletionsPercentage = 0;
    }
    else if (lastWeekDeletions === 0)
    {
        lastWeekDeletionsPercentage = 0;
        thisWeekDeletionsPercentage = 100;
    }
    else
    {
        thisWeekDeletionsPercentage = (thisWeekDeletions / lastWeekDeletions) * 100;

        if (thisWeekDeletionsPercentage > 100)
            thisWeekDeletionsPercentage = 100;

        lastWeekDeletionsPercentage = 100 - thisWeekDeletionsPercentage;
    }

    var myChart1 = new Chart(deletionsElement,
    {
        type: 'doughnut',
        data:
        {
            labels: ["Deletions"],
            datasets: [
            {
                backgroundColor: ["#F7464A", "#4C0000", "#FFFFFF"],
                data: [thisWeekDeletionsPercentage * 0.75, lastWeekDeletionsPercentage * 0.75, 25]
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

    var lastWeekAdditions = weeklyData[1].Additions;
    var thisWeekAdditions = weeklyData[0].Additions;

    var lastWeekAdditionsPercentage = weeklyData[1].Additions;
    var thisWeekAdditionsPercentage = weeklyData[0].Additions;

    if (lastWeekAdditions === 0 && thisWeekAdditions === 0) {
        lastWeekAdditionsPercentage = 100;
        thisWeekAdditionsPercentage = 0;
    }
    else if (lastWeekAdditions === 0) {
        lastWeekAdditionsPercentage = 0;
        thisWeekAdditionsPercentage = 100;
    }
    else {
        thisWeekAdditionsPercentage = (thisWeekAdditions / lastWeekAdditions) * 100;

        if (thisWeekAdditionsPercentage > 100)
            thisWeekAdditionsPercentage = 100;

        lastWeekDeletionsPercentage = 100 - thisWeekAdditionsPercentage;
    }

    var myChart2 = new Chart(additionsElement,
    {
        type: 'doughnut',
        data:
        {
            labels: ["Additions"],
            datasets: [
            {
                backgroundColor: ["#32CD32", "#004C00", "#FFFFFF"],
                data: [thisWeekAdditionsPercentage * 0.75, lastWeekDeletionsPercentage * 0.75, 25]
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
            labels: [sixMonthData[5].Month, sixMonthData[4].Month, sixMonthData[3].Month, sixMonthData[2].Month, sixMonthData[1].Month, sixMonthData[0].Month],
            datasets: [{
                label: 'Commits',
                data: [sixMonthData[5].CommitCount, sixMonthData[4].CommitCount, sixMonthData[3].CommitCount, sixMonthData[2].CommitCount, sixMonthData[1].CommitCount, sixMonthData[0].CommitCount],
                backgroundColor: [
                    'rgba(255, 99, 132, 0.6)',
                    'rgba(54, 162, 235, 0.6)',
                    'rgba(255, 206, 86, 0.6)',
                    'rgba(75, 192, 192, 0.6)',
                    'rgba(153, 102, 255, 0.6)',
                    'rgba(255, 159, 64, 0.6)'
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
            },
            scales:
            {
                yAxes: [
                {
                    ticks:
                    {
                        suggestedMax: 100,
                        min: 0
                    }
                }]
            }
        }
    });
}