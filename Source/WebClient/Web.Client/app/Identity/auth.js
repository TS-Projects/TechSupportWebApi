(function () {
    'use strict';

    var authService = function authService($http, $q, $cookies, identity, appSettings, authorization, errorHandler) {

        var userLoginUrlApi = appSettings.serverPath + '/api/users/login';
        var userIdentityUrlApi = appSettings.serverPath + '/api/users/identity';
        var userRegisterUrlApi = appSettings.serverPath + '/api/Account/Register';
        var userLogoutUrlApi = appSettings.serverPath + '/api/Account/Logout';

        var login = function login(user) {
            var deferred = $q.defer();

            var data = "grant_type=password&username=" + (user.username || '') + '&password=' + (user.password || '');

            $http.post(userLoginUrlApi, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                .then(function (response) {
                    if (response.data["access_token"]) {
                        identity.setCurrentUser(response.data);

                        var allRoles = response.data.roles;

                        var role = allRoles.split(',')[0];

                        console.log("isAdmin role: ", role);

                        deferred.resolve(true);
                    }
                    else {
                        deferred.resolve(false);
                    }
                });

            return deferred.promise;
        };
        var signUp = function(user) {
            var deferred = $q.defer();

            $http.post(userRegisterUrlApi, user)
                .success(function() {
                    deferred.resolve();
                }, function(response) {
                    deferred.reject(response);
                })
                .error(errorHandler);

            return deferred.promise;
        };
        var logout = function() {
            var deferred = $q.defer();

            var headers = authorization.getAuthorizationHeader();
            console.log("logout header: ", headers);
            $http.post(userLogoutUrlApi, {}, { headers: headers })
                .then(function() {
                    identity.setCurrentUser(undefined);
                    deferred.resolve();
                });

            return deferred.promise;
        };
        var isAuthenticated = function() {
            if (identity.isAuthenticated()) {
                return true;
            } else {
                return $q.reject('not authorized');
            }
        };

        return {
            login: login,
            signUp: signUp,
            logout: logout,
            isAuthenticated: isAuthenticated
        };
    };

    angular
        .module('techSupportApp.services')
        .factory('auth', ['$http', '$q', '$cookies', 'identity', 'appSettings', 'authorization', 'errorHandler', authService]);
}());