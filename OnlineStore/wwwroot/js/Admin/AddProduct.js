const form = document.getElementById("AddProduct");

$(document).ready(() => {
    $("#AddProduct").on("submit", submit);
});

function submit(event) {
    event.preventDefault();
    if ($("#AddProduct").valid()) {
        let loadingSpinner = $("#LoadingSpinner");
        $("#AddProduct input[type='submit']").prop("disabled", true).addClass("d-none");
        loadingSpinner.removeClass("d-none");

        fetch(apiUrls["adminAddProduct"], {
            method: "POST",
            body: new FormData(form)
        })
            .then(response => response.json())
            .then(data => {
                loadingSpinner.addClass("d-none");
                $("#ProductAdded").removeClass("d-none");
                $("html, body").animate({ scrollTop: 0 }, "fast");

                window.setTimeout(() => {
                    window.location.href = data.redirectUrl;
                }, 3000);
            })
    }
}