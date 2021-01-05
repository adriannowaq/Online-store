const form = document.getElementById("AddProduct");
let apiUrl = "";

$(document).ready(() => {
    $("#AddProduct").on("submit", submit);
});

function setApiUrl(url) {
    apiUrl = url;
}

function submit(event) {
    event.preventDefault();
    if ($("#AddProduct").valid()) {
        let loadingSpinner = $("#LoadingSpinner");
        $("#AddProduct input[type='submit']").prop("disabled", true).addClass("d-none");
        loadingSpinner.removeClass("d-none");

        fetch(apiUrl, {
            method: "POST",
            body: new FormData(form)
        })
            .then(response => response.json())
            .then(data => {
                loadingSpinner.addClass("d-none");
                $("#ProductAdded").removeClass("d-none");

                window.setTimeout(() => {
                    window.location.href = data.redirectUrl;
                }, 3000);
            })
    }
}