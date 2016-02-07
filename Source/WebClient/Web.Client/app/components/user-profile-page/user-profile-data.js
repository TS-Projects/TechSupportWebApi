(function () {
    'use strict';

    var userProfileData = function userProfileData(data) {
        function getUser(username) {
            console.log("parametyra username:", username);
            return data.get('/api/users/profile/' + username + '/');
        }

        return {
            getUser: getUser
        };
    };

    angular
        .module('techSupportApp.data')
        .factory('userProfileData', ['data', userProfileData]);
}());