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