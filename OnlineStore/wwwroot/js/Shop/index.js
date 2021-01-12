$(document).ready(function () {
    const urlParams = new URLSearchParams(window.location.search);
    if (urlParams.has("order")) {
        $('.sort [value="' + urlParams.get("order") + '"]').attr("checked", "checked");
    }

    document.querySelectorAll('.btn-addtocart').forEach(item => {
        item.addEventListener('click', event => {

            let actualPrice = getTotalPrice();
            let itemPrice = parseFloat(item.dataset.price.replace(',', '.'));
            let newTotal = actualPrice + itemPrice;
            
            $.toastDefaults = {
                position: 'top-right',
                dismissible: true,
                stackable: true,
                pauseDelayOnHover: true,
                style: {
                    toast: '.toast', 
                }
            };
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
                body: productId
            })
                .then(response => {
                    if (response.status == 200) {
                    item.children[0].classList.remove("d-none");
                    item.children[1].classList.add("d-none");
                    item.disabled = false;
                    setTotalPrice(newTotal.toFixed(2));
                    createToast('Dodano do koszyka', item.dataset.productName, itemPrice);
                }})   
        })
    })
});

function submit() {
    $("form").submit();
}

function getTotalPrice() {
    let amount = document.getElementById("amount");
    amount = parseFloat(amount.textContent.replace(',', '.'));
    return amount;
}

function setTotalPrice(price) {
    let amount = document.getElementById("amount");
    amount.textContent = price;
}

function createToast(toastTitle, productName, productPrice) {
    $.toast({
        title: toastTitle ,
        subtitle: 'teraz',
        content: productName + ' (' + productPrice + ' zł)',
        type: 'toast',
        delay: 5000,
        dismissible: true
    });
}