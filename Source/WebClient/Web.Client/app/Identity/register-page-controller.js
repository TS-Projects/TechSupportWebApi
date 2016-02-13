(function () {
    'use strict';

    var registerPageController = function registerPageController($location, notifier, auth) {
        var vm = this;

        vm.emailPattern = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

        vm.signUp = function (user) {
            auth.signUp(user).then(function () {
                notifier.success('Registration successful!');
                $location.path('/');
            }, function(error) {
                notifier.error(error);
            });
        }
    }

    angular
        .module('techSupportApp.controllers')
        .controller('RegisterPageController', ['$location', 'notifier', 'auth', registerPageController]);
}());