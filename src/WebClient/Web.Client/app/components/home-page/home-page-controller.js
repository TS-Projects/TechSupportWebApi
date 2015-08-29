(function () {
    'use strict';

    var homePageController = function homePageController(latestProjects) {
        var vm = this;

        vm.latestProjects = latestProjects;
    };

    angular
        .module('techSupportApp.controllers')
        .controller('HomePageController', ['latestProjects', homePageController]);
}());