$(document).ready(function () {
    var path = window.location.pathname;
    if (path == '/') {
        $.notify("Testing on notify JS", "success")
    }   
});