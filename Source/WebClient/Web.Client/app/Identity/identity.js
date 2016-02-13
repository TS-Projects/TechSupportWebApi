(function () {
    'use strict';

    var identityService = function identityService($q, $cookies) {
        var currentUser = {};
        var cookieStorageUserKey = 'currentApplicationUser';
        var deferred = $q.defer();

        return {
            getUser: function () {
                var savedUser = $cookies.getObject(cookieStorageUserKey);
                if (savedUser) {
                    return $q.resolve(savedUser);
                }
                return deferred.promise;
            },
            isAuthenticated: function () {
                //return !!this.getUser();
                return Object.getOwnPropertyNames(currentUser).length !== 0;
            },
            setUser: function (user) {
                if (user) {
                    $cookies.putObject(cookieStorageUserKey, user);
                }
                else {
                    $cookies.remove(cookieStorageUserKey);
                }

                currentUser = user;
                deferred.resolve(user);
            },
            removeUser: function () {
                currentUser = {};
                deferred = $q.defer();
            }
        };
    };

    angular
        .module('techSupportApp.services')
        .factory('identity', ['$q', '$cookies', identityService]);
}());