(function () {
    'use strict';

    function authorization(identity, $cookies) {
        var TOKEN_KEY = 'currentApplicationUser';

        return {
            getAuthorizationHeader: function () {

                if (!identity.isAuthenticated()) {
                    return {};
                }

                return {
                    'Authorization': 'Bearer ' + $cookies.getObject(TOKEN_KEY)['access_token']
                }
            }
        }
    }

    angular.module('techSupportApp.data')
        .factory('authorization', ['identity','$cookies', authorization]);
}());