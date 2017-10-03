$(document).ready(function () {
    var movieDT = $("#movies").DataTable({
        ajax: {
            url: "api/Movies",
            dataSrc: ""
        },
        columns: [
            {
                data: "name",
                render: function (data, type, row) {
                    return "<a href='/data/edit/" + row.id + "'>" + data + "</a>";
                }
            },
            {
                data: "genre.genreType"
            },
            {
                data: "id",
                render: function (data) {
                    return "<button data-movie-id='" + data + "' class='btn-link js-delete'>Delete</button>";
                }
            }

        ] 
    });

    $("#movies").on("click", ".js-delete", function () {
        var button = $(this);
        bootbox.confirm("Are you sure you want to delete this movie?", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/movies/" + button.data("movie-id"),
                    method: "DELETE",
                    success: function () {
                        movieDT.row(button.parents("tr")).remove().draw();
                    }
                });
            }
        });
    });

});