
function setUrl() {
    window.location.href = 'SearchResult?id=' + document.getElementById('searchInput').value;
};

function CatUrl() {
    window.location.href = 'CategoryResultPage?QueryString=' + document.getElementById('AllProducts').value;s
}
function CatUrl1() {
    window.location.href = 'CategoryResultPage?QueryString=' + document.getElementById('Sweaters').value;
}
function CatUrl2() {
    window.location.href = 'CategoryResultPage?QueryString=' + document.getElementById('Accessories').value;
}
function CatUrl3() {
    window.location.href = 'CategoryResultPage?QueryString=' + document.getElementById('Blazers').value;
}
function CatUrl4() {
    window.location.href = 'CategoryResultPage?QueryString=' + document.getElementById('Pants').value;
}
function CatUrl5() {
    window.location.href = 'CategoryResultPage?QueryString=' + document.getElementById('Shirts').value;
}
function CatUrl6() {
    window.location.href = 'CategoryResultPage?QueryString=' + document.getElementById('T-shirts').value;
}
function CatUrl7() {
    window.location.href = 'CategoryResultPage?QueryString=' + document.getElementById('Diverse').value;
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
function reaUrl() {
    window.location.href = 'SearchResult?id=rea';
};



