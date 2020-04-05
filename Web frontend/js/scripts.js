


function defaultMap(){
    window.mymap = L.map('map').setView([62.3875, 16.325556], 5);

    let myTileLayer = L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}', {
        attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
        maxZoom: 18,
        id: 'mapbox/streets-v11',
        tileSize: 512,
        zoomOffset: -1,
        accessToken: 'pk.eyJ1Ijoia2ltY3Jpc2lzMjAyMCIsImEiOiJjazhrcDh1MnIwMDVnM2xuMmEydDNhdXVqIn0.F7-tpWrGM3WstUlvRX9RZA'
    }).addTo(window.mymap);
}


function drawTravelspaths(){
    let polyLines = []
    get_commonPaths().map((path, idx) => {
        let pointList = []
        for(let i=0; i < path.length; i++){
            let lat = path[i][0], lon = path[i][1];
            let point = new L.LatLng(lat, lon);
            pointList.push(point);
        }
        polyLines.push(new L.Polyline(pointList, {
            color: 'red',
            weight: 3,
            opacity: 0.5,
            smoothFactor: 1
        }));
    });

    for (polyLine of polyLines){
        polyLine.addTo(window.mymap)
    }
    window.mymap.setView([62.3875, 16.325556], 5);
}

function drawMalmo(){
    window.mymap.setView([55.6089924,12.9996260], 13); //Malmö
    L.circle([55.6087193,12.9993771], 120).addTo(window.mymap);
    
}

function drawGoteborg(){
    window.mymap.setView([57.7083698,11.9750255], 13); //Göteborg
    L.circle([57.7082687,11.9724157], 250).addTo(window.mymap); // Centralstationen
    L.circle([57.6964669,11.9870811], 300).addTo(window.mymap); // Korsvägen
    L.circle([57.7000671,11.9521485], 180).addTo(window.mymap); // Järntorget
}   

function drawStockholm(){
    window.mymap.setView([59.3311284,18.0565218], 13); //Stockholm
    L.circle([59.3137671,18.0623034], 220).addTo(window.mymap);
    L.circle([59.3231263,18.0669413], 420).addTo(window.mymap);
    L.circle([59.3310605,18.0567021], 180).addTo(window.mymap);
    L.circle([59.3319858,18.0292428], 160).addTo(window.mymap);
    L.circle([59.3387899,18.0905982], 220).addTo(window.mymap);
    L.circle([59.3076528,18.0746842], 150).addTo(window.mymap);
}

function drawContributions(id){
    const url = "https://hackcrisisapi.azurewebsites.net/api/Coordinates"
    
    fetch(url).then(function (response) {
        return response.json();
    }).then(function (json) {
        let lats = [];
        let lons = [];
        for( let i = 0; i < json.length; i++){
            let entry = json[i];
            if (entry['appuserid']) {
                if (entry['appuserid'] == id) {
                    let lat = entry['latitude'];
                    let lon = entry['longitude'];
                    lats.push(lat);
                    lons.push(lon);
                    L.circle([lat,lon], 2).addTo(window.mymap);
                }
            }
        }

        if(lats.length > 0){
            const arrAvg = arr => arr.reduce((a,b) => a + b, 0) / arr.length;
            window.mymap.setView([arrAvg(lats), arrAvg(lons)], 13);
        } else {
            
            alert("No positions from ID: " + id + " found");
        }
        // 0acb2961-9ed9-4d8c-b808-c4677da69cd7
        
    });
}

function generateMap(){
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    let mapviewKey = urlParams.get('mapview');
    if (!mapviewKey) {
        mapviewKey = 'main'
    }
    defaultMap();

    if (mapviewKey == 'travel'){
        drawTravelspaths();
    } else if(mapviewKey == 'malmo'){
        drawMalmo();
    } else if(mapviewKey == 'goteborg') {
        drawGoteborg();
    } else if(mapviewKey == 'stockholm') {
        drawStockholm();
    }else if(mapviewKey == 'contributions') {
        const id = urlParams.get('id');
        drawContributions(id);
    }
}

window.onload = function() {
    generateMap();
};

// Search button
function searchClick(){
    const id = document.getElementById("covidID").value;
    window.location.href = "?mapview=contributions&id=" + id
}

