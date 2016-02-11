(function () {
    'use strict';

    function identity($cookieStore) {
        var cookieStorageUserKey = 'currentApplicationUser';

        var currentUser;
        return {
            getCurrentUser: function () {
                var savedUser = $cookieStore.get(cookieStorageUserKey);
                console.log("savedUser before: ", savedUser);
                if (savedUser) {
                    console.log("savedUser after: ", savedUser);
                    return savedUser;
                }

                return currentUser;
            },
            setCurrentUser: function (user) {
                if (user) {
                    $cookieStore.put(cookieStorageUserKey, user);
                }
                else {
                    $cookieStore.remove(cookieStorageUserKey);
                }

                currentUser = user;
            },
            isAuthenticated: function () {
                return !!this.getCurrentUser();
            }
        }
    }

    angular
        .module('techSupportApp.services')
        .factory('identity', ['$cookieStore', identity]);
}());