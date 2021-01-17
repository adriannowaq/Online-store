const form = document.getElementById("EditProduct");

$(document).ready(() => {
    $("#EditProduct").on("submit", submit);

    $("input[type=radio][name=Photo]").change(function () {
        if (this.value == 1) {
            $("#changePhoto").removeClass("d-none");
        }
        else {
            $("#changePhoto").addClass("d-none");
        }
    });
});

function submit(event) {
    event.preventDefault();
    if ($("#EditProduct").valid()) {
        $("#EditProduct input[type='submit']").prop("disabled", true).addClass("d-none");

        let loadingSpinner = $("#LoadingSpinner");
        loadingSpinner.removeClass("d-none");

        fetch(apiUrls["adminEditProduct"], {
            method: "POST",
            body: new FormData(form)
        })
            .then(response => response.json())
            .then(data => {
                loadingSpinner.addClass("d-none");
                window.setTimeout(() => {
                    window.location.href = data.redirectUrl;
                }, 1000);
            })
    }
}