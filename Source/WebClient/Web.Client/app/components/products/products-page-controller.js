/// <reference path="products-page-view.html" />
(function () {
    'use strict';

    var productsPageController = function productsPageController(productsPageData, allProducts) {
        var vm = this;

       // vm.productsPageData = productsPageData.getAllProducts();
        vm.allProducts = allProducts;
        console.log('allProducts', vm.allProducts);
    };

    angular
        .module('techSupportApp.controllers')
        .controller('ProductsPageController', ['productsPageData', 'allProducts', productsPageController]);
}());