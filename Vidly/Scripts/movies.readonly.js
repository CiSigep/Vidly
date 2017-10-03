$(document).ready(function () {
    var movieDT = $("#movies").DataTable({
        ajax: {
            url: "api/Movies",
            dataSrc: ""
        },
        columns: [
            {
                data: "name"
            },
            {
                data: "genre.genreType"
            }

        ] 
    });

});