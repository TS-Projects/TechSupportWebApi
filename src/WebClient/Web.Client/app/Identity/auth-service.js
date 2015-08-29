(function () {
    'use strict';

    var authService = function authService($http, $q, $cookies, identity, appSettings) {

        var userLoginUrlApi = appSettings + '/login';
        var userRegisterUrlApi = appSettings + '/register';

        var TOKEN_KEY = 'authentication';

        var login = function login(user) {
            var deferred = $q.defer();

            var data = "grant_type=password&username=" + (user.username || '') + '&password=' + (user.password || '');

            $http.post(userLoginUrlApi, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                .success(function (response) {
                    var tokenValue = response.access_token;

                    var theBigDay = new Date();
                    theBigDay.setHours(theBigDay.getHours() + 72);

                    $cookies.put(TOKEN_KEY, tokenValue, { expires: theBigDay });

                    $http.defaults.headers.common.Authorization = 'Bearer ' + tokenValue;

                    deferred.resolve(response);
                })
                .error(function (err) {
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













//'use strict';

//angular.module('AuthService', []).factory('auth', ['$http', '$q', 'identity', 'authorization', 'errorHandler', 'baseServiceUrl', function ($http, $q, identity, authorization, errorHandler, baseServiceUrl) {
//    var usersApi = baseServiceUrl + '/api/Account';

//    return {
//        signup: function(user) {
//            var deferred = $q.defer();

//            $http.post(usersApi + '/register', user)
//                .success(function() {
//                    deferred.resolve();
//                }, function(response) {
//                    deferred.reject(response);
//                })
//                .error(errorHandler.processError);

//            return deferred.promise;
//        },
//        login: function(user){
//            var deferred = $q.defer();
//            user['grant_type'] = 'password';
//            $http.post(usersApi + '/login', 'username=' + user.username + '&password=' + user.password + '&grant_type=password', { headers: {'Content-Type': 'application/x-www-form-urlencoded'} })
//                .success(function(response) {
//                    if (response['access_token']) {
//                        identity.setCurrentUser(response);
//                        authorization.setAuthorizationHeader(response['access_token']);
//                        deferred.resolve(true);
//                    }
//                    else {
//                        deferred.resolve(false);
//                    }
//                });

//            return deferred.promise;
//        },
//        logout: function() {
//            var deferred = $q.defer();

//            var headers = authorization.getAuthorizationHeader();
//            $http.post(usersApi + '/logout', {}, { headers: headers })
//                .success(function() {
//                    identity.setCurrentUser(undefined);
//                    authorization.removeAuthorizationHeader();
//                    deferred.resolve();
//                });

//            return deferred.promise;
//        },
//        userInfo: function() {
//            var deferred = $q.defer();

//            var userInfo = identity.getCurrentUser().userInfo;
//            if (userInfo) {
//                deferred.resolve(userInfo);
//            }
//            else {
//                var headers = authorization.getAuthorizationHeader();
//                $http.get(usersApi + '/userInfo', { headers: headers })
//                    .success(function(response) {
//                        var currentUser = identity.getCurrentUser();
//                        angular.extend(currentUser, { userInfo : response });
//                        identity.setCurrentUser(currentUser);
//                        deferred.resolve(response);
//                    });
//            }

//            return deferred.promise;
//        },
//        isAuthenticated: function() {
//            if (identity.isAuthenticated()) {
//                return true;
//            }
//            else {
//                return $q.reject('not authorized');
//            }
//        }
//    }
//}]);