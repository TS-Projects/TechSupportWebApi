(function () {
    'use strict';

    var customerCardsAdministrationController = function customerCardsAdministrationController($scope, allCustomerCards, customerCardsAdministrationPageData, $translate) {
        var vm = this;

        $scope.lang = "en-US";

        $scope.dropDownOptions = {
            dataValueField: "value",
            dataTextField: "text",
            dataSource: {
                data: [{ value: "en-US", text: "English" }, { value: "bg-BG", text: "Bulgarian" }]
            },
            change: function () {

                /* The kendo.culture.xx-XX.js files can be pre-loaded in the <head> section of the document,
                        but the kendo.messages.xx-XX.js file should be loaded on demand when the language is about to change */

                /* We are using the jQuery.getScript method to load the messages file 
                        and use the callback function to change the kendo culture, kendo messages and angular-translate language */

                $.getScript("http://cdn.kendostatic.com/2014.2.903/js/messages/kendo.messages." + $scope.lang + ".min.js", function () {

                    /* $scope.$apply should be used in order to notify the $scope for language change */
                    $scope.$apply(function () {

                        $translate.use($scope.lang); //change angular-translate language
                        kendo.culture($scope.lang); //change kendo culture

                        /* we use dummy language option in order to force the Grid to rebind */
                        $scope.mainGridOptions.language = $scope.lang;

                    });
                });
            }
        }

        $scope.mainGridOptions = {
            dataSource: allCustomerCards,
            toolbar: ["create"],
            sortable: true,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            language: "english",
            columns: [
                { field: "Id", title: "ID", width: "110px" },
                { field: "FirstName", title: "Име" },
                { field: "LastName", title: "Фамилия" },
                { field: "City", title: "Град" },
                { field: "Phone", title: "Мобилен номер" },
                { field: "Informed", title: "Уведомен" },
                { field: "Warranty", title: "Гаранция" },
                { field: "CustomerCardPassword", title: "Парола" },
                { field: "Description", title: "Описание" },
                { field: "IsVisible", title: "Видима" },
                { command: ["edit", "destroy"], title: "&nbsp;", width: "180px" }
        //        Id: { type: "string" },
        //FirstName: { type: "string" },
        //LastName: { type: "string" },
        //City: { type: "string" },
        //Phone: { type: "string" },
        //Informed : { type: "bool" },
        //Warranty: { type: "bool" },
        //CustomerCardPassword: { type: "string" },
        //Description: { type: "string" },
        //IsVisible: { type: "bool" }
            ],
            //columns: [{
            //    field: "Id"
            //},
            //"UserName", "Email", "FirstName", "LastName", "City", "Phone", "About", { command: "edit" }
            //],
            editable: "inline"
        };
    };

    angular
        .module('techSupportApp.controllers')
        .controller('CustomerCardsAdministrationController', ['$scope', 'allCustomerCards', 'customerCardsAdministrationPageData', '$translate', customerCardsAdministrationController]);
}());