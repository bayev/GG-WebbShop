// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
function setUrl() {
    window.location.href = 'SearchResult?id=' + document.getElementById('searchInput').value;
};

function CatUrl() {
    window.location.href = 'CategoryResultPage?QueryString=' + document.getElementById('searchInput').value;
}
function CatUrl1() {
    window.location.href = 'CategoryResultPage?QueryString=' + document.getElementById('Sweater').value;
}
function CatUrl2() {
    window.location.href = 'CategoryResultPage?QueryString=' + document.getElementById('Accessories').value;
}
function CatUrl3() {
    window.location.href = 'CategoryResultPage?QueryString=' + document.getElementById('Blazer').value;
}
function CatUrl4() {
    window.location.href = 'CategoryResultPage?QueryString=' + document.getElementById('Pants').value;
}
function CatUrl5() {
    window.location.href = 'CategoryResultPage?QueryString=' + document.getElementById('Shirt').value;
}

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


