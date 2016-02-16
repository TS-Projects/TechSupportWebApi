﻿(function () {
    'use strict';

    var userProfileController = function userProfileController(userProfileData, identity, user, notifier) {

        var vm = this;

        vm.user = user;
        vm.username = vm.user.userName.toLowerCase();

        userProfileData.getProfile(vm.username)
            .then(function (response) {
                console.log('response: ', response);
                vm.profile = response;

                if (response.firstName && response.lastName) {
                    //vm.profile.firstName = response.firstName;
                    //vm.profile.lastName = response.firstName;
                    vm.fullname = response.firstName + " " + response.lastName;
                }





            });


        vm.submitProfileUpdate = function(newProfile) {
            userProfileData.postProfileData(newProfile)
                .then(function(success) {
                    console.log("ha success: ", success);
                    notifier.success('Update successful!');
                    //$location.path('/');
                }, function(error) {
                    notifier.error(error);
                });
        };

        //vm.submitChangePassword = function () {

        //}

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
        .controller('UserProfileController', ['userProfileData', 'identity', 'user', 'notifier', userProfileController]);
}());