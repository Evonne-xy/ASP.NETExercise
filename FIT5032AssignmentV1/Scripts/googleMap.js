let map;

var xmlHttp = new XMLHttpRequest();
xmlHttp.open("GET", "Providers/GetProviderDetail", false);
xmlHttp.send();
var providers = JSON.parse(xmlHttp.responseText);


function initMap() {
    const directionsService = new google.maps.DirectionsService();
    const directionsRenderer = new google.maps.DirectionsRenderer();
    var buttonDirection = document.getElementById("Direction");
    const image = "https://developers.google.com/maps/documentation/javascript/examples/full/images/beachflag.png";

    map = new google.maps.Map(document.getElementById("map"), {
        center: { lat: 39.836334, lng: 116.416642 },
        zoom: 12,
    });

    // Try HTML5 geolocation.
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
            (position) => {
                const pos = {
                    lat: position.coords.latitude,
                    lng: position.coords.longitude,
                };
                map.setCenter(pos);
            },
            () => {
                handleLocationError(true, infoWindow, map.getCenter());
            }
        );
    }
    else
    {
        // Browser doesn't support Geolocation
        handleLocationError(false, infoWindow, map.getCenter());
    }
    addmarker();

    // search location
    const geocoder = new google.maps.Geocoder();

    document.getElementById("submit").addEventListener("click", () => {
        geocodeAddress(geocoder, map);
    });
   

    //place complete
    const input = document.getElementById("userInput");
    const autocomplete = new google.maps.places.Autocomplete(input);
    autocomplete.setTypes(["address"])
    autocomplete.bindTo("bounds", map);

    //Find direction
    directionsRenderer.setPanel(document.getElementById("right-panel"));
    directionsRenderer.setMap(map);
    buttonDirection.addEventListener("click", function () {
        calculateAndDisplayRoute(directionsService, directionsRenderer);
    });
    
}

//  serach location function
function geocodeAddress(geocoder, resultsMap) {
    const address = document.getElementById("address").value;
    console.log(address);
    geocoder.geocode({ address: address }, (results, status) => {
        if (status === "OK") {
            resultsMap.setCenter(results[0].geometry.location);
            var marker = new google.maps.Marker({
                map: resultsMap,
                position: results[0].geometry.location,
            });
            marker.addListener("click", () => {
                infowindow.open(resultsMap, marker);
            });   
        } else {
            alert("Geocode was not successful for the following reason: " + status);
        }
    });
}

//Add marker
function addmarker() {
    var geocoder = new google.maps.Geocoder();
    for (i = 0; i < providers.length; i++) {
        console.log(providers[i]);
        getLongLat(geocoder, map, providers[i]);
    }
}

function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(
        browserHasGeolocation
            ? "Error: The Geolocation service failed."
            : "Error: Your browser doesn't support geolocation."
    );
    infoWindow.open(map);
}


function getLongLat(geocoder, resultsMap, provider) {
    var contentString = '<div id="content">' + "<p1>" + provider.ProviderName + "</p1>" + "<p2>" + provider.ProviderAddress + "</p2>";
    var infowindow = new google.maps.InfoWindow({
        content: contentString,
    });
    geocoder.geocode({ address: provider.ProviderAddress }, function (results, status) {
        const image = "https://developers.google.com/maps/documentation/javascript/examples/full/images/beachflag.png";
        if (status === "OK") {
           var marker =  new google.maps.Marker({
                map: resultsMap,
               position: results[0].geometry.location,
               icon: image
           });
            marker.addListener("click", () => {
                infowindow.open(resultsMap, marker);
            });   
        }
    })
}

function calculateAndDisplayRoute(directionsService, directionsRenderer) {
    directionsService.route(
        {
            origin: {
                query: document.getElementById("userInput").value,
            },
            destination: {
                query: document.getElementById("end").value,
            },
            travelMode: google.maps.TravelMode.DRIVING,
        },
        (response, status) => {
            if (status === "OK") {
                directionsRenderer.setDirections(response);
            } else {
                window.alert("Directions request failed due to " + status);
            }
        }
    );
}
