(function () {
    'use strict';

    var headerDirective = function headerDirective() {
        return {
            restrict: 'A',
            scope: false,
            templateUrl: 'app/common/header-directive.html'
        };
    };

    angular
        .module('techSupportApp.directives')
        .directive('techsupportHeader', [headerDirective]);
}());