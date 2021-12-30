var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable(url) {
    dataTable = $("#myTable").DataTable({
        "ajax": {
            "url": "/Order/GetOrderList"
        },
        "columns": [
            { "data": "id", "width": "15%" },
            { "data": "fullName", "width": "15%" },
            {
                "data": "orderDate",
                "render": function (data) {
                    var formatDate = new Date(data);
                    var day = formatDate.getDate();
                    var month = formatDate.getMonth();
                    month += 1;
                    var year = formatDate.getFullYear();
                    var hours = formatDate.getHours();
                    var minutes = formatDate.getMinutes();
                    
                    return date = day + "." + month + "." + year + " " + hours + ":" + minutes;
                },
                "width": "15%"
            }
        ],
        "order": [[2, "desc"]],
        "language": { "url": "//cdn.datatables.net/plug-ins/1.11.3/i18n/bs_BA.json"}
    });
}