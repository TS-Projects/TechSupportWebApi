(function () {
    'use strict';

    var mainController = function mainController($location, auth, identity) {
        var vm = this;

        waitForLogin();

        vm.logout = function logout() {
            auth.logout();
            vm.currentUser = undefined;
            vm.isAdmin = false;
            waitForLogin();
            $location.path('/');
        };

        //vm.search = function (searchTerm) {
        //    $location.path('/projects/search').search('term', searchTerm);
        //};

        function waitForLogin() {
            identity.getUser().then(function (user) {
                var allRoles = user.roles;
                var role = allRoles.split(',')[0];
                vm.isAdmin = false;
                if (role === "Administrator") {
                    vm.isAdmin = true;
                }

                vm.currentUser = user;
            });
        }
    };

    angular
        .module('techSupportApp.controllers')
        .controller('MainController', ['$location', 'auth', 'identity', mainController]);
}());