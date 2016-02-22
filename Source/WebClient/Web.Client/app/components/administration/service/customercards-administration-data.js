(function () {
    'use strict';

    var customerCardsAdministrationPageData = function customerCardsAdministrationPageData(appSettings, data, $cookies) {
        function getAllRegistersUsers() {
            var TOKEN_KEY = 'currentApplicationUser';
            var token = $cookies.getObject(TOKEN_KEY)['access_token'];

            var URL = appSettings.odataServerPath + '/CustomerCards';
            var auth = 'Bearer ' + token;
            var model = {
                id: "Id",
                fields: {
                    Id: { type: "string", editable: false },
                    FirstName: { type: "string" },
                    LastName: { type: "string" },
                    City: { type: "string" },
                    Phone: { type: "string" },
                    Informed : { type: "bool" },
                    Warranty: { type: "bool" },
                    CustomerCardPassword: { nullable: true, type: "string" },
                    Description: { type: "string" },
                    IsVisible: { type: "bool" }
                    //EnrollmentDate: { type: "string" }
                    //EndDate: { type: "string" }
                }
            };

            return data.genericOdataKendo(URL, auth, model);
        }
        return {
            getAllRegistersUsers: getAllRegistersUsers
        };
    };

    angular.module('techSupportApp.data')
        .factory('customerCardsAdministrationPageData', ['appSettings', 'data', '$cookies', customerCardsAdministrationPageData]);
}());