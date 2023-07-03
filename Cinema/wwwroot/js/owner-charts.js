function getData(data) {
    var arr = [];
    for (var key in data) {
        arr.push(data[key]);
    }
    console.log(arr);
    return arr;
}
const marketShare = new Chart(document.getElementById('marketShare'), {
    type: 'doughnut',
    data: {
        labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
        datasets: [{
            label: 'My First dataset',
            backgroundColor: ['rgb(0, 220, 220)', 'rgb(220, 0, 220)'],
            borderColor: 'rgba(220, 220, 220, 1)',
            pointBackgroundColor: 'rgba(220, 220, 220, 1)',
            pointBorderColor: '#fff',
            color: '#ff0000',
            data: [1,2]
        }]
    },
    options: {
        responsive: true,
        plugins: {
            legend: {
                position: 'top',
                labels: {
                    color: '#fff'
                }
            },
            title: {
                display: true,
                text: 'Chart.js Doughnut Chart'
            }
        }
    },
});