(function () {
    'use strict';

    var customersAdministrationPageData = function customersAdministrationPageData(data) {
        function getAllRegistersUsers() {
            return data.getDataSource();
        }
        return {
            getAllRegistersUsers: getAllRegistersUsers
        };
    };

    angular.module('techSupportApp.data')
        .factory('customersAdministrationPageData', ['data', customersAdministrationPageData]);
}());