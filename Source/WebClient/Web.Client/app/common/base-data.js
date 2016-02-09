(function () {
    'use strict';

    var baseData = function baseData($http, $q, appSettings, notifier, $cookies) {
        var headers = {
            'Content-Type': 'application/json'
        },
            authorizationErrorMessage = 'You must be logged in to do that';

        function get(url, authorize) {
            var deferred = $q.defer();
            if (authorize && !identity.isAuthenticated()) {
                notifier.error(authorizationErrorMessage);
                deferred.reject();
            }
            else {
                var URL = appSettings.serverPath + url;

                $http.get(URL)
                    .then(function (data) {
                        deferred.resolve(data);
                    },
                    function (err) {
                        deferred.reject(err);
                    });
            }

            return deferred.promise;
        }

        function getOData(url, authorize) {
            var deferred = $q.defer();
            var URL = appSettings.odataServerPath + url;

            $http.get(URL)
                .then(function (data) {
                    deferred.resolve(data);
                },
                function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function post(url, data, authorize) {
            var deferred = $q.defer();

            if (authorize && !identity.isAuthenticated()) {
                notifier.error(authorizationErrorMessage);
                deferred.reject();
            }
            else {
                var URL = appSettings.serverPath + url;

                $http.post(URL, data, headers)
                    .then(function (data) {
                        deferred.resolve(data);
                    },
                    function (err) {
                        deferred.reject(err);
                    });
            }

            return deferred.promise;
        }

        function getDataSource() {
            var URL = appSettings.odataServerPath + '/Customers';
            var TOKEN_KEY = 'authentication';
            var token = $cookies.get(TOKEN_KEY);

            var dataSource = {
                type: "odata-v4",
                transport: {
                    read: {
                        url: URL,
                        beforeSend: function (xhr) {
                            var auth = 'Bearer ' + token;
                            xhr.setRequestHeader('Authorization', auth);
                        }

                    },
                    update: {
                        url: function (data) {
                            return URL + "('" + data.Id + "')";
                        }
                    },
                    create: {
                        url: URL
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
            }

            return dataSource;
        }

        return {
            get: get,
            getOData: getOData,
            post: post,
            getDataSource: getDataSource
        };
    };

    angular
        .module('techSupportApp.data')
        .factory('data', ['$http', '$q', 'appSettings', 'notifier', '$cookies', baseData]);
}());