(function () {
    'use strict';

    var baseData = function baseData($http, $q, appSettings, notifier, identity, $cookies, authorization) {
        //var headers = {
        //    'Content-Type': 'application/json'
        //},
        //    authorizationErrorMessage = 'You must be logged in to do that';

        function get(url, queryParams) {
            var defered = $q.defer();

            var URL = appSettings.serverPath + url;
            var authHeader = authorization.getAuthorizationHeader();

            $http.get(URL, { params: queryParams, headers: authHeader })
                .then(function (response) {
                    defered.resolve(response.data);
                }, function (error) {
                    error = getErrorMessage(error);
                    notifier.error(error);
                    defered.reject(error);
                });

            return defered.promise;
        }

        function post(url, postData) {
            var defered = $q.defer();

            var URL = appSettings.serverPath + url;
            var authHeader = authorization.getAuthorizationHeader();

            $http.post(URL, postData, { headers: authHeader })
                .then(function (response) {
                    defered.resolve(response.data);
                }, function (error) {
                    error = getErrorMessage(error);
                    notifier.error(error);
                    defered.reject(error);
                });

            return defered.promise;
        }

        function put() {
            throw new Error('Not implemented!');
        }

        function getErrorMessage(response) {
            var error = response.data.modelState;
            if (error && error[Object.keys(error)[0]][0]) {
                error = error[Object.keys(error)[0]][0];
            }
            else {
                error = response.data.message;
            }

            return error;
        }

        //function get(url, authorize) {
        //    var deferred = $q.defer();
        //    if (authorize && !identity.isAuthenticated()) {
        //        notifier.error(authorizationErrorMessage);
        //        deferred.reject();
        //    }
        //    else {
        //        var URL = appSettings.serverPath + url;

        //        $http.get(URL)
        //            .then(function (data) {
        //                deferred.resolve(data);
        //            },
        //            function (err) {
        //                deferred.reject(err);
        //            });
        //    }

        //    return deferred.promise;
        //}

        //function getOData(url, authorize) {
        //    var deferred = $q.defer();
        //    var URL = appSettings.odataServerPath + url;

        //    $http.get(URL)
        //        .then(function (data) {
        //            deferred.resolve(data);
        //        },
        //        function (err) {
        //            deferred.reject(err);
        //        });

        //    return deferred.promise;
        //}

        //function post(url, data, authorize) {
        //    var deferred = $q.defer();

        //    if (authorize && !identity.isAuthenticated()) {
        //        notifier.error(authorizationErrorMessage);
        //        deferred.reject();
        //    }
        //    else {
        //        var URL = appSettings.serverPath + url;

        //        $http.post(URL, data, headers)
        //            .then(function (data) {
        //                deferred.resolve(data);
        //            },
        //            function (err) {
        //                deferred.reject(err);
        //            });
        //    }

        //    return deferred.promise;
        //}

        //function GenericGetDataSource(URL, auth, model) {
        //    var URL = appSettings.odataServerPath + '/Users';
        //    var TOKEN_KEY = 'currentApplicationUser';
        //    var token = $cookies.getObject(TOKEN_KEY)['access_token'];

        //    var dataSource = {
        //        type: "odata-v4",
        //        transport: {
        //            read: {
        //                url: URL,   //URL
        //                beforeSend: function (xhr) {
        //                    var auth = 'Bearer ' + token;
        //                    xhr.setRequestHeader('Authorization', auth);  //auth
        //                }

        //            },
        //            update: {
        //                url: function (data) {
        //                    return URL + "('" + data.Id + "')";
        //                }
        //            },
        //            create: {
        //                url: URL
        //            }
        //        },
        //        schema: {
        //            model: {                                //model
        //                id: "Id",
        //                fields: {
        //                    Id: { type: "string" },
        //                    UserName: { type: "string" },
        //                    Email: { type: "string" },
        //                    FirstName: { type: "string" },
        //                    LastName: { type: "string" },
        //                    City: { type: "string" },
        //                    Phone: { type: "string" },
        //                    About: { type: "string" }
        //                }
        //            }
        //        },
        //        pageSize: 5,
        //        serverPaging: true,
        //        serverSorting: true
        //    }

        //    return dataSource;
        //}


        function getDataSource() {
            var URL = appSettings.odataServerPath + '/Users';
            var TOKEN_KEY = 'currentApplicationUser';
            var token = $cookies.getObject(TOKEN_KEY)['access_token'];

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
                    },
                    destroy: {
                        url: function (data) {
                            return URL + "('" + data.Id + "')";
                        }
                    }
                },
                schema: {
                    model: {
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
            post: post,
            getDataSource: getDataSource
        };
    };

    angular
        .module('techSupportApp.data')
        .factory('data', ['$http', '$q', 'appSettings', 'notifier', 'identity', '$cookies', 'authorization', baseData]);
}());