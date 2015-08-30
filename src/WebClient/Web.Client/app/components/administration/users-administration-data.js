(function () {
    'use strict';

    var administrationPageData = function administrationPageData(data) {
        function getAllRegistersUsers() {
            return data.getDataSource();
        }

        //var getAllRegistersUsers = data.getDataSource();

        return {
            getAllRegistersUsers: getAllRegistersUsers
        };
    };

    angular.module('techSupportApp.data')
        .factory('administrationPageData', ['data', administrationPageData]);
}());