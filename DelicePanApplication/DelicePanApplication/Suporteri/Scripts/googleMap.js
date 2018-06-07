function myMap() {
    var mapCanvas = document.getElementById("map");
    var myCenter = new google.maps.LatLng(46.77, 23.59);
    var mapOptions = { center: myCenter, zoom: 8 };
    var map = new google.maps.Map(mapCanvas, mapOptions);
    google.maps.event.addListener(map, 'click', function (event) {
        placeMarker(map, event.latLng);
    });
}
var markers = [];
function placeMarker(map, location) {
    var marker = new google.maps.Marker({
        position: location,
        map: map
    });
    markers.push(marker);
    var infowindow = new google.maps.InfoWindow({
        content: 'Latitude: ' + location.lat() + '<br>Longitude: ' + location.lng()
    });
    infowindow.open(map, marker);

    // Zoom into when clicking on marker
    google.maps.event.addListener(marker, 'click', function () {
        map.setZoom(marker.map.zoom + 1);
        map.setCenter(marker.getPosition());
    });
    $('#lat').val(location.lat());
    $('#lng').val(location.lng());
}
// Deletes all markers in the array by removing references to them.
function deleteMarkers(marker) {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
    $('#lat').val('');
    $('#lng').val('');
}