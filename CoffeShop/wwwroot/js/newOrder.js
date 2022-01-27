var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable(url) {
    dataTable = $('#tableData').DataTable({
        "ajax": {
            "url": "/Order/GetOrderList"
        },
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "fullName", "width": "20%" },
            { "data": "email", "width": "20%" },
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
                "width": "20%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Order/Details/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="bi bi-pencil-square"></i>
                            </a>
                        </div>
                    `;
                },
                "width": "15%"
            }
        ],
        "order": [[0, "desc"]],
        "language": { "url": "//cdn.datatables.net/plug-ins/1.11.3/i18n/bs_BA.json" }
    });
}