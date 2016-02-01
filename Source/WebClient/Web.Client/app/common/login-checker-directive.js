(function () {
    'use strict';

    var CheckerDirective = function CheckerDirective() {
        return {
            restrict: 'A',
            scope: false,
            templateUrl: 'app/common/login-checker-directive.html'
        };
    };

    angular
        .module('techSupportApp.directives')
        .directive('loginChecker', [CheckerDirective]);
}());