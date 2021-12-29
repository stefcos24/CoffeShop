var dataTable;


$(document).ready(function () {
    loadDataTable("GetOrderList")
});

function loadDataTable(url) {
    dataTable = $("#myTable").DataTable();
}