(function () {
    'use strict';

    var loginPageController = function loginPageController($scope, $location, notifier, auth) {
       
        //vm.login = function (user) {
        //    auth.login(user).then(function () {
        //        notifier.success('Successful login!');
        //        $location.path('/');
        //    });
        //}


        $scope.login = function (user) {
            if ($scope.loginForm.$valid) {
                auth.login(user).then(function (success) {
                    if (success) {
                        notifier.success('Successful login!');
                        $location.path('/');
                    }
                    else {
                        notifier.error('Username/Password combination is not valid!');
                    }
                });
            }
            else {
                notifier.error('Username and password are required fields!');
            }
        }
    }

    angular
        .module('techSupportApp.controllers')
        .controller('LoginPageController', ['$scope', '$location', 'notifier', 'auth', loginPageController]);
}());