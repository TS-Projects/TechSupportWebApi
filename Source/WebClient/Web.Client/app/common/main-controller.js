(function () {
    'use strict';

    var mainController = function mainController($location, auth, identity) {
        var vm = this;
        var administratorRole = "Administrator";

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
                var rolesArr = allRoles.split(',');
                vm.isAdmin = false;
                if (rolesArr.indexOf(administratorRole) > -1) {
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