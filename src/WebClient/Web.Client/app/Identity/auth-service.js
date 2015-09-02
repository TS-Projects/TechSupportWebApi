(function () {
    'use strict';

    var authService = function authService($http, $q, $cookies, identity, appSettings) {

        var userLoginUrlApi = appSettings.serverPath + '/users/login';
        var userIdentityUrlApi = appSettings.serverPath + '/login';
        var userRegisterUrlApi = appSettings.serverPath + '/register';
        ////'http://localhost:13078/api/users/login'
        

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

                   // var sss = $cookies.put(TOKEN_KEY, tokenValue, { expires: theBigDay });
                    var favoriteCookie = $cookies.get(TOKEN_KEY);
                    console.log('$cookies.put:', favoriteCookie);
                    
                    $http.defaults.headers.common.Authorization = 'Bearer ' + tokenValue;

                    getIdentity().then(function () {
                        deferred.resolve(response);
                    });
                },
                function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        };

        var getIdentity = function () {
            var deferred = $q.defer();

            $http.get(userIdentityUrlApi)
                .then(function (identityResponse) {
                    identity.setUser(identityResponse);
                    deferred.resolve(identityResponse);
                }, function(err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        };

        //var signUp = function (user) {
        //    var deferred = $q.defer();

        //    $http.post(userRegisterUrlApi + '/register', user)
        //                    .success(function () {
        //                        deferred.resolve();
        //                    }, function (response) {
        //                        deferred.reject(response);
        //                    })
        //                    .error(errorHandler.processError);

        //    return deferred.promise;
        //}

        return {
            login: login,
            //signUp: signUp,
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
        .factory('auth', ['$http', '$q', '$cookies', 'identity', 'appSettings', authService]);
}());