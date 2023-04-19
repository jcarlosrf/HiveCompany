var map;
window.initMap = initMap;


function initMap() {

    var latlng = new google.maps.LatLng(-15.7217003, -48.1021774);

    var options = {
        zoom: 5,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    map = new google.maps.Map(document.getElementById("maparota"), options);
    
}