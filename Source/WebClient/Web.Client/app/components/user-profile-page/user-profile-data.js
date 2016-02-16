(function () {
    'use strict';

    var userProfileData = function userProfileData(data) {

        var Profile_URL = '/api/users/profile/';

        function getProfile(username) {
            return data.get(Profile_URL + username + '/');
        }

        //function getProfileData(username) {
        //    return data.get('/api/users/profiledata/' + username + '/');
        //}

        function postProfileData(profile) {
            return data.post(Profile_URL, profile);
        }

        return {
            getProfile: getProfile,
            postProfileData: postProfileData
        };
    };

    angular
        .module('techSupportApp.data')
        .factory('userProfileData', ['data', userProfileData]);
}());