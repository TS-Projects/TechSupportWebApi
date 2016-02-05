(function() {
    'use strict';

    var userProfileController = function userProfileController(identity, user, profile) {
        var vm = this;

        vm.user = user;
        console.log(user);
        vm.profile = profile;
    };

    angular
        .module('techSupportApp.controllers')
        .controller('UserProfileController', ['identity', 'user', 'profile', userProfileController]);
}());