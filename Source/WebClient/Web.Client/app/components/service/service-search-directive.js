(function () {
    'use strict';

    var serviceSearchDirective = function serviceSearchDirective() {
        return {
            restrict: 'A',
            templateUrl: 'app/components/service/service-search-directive.html',
            scope: {
                orders: '='
            },
            link: function (scope, element) {

            }
        };
    };

    angular
        .module('techSupportApp.directives')
        .directive('serviceSearchDirective', ['jQuery', serviceSearchDirective]);
}());