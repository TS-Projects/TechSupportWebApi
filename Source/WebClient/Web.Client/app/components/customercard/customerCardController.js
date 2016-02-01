 angular.module("CustomerCardCtrl", [])
        .controller("CustomerCardController", ['$scope', 'baseServiceUrl', function ($scope, baseServiceUrl) {
            var usersApi = baseServiceUrl + '/odata/CustomerCard';

            $scope.customerGridOptions = {
                dataSource: {
                    type: "odata-v4",
                    transport: {
                        read: {
                            url: usersApi
                        },
                        update: {
                            url: function (data) {
                                return usersApi + "(" + data.Id + ")";
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
                                Id: { type: "int" },
                                Description: { type: "string" },
                                Comment: { type: "string" },
                                Summary: { type: "string" }
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
                "Description", "Comment", "Summary", { command: "edit" }
                ],
                editable: "inline"
            };
        }]);
