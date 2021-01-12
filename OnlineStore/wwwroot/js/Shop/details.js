$("#description").click(function (e) {
   $("#reviews-accordion").addClass("d-none").fadeOut(500).fadeIn(0);
   $("#description-accordion").toggleClass("d-none").fadeOut(0).fadeIn(500);
   $("#reviews-panel").removeClass("tile-active");
   $("#description").toggleClass("tile-active").fadeOut(0).fadeIn(500);
});

$("#reviews-panel").click(function (e) {
    $("#description-accordion").addClass("d-none").fadeOut(500).fadeIn(0);
    $("#reviews-accordion").toggleClass("d-none").fadeOut(0).fadeIn(500);
    $("#description").removeClass("tile-active");
    $("#reviews-panel").toggleClass("tile-active").fadeOut(0).fadeIn(500);
});


$(function () {
    $('#ratingbar').barrating({
        theme: 'fontawesome-stars'
    });
});