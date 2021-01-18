function addItemToCart(item) {
    item.addEventListener('click', event => {
        let itemPrice = parseFloat(item.dataset.price.replace(',', '.'));
        let count = document.getElementById("itemsCount");
        if (count==null) {
            count = 1
        }
        else {
            count =  Number(document.getElementById("itemsCount").value)
        }
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
            body: JSON.stringify({ productId, count })
        })
            .then(response => response.json())
            .then(data => {
                console.log(data);
                item.children[0].classList.remove("d-none");
                item.children[1].classList.add("d-none");
                item.disabled = false;
                setTotalPrice(data.summaryCost.toFixed(2).toString().replace(".", ","));
                createToast('Dodano do koszyka', item.dataset.productName, itemPrice, count);
            });
    });
}

function setTotalPrice(price) {
    let amount = document.getElementById("amount");
    amount.textContent = price;
}

function createToast(toastTitle, productName, productPrice, count) {
    $.toast({
        title: toastTitle,
        subtitle: 'teraz',
        content: productName + ' (' + productPrice + ' zł) ' + count +'szt.',
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