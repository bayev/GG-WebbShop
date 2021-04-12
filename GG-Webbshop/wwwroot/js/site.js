// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
function setUrl() {
    window.location.href = 'SearchResult?id=' + document.getElementById('searchInput').value;
};

function scrollDown() {
    var x = document.getElementById("PaymentDiv");
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
    window.scrollBy(0, 500);
}
function infoDivShow() {
    var x = document.getElementById("infoDiv");
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
    window.scrollBy(0, 500);
}

//$(document).ready(function () {

//    $('.radio-group .radio').click(function () {
//        $('.radio').addClass('gray');
//        $(this).removeClass('gray');
//    });
//});


