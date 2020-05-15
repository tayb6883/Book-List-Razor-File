
var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    //columns follow camel case
    //ajax to load data

    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "api/book/GetAll",
            "type": "Get",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "author", "width": "20%" },
            { "data": "isbn", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    //bind Edit and delete button

                    return `
                     <a href="/BookList/Upsert?id=${data}"  style='cursor:pointer; width:70px' class='btn btn-sm btn-success text-white'>
                    Edit
                    </a > 
                    &nbsp;
                     <a  
style='cursor:pointer; width:70px'
class='btn btn-sm btn-danger text-white'
onClick=DeleteBook('api/book/DeleteBook?bookId='+${data})>
                    Delete
                    </a > `
                },
                "width":"40%"
            },

        ],
        "language": {

            "emptyTable": "No Data Found"
        },
        "width":"100%"
    })
}


function DeleteBook(url) {

    swal({
        title: "Are you sure?",
        text: "Once deleted, you wont be able to recover",
        icon: "warning",
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "Delete",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message)
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message)
                    }
                }
            })

        }
    });
}