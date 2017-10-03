$(document).ready(function () {
    var vm = {
        movieIds: []
    };

    var customers = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/api/customers?query=%QUERY',
            wildcard: '%QUERY'
        },
        limit: 10
    });

  
    $('#customer').typeahead({ minLength: 3, highlight: true }, {
        name: 'customers',
        display: 'name',
        source: customers
    }).on("typeahead:select", function (e, customer) {
        vm.customerId = customer.id;

    });


    var movies = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/api/movies?query=%QUERY',
            wildcard: '%QUERY'
        }
    });

    $('#movie').typeahead({minLength: 3, highlight: true},
        {
            name: "movies",
            display: "name",
            source: movies
        }).on("typeahead:select", function(e, movie) {
            $('#movies').append("<li class='list-group-item'>" + movie.name + "</li>");
            $('#movie').typeahead("val", "");
            vm.movieIds.push(movie.id);
        });

    $.validator.addMethod("validCustomer", function () {
        return vm.customerId && vm.customerId != 0;
    }, "Please select a valid customer.");

    $.validator.addMethod("validMovies", function () {
        return vm.movieIds.length > 0 && vm.movieIds.indexOf(0) == -1;
    }, "Please select at least one valid movie");

    var validator = $("#newRental").validate({
        submitHandler: function (ev) {

            $.post('/api/Rental/newRental', vm).done(function () {
                toastr.success("Rentals successfully recorded.");

                $('#customer').typeahead("val", "");
                $('#movie').typeahead('val', '');
                $('#movies').empty();

                vm = { movieIds: [] }

                validator.resetForm();
            }).fail(function () {
                toastr.error("Something unexpected happened");
            });

            return false;

        }
    });


});