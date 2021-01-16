
$(document).ready(function () {

    document.querySelectorAll('#btn-addtocart').forEach(item => {
        item.addEventListener('click', event => {
            let itemPrice = parseFloat(item.dataset.price.replace(',', '.'));
            loadToastDefaults();
            item.children[0].classList.add("d-none");
            item.children[1].classList.remove("d-none");
            item.disabled = true;
            let productId = item.dataset.productId;

            fetch(apiUrls["addProductToCart"], {
                headers: {
                    'Content-Type': "application/json",
                    'RequestVerificationToken': document.getElementById("RequestVerificationToken").value
                },
                method: "POST",
                body: JSON.stringify({ productId, count: 1 })
            })
                .then(response => response.json())
                .then(data => {
                    console.log(data);
                    item.children[0].classList.remove("d-none");
                    item.children[1].classList.add("d-none");
                    item.disabled = false;
                    setTotalPrice(data.summaryCost.toFixed(2).toString().replace(".", ","));
                    createToast('Dodano do koszyka', item.dataset.productName, itemPrice);
                })
        })
    })
    
});


$("#description").click(function (e) {
   $("#reviews-accordion").addClass("d-none").fadeOut(500).fadeIn(0);
   $("#description-accordion").toggleClass("d-none").fadeOut(0).fadeIn(500);
   $("#reviews-panel").removeClass("tile-active");
    $("#description").toggleClass("tile-active").fadeOut(0).fadeIn(500);
});

$("#reviews-panel").click(function (e) {
    $("#description-accordion").addClass("d-none").fadeOut(500).fadeIn(0);
    $("#reviews-accordion").toggleClass("d-none").fadeOut(0).fadeIn(500);
    $("#description").removeClass("tile-active");
    $("#reviews-panel").toggleClass("tile-active").fadeOut(0).fadeIn(500);
});


$(function () {
    $('#ratingbar').barrating({
        theme: 'fontawesome-stars'
    });
});

function setTotalPrice(price) {
    let amount = document.getElementById("amount");
    amount.textContent = price;
}

function createToast(toastTitle, productName, productPrice) {
    $.toast({
        title: toastTitle,
        subtitle: 'teraz',
        content: productName + ' (' + productPrice + ' zł)',
        type: 'toast',
        delay: 5000,
        dismissible: true
    });
}

function loadToastDefaults() {
    $.toastDefaults = {
        position: 'top-right',
        dismissible: true,
        stackable: true,
        pauseDelayOnHover: true,
        style: {
            toast: '.toast',
        }
    };
}