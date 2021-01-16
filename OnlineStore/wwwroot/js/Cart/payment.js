$(document).ready(() => {
    $("#address").change(function () {
        $(this).prop("disabled", true);
        window.location.href = $(this).attr("data-url").replace("True", this.checked);
    });
});