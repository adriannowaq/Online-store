
$(document).ready(function () {
    const urlParams = new URLSearchParams(window.location.search);
    if (urlParams.has("order")) {
        $('.sort [value="' + urlParams.get("order") + '"]').attr("checked", "checked");
    }

    document.querySelectorAll('.btn-addtocart').forEach(item => {
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



function submit() {
    $("form").submit();
}

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
