(function () {
    'use strict';

    var authService = function authService($http, $q, $cookies, identity, appSettings, errorHandler) {

        var userLoginUrlApi = appSettings.serverPath + '/api/users/login';
        var userIdentityUrlApi = appSettings.serverPath + '/api/users/identity';
        var userRegisterUrlApi = appSettings.serverPath + '/api/Account/Register';
        
        var TOKEN_KEY = 'authentication';

        var login = function login(user) {
            var deferred = $q.defer();

            var data = "grant_type=password&username=" + (user.username || '') + '&password=' + (user.password || '');

            $http.post(userLoginUrlApi, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                .then(function (response) {
                    var tokenValue = response.data.access_token;

                    var theBigDay = new Date();
                    theBigDay.setHours(theBigDay.getHours() + 72);

                    $cookies.put(TOKEN_KEY, tokenValue, { expires: theBigDay });
                    console.log("tokenValue:", tokenValue);
                    $http.defaults.headers.common.Authorization = 'Bearer ' + tokenValue;
                    console.log("$http.defaults.headers:", $http.defaults.headers);
                    console.log("$http.defaults.headers.common.Authorization:", $http.defaults.headers.common.Authorization);
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

            $http.get(userIdentityUrlApi)
                .then(function (identityResponse) {
                    console.log("identityResponse: ", identityResponse);
                    identity.setUser(identityResponse.data);
                    deferred.resolve(identityResponse.data);
                }, function (err) {
                    console.log("err: ", err);
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
                return !!$cookies.get(TOKEN_KEY);
            },
            logout: function () {
                $cookies.remove(TOKEN_KEY);
                $http.defaults.headers.common.Authorization = null;
                identity.removeUser();
            }
        };
    };

    angular
        .module('techSupportApp.services')
        .factory('auth', ['$http', '$q', '$cookies', 'identity', 'appSettings', 'errorHandler', authService]);
}());