(function () {
    'use strict';

    var serviceOrdersDirective = function serviceOrdersDirective() {
        return {
            restrict: 'A',
            templateUrl: 'app/components/service/service-orders-directive.html',
            scope: {
                search: '='
            },
            link: function (scope, element) {

            }
        };
    };

    angular
        .module('techSupportApp.directives')
        .directive('serviceOrdersDirective', ['jQuery', serviceOrdersDirective]);
}());