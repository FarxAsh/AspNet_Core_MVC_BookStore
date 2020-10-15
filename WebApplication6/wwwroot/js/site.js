let currentGenre = "";
let currentAuthor = "";
let currentBookCondition = "";

function GetFilteredBooks(filterArgs) {
    event.preventDefault();

    currentAuthor = filterArgs.author || currentAuthor;
    currentBookCondition = filterArgs.bookCondition || currentBookCondition;
    currentGenre = filterArgs.genre || currentGenre;
    const searchString = filterArgs.searchString || "";
    const roleAdmin = filterArgs.roleAdmin;
    const roleUser = filterArgs.roleUser;
    
    $.ajax({
        url: '/Books/GetAllBooksAjax',
        type: 'GET',
        data: { 'Genre': currentGenre, 'Author': currentAuthor, 'SearchString': searchString, 'BookCondition': currentBookCondition },
        dataType: 'json',
        success: function (resp) {
            $('#BookList').children().slice(1).remove();
             
            resp.forEach(book => {
                let card = '<div class="col-sm-3 m-0">' +
                                '<div class="card mb-4 shadow-sm" style="width:240px">' +
                                    '<img src="' + book.image + '" height="195">' +
                                    '<div class="card-body pt-0 pb-3 pl-3 pr-2 m-0">' +
                                        '<p class="card-text">' +
                                            '<dl>' +
                                                 '<dt>Title:</dt>' +
                                                 '<dd>'+ book.title +'</dd>' +
                                                 '<dt>Author:</dt>' +
                                                 '<dd>' + book.author.firstName +"   "+ book.author.lastName + '</dd>' +
                                            '</dl>' +
                                        '</p>' +
                                        '<div class="d-flex justify-content-between align-items-center">' +
                                            '<div class="btn-group">' +
                                                 '<a class="btn btn-sm btn-outline-dark" href ="/Books/Details?id=' + book.id + '">View</a>';

                if (roleUser == 'True') {
                    card += '<input type="button" class="btn btn-sm btn-outline-dark" onclick="AddCartItem('+book.id+');" value="Add to cart" />'; 
                }

                if (roleAdmin == 'True') {
                    card +=
                        '<div class="dropdown dropright">' +
                            '<button type="button" class="btn btn-sm btn-outline-dark dropdown-toggle" data-toggle="dropdown">' +
                                 'Action' +
                            '</button>' +
                            '<div class="dropdown-menu">' +
                                 "<a class='dropdown-item' href='/Books/Edit?id=" + book.id + "'>Edit</a>" +
                                 "<a class='dropdown-item' href='/Books/Delete?id=" + book.id + "'>Delete</a>" +
                            '</div>' +
                        '</div>';
                }              
                                 
                card +='</div >' +
                    '<text class="text-info">' + book.price + '$</text>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div>';
                $('#BookList').append(card);
            });

        }
    });
}



function GetFilteredAuthors(filterArgs) {

    event.preventDefault();
    const genre = filterArgs.genre || "";
    const sortOrder = filterArgs.sortOrder || "";
    const searchString = filterArgs.searchString || "";
    const role = filterArgs.role;
    $.ajax({
        url: '/Author/GetFilterAuthors',
        type: 'GET',
        data: { 'Genre': genre, 'SortOrder': sortOrder, 'SearchString': searchString },
        dataType: 'json',
        success: function (resp) {


            $("#List").children().slice(1).remove();
            resp.authors.forEach(author => {

                let card = "<div class='col-md-3'>" +
                             "<div class='card mb-4 shadow-sm' style='width: 250px'>" +
                                "<img src=' " + author.image + "' height='200'>" +
                                "<div class='card-body pt-0 pb-3 pl-3 pr-2 m-0'>" +
                                    "<p class='card-text'>" +
                                        " <dl>" +
                                            " <dt></dt> " +
                                            "<dd> " + author.firstName + " " + author.lastName + " </dd>" +
                                            "<dt></dt>" +
                                            "<dd>" + author.shortBiography + "</dd>" +
                                        "</dl>" +
                                    "</p>" +
                                    " <div class='d-flex justify-content-between align-items-center'> " +
                                        "<div class='btn-group'>" +
                                        "<a type='button' href='/Home/Details?id=" + author.id + "' class='btn btn-sm btn-outline-secondary'>View</a>";              

                if (role == 'True') {
                    card += "<div class='dropdown dropright'> " +
                                "<button type='button' class='btn btn-sm btn-outline-dark dropdown-toggle' data-toggle='dropdown'>" +
                                    "Action"+
                                "</button> " +
                            "<div class='dropdown-menu'>" +
                                 "<a class='dropdown-item' href='/Author/Edit?id=" + author.id + "'>Edit</a>" +
                                 "<a class='dropdown-item' href='/Author/Delete?id=" + author.id + "'>Delete</a>" +
                            "</div>" +
                        "</div>";
                }

                card += ("</div>" +
                    "</div>" +
                    "</div>" +
                    "</div >" +
                    "</div >");
                $('#List').append(card);
                   
            });
        }
    });
}

function AddCartItem(id) {
    $.ajax({
        url: '/Basket/AddBasketItem',
        type: 'GET',
        data: { 'id': id },

    });
}

function RemoveCartItem(id) {
    event.preventDefault();
    $.ajax({
        url: '/Basket/RemoveBasketItem',
        type: 'GET',
        data: { 'id': id },
        success: function () {
            $('#' + id).remove();
        }
    });
}