(function () {
    'use strict';

    var serviceStatusController = function serviceStatusController($scope, vcRecaptchaService) {
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
            var valid;
            /**
             * SERVER SIDE VALIDATION
             *
             * You need to implement your server side validation here.
             * Send the reCaptcha response to the server and use some of the server side APIs to validate it
             * See https://developers.google.com/recaptcha/docs/verify
             */
            console.log('sending the captcha response to the server', vm.response);
            if (valid) {
                console.log('Success');
            } else {
                console.log('Failed validation');
                // In case of a failed validation you need to reload the captcha
                // because each response can be checked just once
                vcRecaptchaService.reload(vm.widgetId);
            }
        }
    };

    angular
        .module('techSupportApp.controllers')
        .controller('ServiceStatusController', ['$scope', 'vcRecaptchaService', serviceStatusController]);
}());