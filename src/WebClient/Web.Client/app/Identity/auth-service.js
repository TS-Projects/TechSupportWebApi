(function () {
    'use strict';

    var authService = function authService($http, $q, $cookies, appSettings) {

        var userLoginUrlApi = appSettings.serverPath + '/login';
        var userRegisterUrlApi = appSettings.serverPath + '/register';

        var TOKEN_KEY = 'authentication';

        var login = function login(user) {
            var deferred = $q.defer();

            var data = "grant_type=password&username=" + (user.username || '') + '&password=' + (user.password || '');

            $http.post(userLoginUrlApi, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                .then(function (response) {
                    var tokenValue = response.access_token;

                    var theBigDay = new Date();
                    theBigDay.setHours(theBigDay.getHours() + 72);

                    $cookies.put(TOKEN_KEY, tokenValue, { expires: theBigDay });

                    $http.defaults.headers.common.Authorization = 'Bearer ' + tokenValue;

                    deferred.resolve(response);
                    },
                function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        };

        var signUp = function (user) {
            var deferred = $q.defer();

            $http.post(userRegisterUrlApi + '/register', user)
                            .success(function () {
                                deferred.resolve();
                            }, function (response) {
                                deferred.reject(response);
                            })
                            .error(errorHandler.processError);

            return deferred.promise;
        }

        return {
            login: login,
            signUp: signUp,
            isAuthenticated: function () {
                return !!$cookies.get(TOKEN_KEY);
            }
        };
    };

    angular
        .module('techSupportApp.services')
        .factory('auth', ['$http', '$q', '$cookies', 'appSettings', authService]);
}());