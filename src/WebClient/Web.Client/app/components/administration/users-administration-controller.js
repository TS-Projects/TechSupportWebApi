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

    var usersAdministrationController = function usersAdministrationController($scope, appSettings) {
        var vm = this;

        var usersApi = appSettings.odataServerPath + '/Users';

        $scope.mainGridOptions = {
            dataSource: {
                type: "odata-v4",
                transport: {
                    read: {
                        url: usersApi
                    },
                    update: {
                        url: function (data) {
                            console.log(data);
                            return usersApi + "('" + data.Id + "')";
                        }
                    },
                    create: {
                        url: usersApi
                    }
                },
                schema: {
                    model: {
                        id: "Id",
                        fields: {
                            Id: { type: "string" },
                            FirstName: { type: "string" },
                            LastName: { type: "string" },
                            Address: { type: "string" }
                        }
                    }
                },
                pageSize: 5,
                serverPaging: true,
                serverSorting: true
            },
            toolbar: ["create"],
            sortable: true,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            columns: [{
                field: "Id"
            },
            "FirstName", "LastName", "Address", { command: "edit" }
            ],
            editable: "inline"
        };
    };

    angular
        .module('techSupportApp.controllers')
        .controller('UsersAdministrationController', ['$scope','appSettings', usersAdministrationController]);
}());