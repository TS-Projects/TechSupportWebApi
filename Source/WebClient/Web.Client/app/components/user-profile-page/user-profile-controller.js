(function () {
    'use strict';

    var userProfileController = function userProfileController(userProfileData, identity, user) {

        var vm = this;

        vm.user = user;
        vm.username = vm.user.username.toLowerCase();

            identity.getUser()
             .then(function (user) {
                 vm.isAdmin = user.isAdmin;
                 vm.currentlyLoggedUser = user;

                 if (user.userName.toLowerCase() === vm.username || user.isAdmin) {
                     //  userProfileData.getLikedProjects(vm.username)
                     console.log(user.userName.toLowerCase())
                      .then(function (data) {
                               console.log("zizi", data);
                               // vm.likedProjects = data;
                           });
            }
        });

    };

    angular
        .module('techSupportApp.controllers')
        .controller('UserProfileController', ['userProfileData', 'identity', 'user', userProfileController]);
}());