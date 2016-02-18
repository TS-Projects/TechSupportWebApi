(function () {
    'use strict';

    var UsersAdministrationPageData = function UsersAdministrationPageData(data) {
        function getAllRegistersUsers() {
            return data.getDataSource();
        }
        return {
            getAllRegistersUsers: getAllRegistersUsers
        };
    };

    angular.module('techSupportApp.data')
        .factory('usersAdministrationPageData', ['data', UsersAdministrationPageData]);
}());