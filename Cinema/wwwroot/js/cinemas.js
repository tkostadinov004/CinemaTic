/*Colors */
function changeColors(bgColor, boardColor, textColor, btnBgColor, btnTextColor, accentColor) {
    console.log(bgColor);
    if (bgColor) {
        $('.main').css('backgroundColor', bgColor);
    }
    if (boardColor) {
        $('.dates-container, .dates-info-container, .movies-list').each(function () {
            $(this).css('backgroundColor', boardColor);
        });
    }
    if (textColor) {
        $('*').each(function () {
            $(this).css('color', textColor);
        });
    }
    if (btnBgColor) {
        $('button:not(.time-button)').each(function () {
            $(this).css('backgroundColor', btnBgColor);
        });
    }
    if (btnTextColor) {
        $('button:not(.time-button)').each(function () {
            $(this).css('color', btnTextColor);
        });
    }
    if (accentColor) {
        $('button.active').each(function () {
            $(this).css('border-color', accentColor);
        });
    }
}
/*Action*/
document.querySelector('.dropbtn').addEventListener("click", function () {
    document.querySelector(".dropdown-input").classList.toggle("show");
});
var dropdownMenu = document.querySelector('.dropdown-input');
$(document).click(function (e) {
    e.stopPropagation();
    var dropdown = $('.dropdown');

    if (dropdown.has(e.target).length === 0) {
        dropdownMenu.classList.remove("show");
    }
});
let buttons = document.querySelectorAll(".selection-buttons button");
let infoContainer = document.querySelector('.dates-info-container');
buttons.forEach(i => i.addEventListener("click", function () {
    buttons.forEach(k => k.classList.remove("active"));
    i.classList.add("active");

    if (i.id == 'movie-grid-button') {
        infoContainer.querySelector('#movie-grid').removeAttribute("hidden");
        infoContainer.querySelector('#by-movie').setAttribute("hidden", "true");
    }
    else if (i.id == 'by-movie-button') {
        infoContainer.querySelector('#by-movie').removeAttribute("hidden");
        infoContainer.querySelector('#movie-grid').setAttribute("hidden", "true");
    }
}));