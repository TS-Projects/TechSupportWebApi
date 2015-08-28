(function () {
    'use strict';

    var homePageController = function homePageController() {
        var vm = this;

        vm.tagline = 'To the moon and back!';
    };

    angular
        .module('techSupportApp.controllers')
        .controller('HomePageController', [homePageController]);
}());