(function () {
    'use strict';

    var productsPageData = function productsPageData(data) {
        function getAllProducts() {
            var student = { name: 'Ivaylo', age: 1}
            //return data.get('/projects');

            return student;
        }

        return {
            getAllProducts: getAllProducts
        };
    };

    angular.module('techSupportApp.data')
        .factory('productsPageData', ['data', productsPageData]);
}());