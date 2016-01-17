var itemPurchasingViewModel;

function Item(itemId, name, description, price, qtyInStock) {
    var self = this;
    self.ItemId = ko.observable(itemId);
    self.Name = ko.observable(name);
    self.Description = ko.observable(description);
    self.Price = ko.observable(price);
    self.QtyInStock = ko.observable(qtyInStock);

    self.Purchase = function () {
        var tokenKey = 'accessToken';
        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        var dataObject = ko.toJSON(this);

        $.ajax({
            url: '/api/items',
            type: 'post',
            data: dataObject,
            headers: headers,
            contentType: 'application/json',
            success: itemPurchasingViewModel.itemListViewModel.getItems()
    });
    };
}

function ItemList() {
    var self = this;
    self.items = ko.observableArray([]);

    self.getItems = function () {
        self.items.removeAll();

        $.getJSON('/api/items', function (data) {
            $.each(data, function (key, value) {
                self.items.push(new Item(value.ItemId, value.Name, value.Description, value.Price, value.QtyInStock));
            });
        });
    };

    //self.purchaseItem = function (item) 
    //{
    //    $.ajax({
    //        url: '/api/items/' + item.ItemId(),
    //        type: 'Purchase',
    //        contentType: 'application/json',
    //        success: function () {
    //            item.QtyInStock -= 1;
    //        }
    //    });
    //};
}

function LoginViewModel() {
    var self = this;

    var tokenKey = 'accessToken';

    self.result = ko.observable();
    self.user = ko.observable();

    self.registerEmail = ko.observable();
    self.registerPassword = ko.observable();
    self.registerPassword2 = ko.observable();

    self.loginEmail = ko.observable();
    self.loginPassword = ko.observable();

    function showError(jqXHR) {
        self.result(jqXHR.status + ': ' + jqXHR.statusText);
    }

    self.callApi = function () {
        self.result('');

        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        $.ajax({
            type: 'GET',
            url: '/api/values',
            headers: headers
        }).done(function (data) {
            self.result(data);
        }).fail(showError);
    }

    self.register = function () {
        self.result('');

        var data = {
            Email: self.registerEmail(),
            Password: self.registerPassword(),
            ConfirmPassword: self.registerPassword2()
        };

        $.ajax({
            type: 'POST',
            url: '/api/Account/Register',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (data) {
            self.result("Done!");
        }).fail(showError);
    }

    self.login = function () {
        self.result('');

        var loginData = {
            grant_type: 'password',
            username: self.loginEmail(),
            password: self.loginPassword()
        };

        $.ajax({
            type: 'POST',
            url: '/Token',
            data: loginData
        }).done(function (data) {
            self.user(data.userName);
            // Cache the access token in session storage.
            sessionStorage.setItem(tokenKey, data.access_token);
        }).fail(showError);
    }

    self.logout = function () {
        self.user('');
        sessionStorage.removeItem(tokenKey)
    }
}

itemPurchasingViewModel = { itemViewModel: new Item(), itemListViewModel: new ItemList(), loginViewModel: new LoginViewModel() };
// on document ready
$(document).ready(function () {

    ko.applyBindings(itemPurchasingViewModel);
    itemPurchasingViewModel.itemListViewModel.getItems();
});