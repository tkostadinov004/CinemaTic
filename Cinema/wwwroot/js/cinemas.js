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
document.querySelector('#input-search').addEventListener("keyup", function () {
    var input = document.querySelector("#input-search");
    var filter = input.value.toLowerCase();
    var items = document.querySelector(".items").getElementsByTagName("button");
    for (i = 0; i < items.length; i++) {
        txtValue = items[i].textContent || items[i].innerText;
        if (txtValue.toLowerCase().startsWith(filter)) { //txtValue.toUpperCase().indexOf(filter) > -1
            items[i].style.display = "";
        } else {
            items[i].style.display = "none";
        }
    }
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