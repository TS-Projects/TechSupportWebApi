(function () {
    'use strict';

    var userProfileController = function userProfileController(userProfileData, identity, user) {

        var vm = this;

        vm.user = user;
        vm.username = vm.user.data.userName.toLowerCase();

        //    identity.getUser()
        //     .then(function (user) {
        //         vm.isAdmin = user.isAdmin;
        //         vm.currentlyLoggedUser = user;

        //         if (user.data.userName.toLowerCase() === vm.username || user.isAdmin) {
        //             //  userProfileData.getLikedProjects(vm.username)
        //             console.log("bizi: ", user)
        //              .then(function (data) {
        //                       console.log("zizi", data);
        //                       // vm.likedProjects = data;
        //                   });
        //    }
        //});

    };

    angular
        .module('techSupportApp.controllers')
        .controller('UserProfileController', ['userProfileData', 'identity','user', userProfileController]);
}());