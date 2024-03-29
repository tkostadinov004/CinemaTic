﻿const randomNum = () => Math.floor(Math.random() * (235 - 52 + 1) + 52);
const randomRGB = () => `rgb(${randomNum()}, ${randomNum()}, ${randomNum()})`;
var usersPerMonth; var usersGrowth;
function getRegisteredUsersByMonth() {
    $.ajax({
        type: "GET",
        url: `/Admin/Charts/GetRegisteredUsersByMonth`,
        data: {},
        success: function (response) {
            usersPerMonth = new Chart(document.getElementById('registeredByMonth'), {
                type: 'line',
                data: {
                    labels: response['labels'],
                    datasets: [{
                        label: 'Users',
                        fill: false,
                        borderColor: 'rgb(75, 192, 192)',
                        pointBackgroundColor: 'rgba(220, 220, 220, 1)',
                        pointBorderColor: '#fff',
                        color: '#ff0000',
                        data: response['usersCounts']
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: {
                            position: 'top'
                        },
                        title: {
                            display: true,
                            text: `Registered users per month`
                        }
                    },
                    scales: {
                        y: {
                            ticks: {
                                stepSize: 1,
                            },
                            grid: {
                                borderColor: '#fff'
                            }
                        }
                    }
                },
            });
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}
function getUsersGrowth() {
    $.ajax({
        type: "GET",
        url: `/Admin/Charts/GetUsersGrowth`,
        data: {},
        success: function (response) {
            usersGrowth = new Chart(document.getElementById('usersGrowth'), {
                type: 'line',
                data: {
                    labels: response['labels'],
                    datasets: [{
                        label: 'Users',
                        fill: false,
                        borderColor: 'rgb(206, 209, 0)',
                        pointBackgroundColor: 'rgba(220, 220, 220, 1)',
                        pointBorderColor: '#fff',
                        color: '#ff0000',
                        data: response['usersCounts']
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: {
                            position: 'top'
                        },
                        title: {
                            display: true,
                            text: `Total users count`
                        }
                    },
                    scales: {
                        y: {
                            ticks: {
                                stepSize: 1
                            }
                        }
                    }
                },
            });
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}
function updateCharts() {
    usersPerMonth.update();
    usersGrowth.update();
}
function lighten() {
    usersPerMonth.options.scales.x.ticks.color = '#000';
    usersPerMonth.options.scales.y.ticks.color = '#000';

    usersGrowth.options.scales.x.ticks.color = '#000';
    usersGrowth.options.scales.y.ticks.color = '#000';
}
function darken() {
    usersPerMonth.options.scales.x.ticks.color = '#fff';
    usersPerMonth.options.scales.y.ticks.color = '#fff';

    usersGrowth.options.scales.x.ticks.color = '#fff';
    usersGrowth.options.scales.y.ticks.color = '#fff';
}