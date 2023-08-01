const randomNum = () => Math.floor(Math.random() * (235 - 52 + 1) + 52);
const randomRGB = () => `rgb(${randomNum()}, ${randomNum()}, ${randomNum()})`;
var marketShare; var totalIncomes; var customersPerCinema; var bestSellingMoviesPerCinema;

function getData(data) {
    var arr = [];
    for (var key in data) {
        arr.push(data[key]);
    }
    console.log(arr);
    return arr;
}
function getMarketShare() {
    $.ajax({
        type: "GET",
        url: `/Charts/GetMarketShare`,
        data: { },
        success: function (response) {
            marketShare = new Chart(document.getElementById('marketShare'), {
                type: 'doughnut',
                data: {
                    labels: ['Your income', 'Total income'],
                    datasets: [{
                        label: 'Income (in dollars)',
                        backgroundColor: ['#edd145', '#6ca893'],
                        borderColor: 'rgba(220, 220, 220, 1)',
                        pointBackgroundColor: 'rgba(220, 220, 220, 1)',
                        pointBorderColor: '#fff',
                        color: '#ff0000',
                        data: [response['personalIncome'], response['totalIncome'] - response['personalIncome']]
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
                            text: `Your cinemas' market share`
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
function getTotalIncomes() {
    $.ajax({
        type: "GET",
        url: `/Charts/GetTotalIncomes`,
        data: {},
        success: function (response) {
            let colors = [];
            let datasets = [];
            for (var i = 0; i < response['labels'].length; i++) {
                colors.push(randomRGB());

                datasets.push({
                    label: response['labels'][i],
                    backgroundColor: colors[i],
                    borderColor: 'rgba(220, 220, 220, 1)',
                    pointBackgroundColor: 'rgba(220, 220, 220, 1)',
                    pointBorderColor: '#fff',
                    color: '#ff0000',
                    data: { 'Income (in dollars)': response['incomes'][i] }
                });
            }
            console.log(datasets);
            totalIncomes = new Chart(document.getElementById('totalRevenuePerCinema'), {
                type: 'bar',
                data: {
                    datasets: datasets
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: {
                            position: 'top'
                        },
                        title: {
                            display: true,
                            text: `Total income per cinema`
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
function getCustomersPerCinema() {
    $.ajax({
        type: "GET",
        url: `/Charts/GetCustomersPerCinema`,
        data: {},
        success: function (response) {
            let colors = [];
            let datasets = [];
            for (var i = 0; i < response['labels'].length; i++) {
                colors.push(randomRGB());

                datasets.push({
                    label: response['labels'][i],
                    backgroundColor: colors[i],
                    borderColor: 'rgba(220, 220, 220, 1)',
                    pointBackgroundColor: 'rgba(220, 220, 220, 1)',
                    pointBorderColor: '#fff',
                    color: '#ff0000',
                    data: { 'Customers count': response['customersCounts'][i] }
                });
            }
            customersPerCinema = new Chart(document.getElementById('customersPerCinema'), {
                type: 'bar',
                data: {
                    datasets: datasets
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: {
                            position: 'top'
                        },
                        title: {
                            display: true,
                            text: `Total customers per cinema`
                        }
                    },
                    scales: {
                        y: {
                            ticks: {
                                
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
function getBestSellingMoviesPerCinema() {
    $.ajax({
        type: "GET",
        url: `/Charts/GetBestSellingMoviesPerCinema`,
        data: {},
        success: function (response) {
            let colors = [];
            for (var i = 0; i < response['labels'].length; i++) {
                colors.push(randomRGB());
            }
            bestSellingMoviesPerCinema = new Chart(document.getElementById('bestSellingMoviesPerCinema'), {
                type: 'doughnut',
                data: {
                    labels: response['labels'],
                    datasets: [{
                        label: 'Cinemas',
                        backgroundColor: colors,
                        borderColor: 'rgba(220, 220, 220, 1)',
                        pointBackgroundColor: 'rgba(220, 220, 220, 1)',
                        pointBorderColor: '#fff',
                        color: '#ff0000',
                        data: response['moviesCounts']
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
                            text: `Most popular movie per cinema`
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
function lighten() {
    customersPerCinema.options.scales.x.ticks.color = '#000';
    customersPerCinema.options.scales.y.ticks.color = '#000';

    totalIncomes.options.scales.x.ticks.color = '#000';
    totalIncomes.options.scales.y.ticks.color = '#000';
}
function darken() {
    customersPerCinema.options.scales.x.ticks.color = '#fff';
    customersPerCinema.options.scales.y.ticks.color = '#fff';

    totalIncomes.options.scales.x.ticks.color = '#fff';
    totalIncomes.options.scales.y.ticks.color = '#fff';
}
function updateCharts() {
    marketShare.update();
    totalIncomes.update();
    customersPerCinema.update();
    bestSellingMoviesPerCinema.update();
}