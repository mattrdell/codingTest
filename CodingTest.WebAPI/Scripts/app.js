var ViewModel = function () {
    var self = this;
    self.items = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();
    self.authors = ko.observableArray();
    self.newItem = {
        ItemId: ko.observable(),
        Description: ko.observable(),
        Price: ko.observable(),
        Name: ko.observable()
    }

    var itemsUri = '/api/items/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllItems() {
        ajaxHelper(itemsUri, 'GET').done(function (data) {
            self.items(data);
        });
    }

    //self.getBookDetail = function (item) {
    //    ajaxHelper(itemsUri + item.Id, 'GET').done(function (data) {
    //        self.detail(data);
    //    });
    //}

    //self.addBook = function (formElement) {
    //    var book = {
    //        AuthorId: self.newBook.Author().Id,
    //        Genre: self.newBook.Genre(),
    //        Price: self.newBook.Price(),
    //        Title: self.newBook.Title(),
    //        Year: self.newBook.Year()
    //    };

    //    ajaxHelper(booksUri, 'POST', book).done(function (item) {
    //        self.books.push(item);
    //    });
    //}

    // Fetch the initial data.
    getAllItems();
};

ko.applyBindings(new ViewModel());