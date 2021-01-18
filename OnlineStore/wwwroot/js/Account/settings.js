$(document).ready(function () {
    $("#addressType select").on("change", function () {
        $("#addressType").submit();
    });
});