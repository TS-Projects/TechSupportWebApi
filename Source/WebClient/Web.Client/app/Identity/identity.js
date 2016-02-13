(function () {
    'use strict';

    var identityService = function identityService($q, $cookieStore) {
        var currentUser = {};
        var cookieStorageUserKey = 'currentApplicationUser';
        var deferred = $q.defer();

        return {
            getUser: function () {
                var savedUser = $cookieStore.get(cookieStorageUserKey);
                if (savedUser) {
                    return $q.resolve(savedUser);
                }
                return deferred.promise;
            },
            isAuthenticated: function () {
                return !!this.getUser();
            },
            setUser: function (user) {
                if (user) {
                    $cookieStore.put(cookieStorageUserKey, user);
                }
                else {
                    $cookieStore.remove(cookieStorageUserKey);
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
        .factory('identity', ['$q', '$cookieStore', identityService]);
}());