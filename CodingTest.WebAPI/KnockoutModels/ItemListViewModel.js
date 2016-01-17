/// <reference path="../Scripts/knockout-3.1.0.js" />
/// <reference path="../Scripts/jquery-1.10.2.js" />

var ItemListViewModel;

function Item(itemId, name, description, price, qtyInStock) {
    var self = this;
    self.ItemId = ko.observable(itemId);
    self.Name = ko.observable(name);
    self.Description = ko.observable(description);
    self.Price = ko.observable(price);
    self.QtyInStock = ko.observable(qtyInStock);
}

function ItemList() {
    var self = this;
    self.items = ko.observableArray([]);

    self.getItems = function () {
        self.items.removeAll();

        $.getJSON('/api/item', function (data) {
            $.each(data, function (key, value) {
                self.items.push(new Item(value.ItemId, value.Description, value.Name, value.Price, value.QtyInStock));
            });
        });
    };

    self.purchaseItem = function (item) 
    {
        $.ajax({
            url: '/api/item/' + item.ItemId(),
            type: 'PurchaseItem',
            contentType: 'application/json',
            success: function () {
                item.QtyInStock -= 1;
            }
        });
    };
}

// on document ready
$(document).ready(function () {
    // bind view model to referring view
    ko.applyBindings(ItemListViewModel);

    // load student data
    ItemListViewModel.getItems();
});