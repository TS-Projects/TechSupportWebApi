//angular.module("techSupportApp.controllers")
//        .controller("UsersAdministrationController", ['$scope', 'appSettings', function ($scope, appSettings) {
//            // var usersApi = baseServiceUrl + '/odata/Users';

//            var vm = this;

//            var usersApi = appSettings.odataServerPath + '/Users';

//            //$scope.mainGridOptions = {
//            //    dataSource: {
//            //        type: "odata-v4",
//            //        transport: {
//            //            read: {
//            //                url: usersApi
//            //            },
//            //            update: {
//            //                url: function (data) {
//            //                    return usersApi + "(" + data.Id + ")";
//            //                }
//            //}
//            //        },
//            //        schema: {
//            //            model: {
//            //                Id: "Id",
//            //                fields: {
//            //                    Id: { editable: true, type: "string" },
//            //                    FirstName: { type: "string" },
//            //                    LastName: { type: "string" },
//            //                    Address: { type: "string" }
//            //                }
//            //            }
//            //        },
//            //        pageSize: 20,
//            //        serverPaging: true,
//            //        serverSorting: true
//            //    },
//            //    height: 550,
//            //    filterable: {
//            //        mode: "row"
//            //    },
//            //        pageable: {
//            //            refresh: true,
//            //            pageSizes: true,
//            //            buttonCount: 5
//            //        },
//            //        columns: [
//            //    { field: "FirstName", width: "200px", title: "First Name" },
//            //    { field: "Id" },
//            //    { field: "LastName", width: "200px", title: "Last Name"},
//            //    { field: "Address", width: "200px", title: "Address" },
//            //    { command: ["edit"], title: "&nbsp;", width: "250px" }],
//            //    // { command: ["edit", "destroy"], title: "&nbsp;", width: "250px" }]
//            //    editable: "inline"
                
//            //};


//            //$scope.mainGridOptions = {
//            //    dataSource: {
//            //        type: "odata-v4",
//            //        transport: {
//            //            read: {
//            //                url: "http://localhost:27714/odata/Products"
//            //            },
//            //            update: {
//            //                url: function (data) {
//            //                    return "http://localhost:27714/odata/Products(" + data.ProductID + ")";
//            //                }
//            //            }
//            //        },
//            //        schema: {
//            //            model: {
//            //                id: "ProductID",
//            //                fields: {
//            //                    UnitPrice: { type: "number" },
//            //                    SupplierID: { type: "number" },
//            //                    UnitsInStock: { type: "number" },
//            //                    UnitsOnOrder: { type: "number" },
//            //                    ProductID: { type: "number" },
//            //                    Discontinued: { type: "boolean" }
//            //                }
//            //            }

//            //        },
//            //        pageSize: 5,
//            //        serverPaging: true,
//            //        serverSorting: true
//            //    },
//            //    sortable: true,
//            //    pageable: {
//            //        refresh: true,
//            //        pageSizes: true,
//            //        buttonCount: 5
//            //    },
//            //    columns: [{
//            //        field: "ProductName",
//            //        width: 300
//            //    }, {
//            //        field: "ProductID"
//            //    },
//            //    "UnitPrice", "UnitsOnOrder", "Discontinued", { command: "edit" }
//            //    ],
//            //    editable: "inline"
//            //};




//            vm.mainGridOptions = {
//                dataSource: {
//                    type: "odata-v4",
//                    transport: {
//                        read: {
//                            url: usersApi
//                        },
//                        update: {
//                            url: function (data) {
//                                console.log(data);
//                                return usersApi + "('" + data.Id + "')";
//                            }
//                        },
//                        create: {
//                            url: usersApi
//                        }
//                    },
//                    schema: {
//                        model: {
//                            id: "Id",
//                            fields: {
//                                Id: { type: "string" },
//                                FirstName: { type: "string" },
//                                LastName: { type: "string" },
//                                Address: { type: "string" }
//                            }
//                        }
//                    },
//                    pageSize: 5,
//                    serverPaging: true,
//                    serverSorting: true
//                },
//                toolbar: ["create"],
//                sortable: true,
//                pageable: {
//                    refresh: true,
//                    pageSizes: true,
//                    buttonCount: 5
//                },
//                columns: [{
//                    field: "Id"
//                },
//                "FirstName", "LastName", "Address", { command: "edit" }
//                ],
//                editable: "inline"
//            };
//        }]);



(function () {
    'use strict';

    var usersAdministrationController = function usersAdministrationController($scope, allUsers, administrationPageData, $translate) {
        //var vm = this;

        //  var usersApi = appSettings.odataServerPath + '/Users';


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
                    $scope.$apply(function() {

                        $translate.use($scope.lang); //change angular-translate language
                        kendo.culture($scope.lang); //change kendo culture

                        /* we use dummy language option in order to force the Grid to rebind */
                        $scope.mainGridOptions.language = $scope.lang;

                    });
                });
            }
        }

        $scope.mainGridOptions = {
            dataSource: allUsers,
            toolbar: ["create"],
            sortable: true,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            language: "english",
            columns: [
                   { field: "Id", title: "ID" },
                   { field: "UserName", title: "Потребителско име" },
                   { field: "Email", title: "Имейл" },
                   { field: "FirstName", title: "Име" },
                   { field: "LastName", title: "Фамилия" },
                   { field: "City", title: "Град" },
                   { field: "Phone", title: "Мобилен номер" },
                   { field: "About", title: "За нас" },
                   { command: "edit" }
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
        .controller('UsersAdministrationController', ['$scope', 'allUsers', 'administrationPageData', '$translate', usersAdministrationController]);
}());