function getTicketsBought() {
    $.ajax({
        type: "GET",
        url: `/Customer/Charts/GetTicketsBought`,
        data: {},
        success: function (response) {
            usersGrowth = new Chart(document.getElementById('ticketsBought'), {
                type: 'line',
                data: {
                    labels: response['labels'],
                    datasets: [{
                        label: 'Tickets',
                        fill: false,
                        borderColor: '#30b6c1',
                        pointBackgroundColor: 'rgba(220, 220, 220, 1)',
                        pointBorderColor: '#fff',
                        color: '#ff0000',
                        data: response['ticketsCounts']
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
                            text: `Total tickets bought`
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