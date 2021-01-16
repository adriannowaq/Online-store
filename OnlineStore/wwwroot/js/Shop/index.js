
$(document).ready(function () {
    const urlParams = new URLSearchParams(window.location.search);
    if (urlParams.has("order")) {
        $('.sort [value="' + urlParams.get("order") + '"]').attr("checked", "checked");
    }

    document.querySelectorAll('.btn-addtocart').forEach(item => {
        addItemToCart(item);
    })
});

function submit() {
    $("form").submit();
}


