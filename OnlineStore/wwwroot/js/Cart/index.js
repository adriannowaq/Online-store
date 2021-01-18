$(document).ready(() => {
    $(".cart-count input").each(function () {
        $(this).attr("data-old-val", $(this).val());
    });

    $(".cart-remove svg").on("click", function () {
        let input = $(this).closest(".list-group-item").find(".cart-count").find("input");
        if (input.attr("data-item-on-change") === "true")
            return;

        $("#payment button").prop("disabled", true);
        $(this).off("click").addClass("d-none");
        input.prop("disabled", true);

        let spinner = $(this).closest("div").find(".spinner-border");
        let div = $(this).closest(".list-group-item");
        let productId = div.attr("data-item-id");
        let finalPrice = $("#final-price");
        spinner.removeClass("d-none");

        fetch(apiUrls["cartDeleteItem"], {
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": $("#RequestVerificationToken").val()
            },
            method: "POST",
            body: productId
        })
        .then(response => response.json())
            .then(data => {
                spinner.addClass("d-none");
                $("[data-toggle='popover']").popover("dispose");
                div.slideUp(700, function () {
                    $(this).remove();
                    $("[data-item-error='true']").popover("show");
                });

                let finalCost = data.summaryCost.toFixed(2).replace(".", ",");
                $("#amount").html(finalCost);
                finalPrice.html(finalCost);

                let anyError = false;
                $(".cart-count input").each(function () {
                    if ($(this).attr("data-item-error") === "true") {
                        anyError = true;
                        return false;
                    }
                });
                if (anyError === false)
                    $("#payment button").prop("disabled", false);

                if (data.cartItems.length < 1)
                    $("#payment button, #login").addClass("d-none");
            });
    })

    $(".cart-count input").on("change paste keyup cut select", function () {
        if (!checkObjNumberIsSafe($(this))) {
            $(this).attr("data-item-error", true);
            $(this).popover("show");
        } else {
            $(this).attr("data-item-error", false);
            $(this).attr("data-item-cost-calculated", false);
            $(this).popover("dispose");
        }

        let submitAllowed = true;
        $(".cart-count input").each(function (i, obj) {
            let error = $(obj).attr("data-item-error");
            if (error === "true") {
                submitAllowed = false;
                return false;
            }
        });
        if (submitAllowed) {
            $("#payment button").prop("disabled", false);
        } else {
            $("#payment button").prop("disabled", true);
        }
    });

    $(".cart-count input").on("focusout", function () {
        if ($(this).attr("data-item-error") === "false") {
            let val = $(this).val();
            if (val === $(this).attr("data-old-val"))
                return;

            $(this).attr("data-item-on-change", true);
            $(this).attr("data-old-val", val);

            let div = $(this).closest(".list-group-item");
            let productId = div.attr("data-item-id");
            let spinner = $(this).closest("div").find(".update-spinner");
            let itemsCost = div.find(".item-cost");
            let finalPrice = $("#final-price");

            $(this).addClass("d-none");
            spinner.removeClass("d-none");
            $("#payment button").addClass("d-none");
            $("#payment div").removeClass("d-none");

            fetch(apiUrls["cartUpdateItem"], {
                headers: {
                    "Content-Type": "application/json",
                    "RequestVerificationToken": $("#RequestVerificationToken").val()
                },
                method: "POST",
                body: JSON.stringify({ productId, count: $(this).val() })
            })
                .then(response => response.json())
                .then(data => {
                    spinner.addClass("d-none");
                    $(this).removeClass("d-none");
                    itemsCost.html(data.cartItems.find(e => e.productId == productId).cost
                        .toFixed(2).replace(".", ","));

                    let finalCost = data.summaryCost.toFixed(2).replace(".", ",");
                    finalPrice.html(finalCost);
                    $("#amount").html(finalCost);

                    $(this).attr("data-item-cost-calculated", true);
                    $("#payment div").addClass("d-none");
                    $("#payment button").removeClass("d-none");
                    $(this).attr("data-item-on-change", false);
                });
        }
     });

    function checkObjNumberIsSafe(obj) {
        if (!Number.isSafeInteger(Number(obj.val())) ||
            obj.val() < parseInt(obj.attr("min")) ||
            obj.val() > parseInt(obj.attr("max")))
            return false;

        return true;
    }
});