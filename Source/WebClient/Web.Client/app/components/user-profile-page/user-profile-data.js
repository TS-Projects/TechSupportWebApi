(function () {
    'use strict';

    var userProfileData = function userProfileData(data) {
        function getProfile(username) {
            return data.get('/api/users/profile/' + username + '/');
        }

        function getProfileData(username) {
            return data.get('/api/users/profiledata/' + username + '/');
        }

        function postProfileData(profile) {
            return data.post(
                )
        }

        return {
            getProfile: getProfile,
            getProfileData: getProfileData
        };
    };

    angular
        .module('techSupportApp.data')
        .factory('userProfileData', ['data', userProfileData]);
}());