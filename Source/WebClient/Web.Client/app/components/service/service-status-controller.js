(function () {
    'use strict';

    var serviceStatusController = function serviceStatusController($scope, $location, $q, $http, authorization, notifier, vcRecaptchaService) {
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
        vm.cbExpiration = function () {
            console.info('Captcha expired. Resetting response object');
            vcRecaptchaService.reload(vm.widgetId);
            vm.response = null;
        };

        // TODO: $ should not be here!
        //http://qloo.github.io/angular-prevent-default/
        $('.tab-button').click(function (e) {
            e.preventDefault();
        });

        var sss = function () {
            var defered = $q.defer();

            var URL = 'http://localhost:13078/api/Service';
            var authHeader = authorization.getAuthorizationHeader();

            $http.get(URL, { headers: authHeader })
                .then(function (response) {
                    defered.resolve(response.data);
                }, function (error) {
                    notifier.error(error);
                    defered.reject(error);
                });

            return defered.promise;
        }

        vm.getStatus = [];

        sss()
          .then(function (response) {
              vm.getStatus = response;
              console.log("ei", response);
              //  defered.resolve(response.data);
          }, function (error) {
              console.log("noi", error);
              //    notifier.error(error);
              //  defered.reject(error);
          });

        vm.submit = function () {
            var defered = $q.defer();

            var URL = 'http://localhost:13078/api/Service';
            vm.formOrder['g-recaptcha-response'] = vm.response;
            var authHeader = authorization.getAuthorizationHeader();

            $http.post(URL, vm.formOrder, { headers: authHeader })
                .then(function (response) {
                    notifier.success('Проверката мина успешно!');
                    $location.path('/service');
                    defered.resolve(response.data);
                }, function (error) {
                    vcRecaptchaService.reload(vm.widgetId);
                    notifier.error('Грешка!');
                    defered.reject(error);
                });

            return defered.promise;
        }
    };

    angular
        .module('techSupportApp.controllers')
        .controller('ServiceStatusController', ['$scope', '$location', '$q', '$http', 'authorization', 'notifier', 'vcRecaptchaService', serviceStatusController]);
}());