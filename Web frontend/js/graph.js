
var configs = {'avgdist': {
    type: 'line',
    data: {
        labels: range(1,52).map((val, _) => {return val.toString()}),
        datasets: [{
            label: 'Distance travelled',
            backgroundColor: "rgb(12, 48, 134)",
            borderColor: "rgb(12, 48, 134)",
            data: get_avgdist(),
            fill: false,
        }]
    },
    options: {
        responsive: true,
        spanGaps: true,
        title: {
            display: true,
            text: 'Average distance travelled'
        },
        tooltips: {
            mode: 'index',
            intersect: false,
        },
        hover: {
            mode: 'nearest',
            intersect: true
        },
        scales: {
            xAxes: [{
                display: true,
                scaleLabel: {
                    display: true,
                    labelString: 'Calendar Week 2020'
                }
            }],
            yAxes: [{
                display: true,
                scaleLabel: {
                    display: true,
                    labelString: 'Distance [m]'
                },
                ticks: {
                    beginAtZero: true
                }
            }]
        }
    }
},
'avgtime': {
    type: 'line',
    data: {
        labels: range(1,52).map((val, _) => {return val.toString()}),
        datasets: [{
            label: 'Average time away from home',
            backgroundColor: "rgb(12, 48, 134)",
            borderColor: "rgb(12, 48, 134)",
            data: get_avgtime(),
            fill: false,
        }]
    },
    options: {
        responsive: true,
        spanGaps: true,
        title: {
            display: true,
            text: 'Average time spent away from home'
        },
        tooltips: {
            mode: 'index',
            intersect: false,
        },
        hover: {
            mode: 'nearest',
            intersect: true
        },
        scales: {
            xAxes: [{
                display: true,
                scaleLabel: {
                    display: true,
                    labelString: 'Calendar Week 2020'
                }
            }],
            yAxes: [{
                display: true,
                scaleLabel: {
                    display: true,
                    labelString: 'Time spent away from home [%]'
                },
                ticks: {
                    beginAtZero: true
                }
            }]
        }
    }
},
'avgplaces': {
    type: 'line',
    data: {
        labels: range(1,52).map((val, _) => {return val.toString()}),
        datasets: [{
            label: 'Average new places visited',
            backgroundColor: "rgb(12, 48, 134)",
            borderColor: "rgb(12, 48, 134)",
            data: get_avgplaces(),
            fill: false,
        }]
    },
    options: {
        responsive: true,
        spanGaps: true,
        title: {
            display: true,
            text: 'Average number of new places'
        },
        tooltips: {
            mode: 'index',
            intersect: false,
        },
        hover: {
            mode: 'nearest',
            intersect: true
        },
        scales: {
            xAxes: [{
                display: true,
                scaleLabel: {
                    display: true,
                    labelString: 'Calendar Week 2020'
                }
            }],
            yAxes: [{
                display: true,
                scaleLabel: {
                    display: true,
                    labelString: 'Average number of new places'
                },
                ticks: {
                    beginAtZero: true
                }
            }]
        }
    }
}};
function range(start, end) {
    return Array(end - start + 1).fill().map((_, idx) => start + idx);
}

  
function randomScalingFactor(){
    const val = Math.random() * 10
    if (val < 9){
        return val
    }
    return NaN
}

function generateGraph(){
    const ctx = document.getElementById("graphCanvas").getContext('2d');
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    let graphKey = urlParams.get('graph');
    if (!graphKey) {
        graphKey = 'avgdist'
    }

    document.getElementById("descriptionContainer").innerHTML = "<h5>" + window.descriptions[graphKey]['title'] + "</h5><p>" + window.descriptions[graphKey]['text'] + "</p>";
    window.graph = new Chart(ctx, configs[graphKey]);
}

window.onload = function() {
    generateGraph()
    
};

$(window).on('resize', function(e) {
    generateGraph()
});