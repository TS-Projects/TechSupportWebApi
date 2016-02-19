(function () {
    'use strict';

    var customersAdministrationPageData = function customersAdministrationPageData(appSettings, data, $cookies) {
        function getAllRegistersUsers() {
            var TOKEN_KEY = 'currentApplicationUser';
            var token = $cookies.getObject(TOKEN_KEY)['access_token'];

            var URL = appSettings.odataServerPath + '/Users';
            var auth = 'Bearer ' + token;
            var model = {
                id: "Id",
                fields: {
                    Id: { type: "string" },
                    UserName: { type: "string" },
                    Email: { type: "string" },
                    FirstName: { type: "string" },
                    LastName: { type: "string" },
                    City: { type: "string" },
                    Phone: { type: "string" },
                    About: { type: "string" }
                }
            };

            return data.genericOdataKendo(URL, auth, model);
        }
        return {
            getAllRegistersUsers: getAllRegistersUsers
        };
    };

    angular.module('techSupportApp.data')
        .factory('customersAdministrationPageData', ['appSettings', 'data', '$cookies', customersAdministrationPageData]);
}());