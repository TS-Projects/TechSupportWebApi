//angular.module('LoginCtrl', []).controller('LoginController', ['$scope', '$location', 'notifier', 'identity', 'auth',
//    function LoginCtrl($scope, $location, notifier, identity, auth) {
//        $scope.identity = identity;

//        $scope.login = function(user, loginForm) {
//            if (loginForm.$valid) {
//                auth.login(user).then(function(success) {
//                    if (success) {
//                        notifier.success('Successful login!');
//                        $location.path('/');
//                    }
//                    else {
//                        notifier.error('Username/Password combination is not valid!');
//                    }
//                });
//            }
//            else {
//                notifier.error('Username and password are required fields!')
//            }
//        }

//        $scope.logout = function() {
//            auth.logout().then(function() {
//                notifier.success('Successful logout!');
//                if ($scope.user) {
//                    $scope.user.email = '';
//                    $scope.user.username = '';
//                    $scope.user.password = '';
//                }

//                $scope.loginForm.$setPristine();
//                $location.path('/');
//            })
//        }
//    }]);


(function () {
    'use strict';

    var loginPageController = function loginPageController($scope, $location, notifier, identity, auth) {
        var vm = this;

        vm.identity = identity;

        vm.login = function(user, loginForm) {
            if (loginForm.$valid) {
                auth.login(user).then(function(success) {
                    if (success) {
                        notifier.success('Successful login!');
                        $location.path('/');
                    } else {
                        notifier.error('Username/Password combination is not valid!');
                    }
                });
            } else {
                notifier.error('Username and password are required fields!');
            }
        };
    }
    angular
        .module('techSupportApp.controllers')
        .controller('LoginPageController', ['$scope', '$location', 'notifier', 'identity', 'auth', loginPageController]);
}());