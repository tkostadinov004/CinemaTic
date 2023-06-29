var sectionArray = [1, 2, 3, 4, 5];
$.each(sectionArray, function (index, value) {
    var stickyNavTop = $('.navbar').offset().top + 65;
    var stickyNav = function () {
        var scrollTop = $(window).scrollTop(); 

        if (scrollTop > stickyNavTop) {
            $('.navbar-nav .nav-item .nav-link').removeClass('inactive');
            $('.navbar-nav .nav-item .nav-link').addClass('active');
            $('.navbar').addClass('sticky');
        }
        else {
            $('.navbar-nav .nav-item .nav-link:link').removeClass('active');
            $('.navbar-nav .nav-item .nav-link').addClass('inactive');
            $('.navbar').removeClass('sticky');
        }
    };

    stickyNav();
    $(window).scroll(function () {
        stickyNav();
    });
    $('.click-scroll').eq(index).click(function (e) {
        var offsetClick = $('#' + 'section_' + value).offset().top - 75;
        e.preventDefault();
        $('html, body').animate({
            'scrollTop': offsetClick
        }, 300)
    });
});

$(document).ready(function () {
    $('.navbar .navbar-nav .nav-item .nav-link:link').addClass('inactive');
    $('.navbar .navbar-nav .nav-item .nav-link').eq(0).addClass('active');
    $('.navbar .navbar-nav .nav-item .nav-link:link').eq(0).removeClass('inactive');
});