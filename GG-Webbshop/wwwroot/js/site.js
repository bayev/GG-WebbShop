﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
function setUrl() {
    window.location.href = 'SearchResult?id=' + document.getElementById('searchInput').value;
};

//function setUrlProductView() {
//    window.location.href = 'ProductView?id=' + document.getElementById('product-id').value;
//};

