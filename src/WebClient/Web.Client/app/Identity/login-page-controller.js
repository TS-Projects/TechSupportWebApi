(function () {
    'use strict';

    var loginPageController = function loginPageController($scope, $location, notifier, auth) {
        var vm = this;

        vm.login = function (user) {
            auth.login(user).then(function () {
                notifier.success('Successful login!');
                $location.path('/');
            }, function () {
               notifier.error('Username/Password combination is not valid!');
            });
        }
    }

    angular
        .module('techSupportApp.controllers')
        .controller('LoginPageController', ['$scope', '$location', 'notifier', 'auth', loginPageController]);
}());