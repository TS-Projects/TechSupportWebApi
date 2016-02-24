(function () {
    'use strict';

    var serviceStatusController = function serviceStatusController($scope, $q, $http, authorization, vcRecaptchaService) {
        var vm = this;
        vm.formOrder = {};

        console.log("this is your app's controller");
        vm.response = null;
        vm.widgetId = null;
        vm.model = {
            key: '6LeyHRkTAAAAAC-O3vwsX6fvpSQ0JnEhM3R8QBwk'
        };
        vm.setResponse = function (response) {
            console.info('Response available');
            vm.response = response;
        };
        vm.setWidgetId = function (widgetId) {
            console.info('Created widget ID: %s', widgetId);
            vm.widgetId = widgetId;
        };
        vm.cbExpiration = function() {
            console.info('Captcha expired. Resetting response object');
            vcRecaptchaService.reload(vm.widgetId);
            vm.response = null;
        };
        vm.submit = function () {
            var defered = $q.defer();

            var URL = 'http://localhost:13078/api/Service';
            vm.formOrder['g-recaptcha-response'] = vm.response;
            console.log("vm.formOrder: ", vm.formOrder);
            var authHeader = authorization.getAuthorizationHeader();

            $http.post(URL, vm.formOrder, { headers: authHeader })
                .then(function (response) {
                    console.log("vm.formOrder: ", vm.formOrder);
                    defered.resolve(response.data);
                }, function (error) {
                    vcRecaptchaService.reload(vm.widgetId);
                    defered.reject(error);
                });

            return defered.promise;
        }
    };

    angular
        .module('techSupportApp.controllers')
        .controller('ServiceStatusController', ['$scope', '$q', '$http', 'authorization', 'vcRecaptchaService', serviceStatusController]);
}());