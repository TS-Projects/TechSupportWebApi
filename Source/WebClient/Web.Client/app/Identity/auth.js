(function () {
    'use strict';

    var authService = function authService($http, $q, $cookies, identity, appSettings, errorHandler) {

        var userLoginUrlApi = appSettings.serverPath + '/api/users/login';
        var userRegisterUrlApi = appSettings.serverPath + '/api/Account/Register';

        var cookieStorageUserKey = 'currentApplicationUser';

        var login = function login(user) {
            var deferred = $q.defer();

            var data = "grant_type=password&username=" + (user.username || '') + '&password=' + (user.password || '');

            $http.post(userLoginUrlApi, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                .then(function (response) {
                    identity.setUser(response.data);
                    getIdentity().then(function () {
                        deferred.resolve(response);
                    });
                }, function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        };

        var getIdentity = function () {
            var deferred = $q.defer();
            identity.getUser()
                .then(function (identityResponse) {
                    identity.setUser(identityResponse);
                    deferred.resolve(identityResponse);
                }, function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        };

        var signUp = function (user) {
            var deferred = $q.defer();

            $http.post(userRegisterUrlApi, user)
                            .success(function () {
                                deferred.resolve();
                            }, function (response) {
                                deferred.reject(response);
                            })
                            .error(errorHandler);

            return deferred.promise;
        }

        return {
            login: login,
            signUp: signUp,
            getIdentity: getIdentity,
            isAuthenticated: function () {
                return !!$cookies.getObject(cookieStorageUserKey);
            },
            logout: function () {
                $cookies.remove(cookieStorageUserKey);
                $http.defaults.headers.common.Authorization = null;
                identity.removeUser();
            }
        };
    };

    angular
        .module('techSupportApp.services')
        .factory('auth', ['$http', '$q', '$cookies', 'identity', 'appSettings', 'errorHandler', authService]);
}());