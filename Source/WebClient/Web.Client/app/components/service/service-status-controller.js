﻿(function () {
    'use strict';

    var serviceStatusController = function serviceStatusController($scope, $q, $http, vcRecaptchaService) {
        var vm = this;

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
        vm.submit = function() {
            var defered = $q.defer();

            var URL = 'http://localhost:13078/api/Captcha';

            var postData = { "g-recaptcha-response": vm.response }

            $http.post(URL, postData)
                .then(function (response) {
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
        .controller('ServiceStatusController', ['$scope', '$q', '$http', 'vcRecaptchaService', serviceStatusController]);
}());