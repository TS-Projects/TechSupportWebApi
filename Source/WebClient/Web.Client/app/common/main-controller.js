(function () {
    'use strict';

    var mainController = function mainController($location, notifier, auth, identity) {
        var vm = this;

        waitForLogin();

        //vm.logout = function logout() {
        //    auth.logout();
        //    vm.currentUser = undefined;
        //    waitForLogin();
        //    $location.path('/');
        //};

        //vm.logout  = function () {
        //    auth.logout().then(function() {
        //        //notifier.success('Successful logout!');
        //        //if (vm.user) {
        //        //    vm.user.email = '';
        //        //    vm.user.username = '';
        //        //    vm.user.password = '';
        //        //}
        //        waitForLogin();
        //        //vm.loginForm.$setPristine();
        //        $location.path('/');
        //    });

        vm.logout = function() {
            auth.logout().then(function () {
                notifier.success('Successful logout!');
                vm.currentUser = undefined;
                $location.path('/');
            });
        };

        function waitForLogin() {
            if (identity.isAuthenticated()) {
                console.log("identity.isAuthenticated: ", identity.isAuthenticated());
                console.log("auth.isAuthenticated: ", auth.isAuthenticated());
                var user = identity.getCurrentUser();
                console.log("ei mainController: ", user);
                vm.currentUser = user;
            }
        };

        //vm.search = function (searchTerm) {
        //    $location.path('/projects/search').search('term', searchTerm);
        //};

        //function waitForLogin() {
        //    identity.getUser().then(function (user) {
        //        vm.currentUser = user;
        //    });
        //}


    };

    angular
        .module('techSupportApp.controllers')
        .controller('MainController', ['$location', 'notifier', 'auth', 'identity', mainController]);
}());