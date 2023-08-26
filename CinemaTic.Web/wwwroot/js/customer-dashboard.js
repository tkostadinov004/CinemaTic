function getCinemas(area, controllerName, actionName, all, pageNumber, searchText) {
    $.ajax({
        type: "GET",
        url: `/${area}/${controllerName}/${actionName}`,
        data: { searchText, pageNumber, all },
        success: function (response) {
            $("section#cinemas #results-container").html(response);
        },
        failure: function (response) {
            window.location = '/Error/statuscode=403';
        },
        error: function (response) {
            window.location = '/Error/statuscode=403';
        }
    });
}
function getTickets(area, controllerName, actionName, pageNumber, searchText) {
    $.ajax({
        type: "GET",
        url: `/${area}/${controllerName}/${actionName}`,
        data: { searchText, pageNumber },
        success: function (response) {
            $("section#tickets #results-container").html(response);
        },
        failure: function (response) {
            window.location = '/Error/statuscode=403';
        },
        error: function (response) {
            window.location = '/Error/statuscode=403';
        }
    });
}