(function () {
    'use strict';

    var loginPageController = function loginPageController($location, notifier, auth) {
        var vm = this;

        vm.login = function (user) {
            auth.login(user).then(function () {
                notifier.success('Successful login!');
                $location.path('/');
            });
        }
    }

    angular
        .module('techSupportApp.controllers')
        .controller('LoginPageController', ['$location', 'notifier', 'auth', loginPageController]);
}());