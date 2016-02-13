(function () {
    'use strict';

    function authorization(identity) {
        return {
            getAuthorizationHeader: function () {

                if (!identity.isAuthenticated()) {
                    return {};
                }

                return {
                    'Authorization': 'Bearer ' + identity.getUser()['access_token']
                }
            }
        }
    }

    angular.module('techSupportApp.data')
        .factory('authorization', ['identity', authorization]);
}());