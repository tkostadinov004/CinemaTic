document.querySelector('#input-search').addEventListener("keyup", function () {
    var input = document.querySelector("#input-search");
    var filter = input.value.toLowerCase();
    var items = document.querySelector(".items").querySelectorAll("button, label");
    for (i = 0; i < items.length; i++) {
        txtValue = items[i].textContent || items[i].innerText;
        if (txtValue.toLowerCase().startsWith(filter)) { //txtValue.toUpperCase().indexOf(filter) > -1
            items[i].parentElement.style.display = "";
        } else {
            items[i].parentElement.style.display = "none";
        }
    }
});