var filter = document.querySelector(".jsFilter");
if (filter) {
    filter.addEventListener("click", function () {
        document.querySelector(".filter-menu").classList.toggle("active");
    });
}
var grid = document.querySelector(".grid");
if (grid) {
    grid.addEventListener("click", function () {
        document.querySelectorAll(".movies-container .cinema-row").forEach(i => i.setAttribute("hidden", true));
        document.querySelectorAll(".movies-container .movie-grid").forEach(i => i.removeAttribute("hidden"));
        document.querySelector(".list").classList.remove("active");
        document.querySelector(".grid").classList.add("active");
        document.querySelector(".products-area-wrapper:not(.details)").classList.add("gridView");
        document
            .querySelector(".products-area-wrapper")
            .classList.remove("tableView");
        var rows = document.querySelectorAll('.image:not(.details-photo-pic) .cinema-link');
        if (rows && rows.length > 0) {
            rows.forEach(i => i.classList.add("flex-column"));
        }
    });
}
var list = document.querySelector(".list");
if (list) {
    list.addEventListener("click", function () {
        document.querySelectorAll(".movies-container .cinema-row").forEach(i => i.removeAttribute("hidden"));
        document.querySelectorAll(".movies-container .movie-grid").forEach(i => i.setAttribute("hidden", true));
        document.querySelector(".list").classList.add("active");
        document.querySelector(".grid").classList.remove("active");
        document.querySelector(".products-area-wrapper:not(.details)").classList.remove("gridView");
        document.querySelector(".products-area-wrapper:not(.details)").classList.add("tableView");
        var rows = document.querySelectorAll('.image:not(.details-photo-pic) .cinema-link');
        if (rows && rows.length > 0) {
            rows.forEach(i => i.classList.remove("flex-column"));
        }
    });
}

var modeSwitch = document.querySelector('.mode-switch');
if (modeSwitch) {
    modeSwitch.addEventListener('click', function () {
        document.documentElement.classList.toggle('light');
        modeSwitch.classList.toggle('active');
    });
}

var statuses = document.querySelectorAll("div.cinema-row div.status-cell span.status");
for (var i = 0; i < statuses.length; i++) {
    var value = statuses[i].innerHTML;
    if (value == 'Approved') {
        statuses[i].classList.add('approved');
    }
    else if (value == 'Pending approval') {
        statuses[i].classList.add('pending-approval');
    }
}

document.querySelectorAll('.sidebar-list-item').forEach(i => i.addEventListener("click", function () {
    document.querySelectorAll('.sidebar-list-item').forEach(j => j.classList.remove("active"));
    i.classList.add("active");
}));
function setStatuses() {
    var statuses = document.querySelectorAll("div.cinema-row div.status-cell span.status");
    if (statuses.length > 0) {
        for (var i = 0; i < statuses.length; i++) {
            var value = statuses[i].innerHTML;
            if (value == 'Approved') {
                statuses[i].classList.add('approved');
            }
            else if (value == 'Pending approval') {
                statuses[i].classList.add('pending-approval');
            }
        }
    }
}
var loadFile = function (event) {
    var output = document.getElementById('tempImg');
    output.src = URL.createObjectURL(event.target.files[0]);
    output.removeAttribute("hidden");
    output.onload = function () {
        URL.revokeObjectURL(output.src);
    }
};



function getItems(controllerName, actionName, searchText, value, filterValue) {
    let key = "id";
    if (actionName == 'SearchAndFilterCinemas') {
        console.log(1);
        key = 'userEmail';
    }
    var data = { "searchText": searchText, filterValue };
    data[key] = value;
    $.ajax({
        type: "POST",
        url: `/${controllerName}/${actionName}`,
        data: data,
        success: function (response) {
            $("#results-container").html(response);
            setStatuses();
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function editCinema(cinemaId) {
    $.ajax({
        type: "GET",
        url: "/Owners/EditCinema",
        data: { cinemaId },
        success: function (response) {
            $("#review-form-container").html(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}