$(document).ready(function () {
    $(window).scroll(function () {

        console.log($(window).scrollTop());

        if ($(window).scrollTop() < 250) {
            $('#filter').removeClass('navbar-fixed-top');
            $('#filter').removeClass('filterPos');

        }

        if ($(window).scrollTop() > 250) {
            $('#filter').addClass('navbar-fixed-top');
            $('#filter').addClass('filterPos');
        }
    });
});