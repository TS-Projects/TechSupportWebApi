(function () {
    'use strict';

    var config = function config($routeProvider, $locationProvider, $httpProvider, routeResolversProvider) {

        var CONTROLLER_VIEW_MODEL_NAME = 'vm';


        //$locationProvider.html5Mode(true);

        var routeResolveChecks = routeResolversProvider.$get();

        $routeProvider
            .when('/', {
                templateUrl: 'app/components/home-page/home-page-view.html',
                controller: 'HomePageController',
                controllerAs: CONTROLLER_VIEW_MODEL_NAME,
                resolve: routeResolveChecks.home
            })
            //.when('/projects/search', {
            //    templateUrl: 'projects-search-page/projects-search-page-view.html',
            //    controller: 'ProjectsSearchPageController',
            //    controllerAs: CONTROLLER_VIEW_MODEL_NAME,
            //    reloadOnSearch: false
            //})
            .when('/demologin', {
                templateUrl: 'app/Identity/login-page-view.html',
                controller: 'LoginPageController',
                controllerAs: CONTROLLER_VIEW_MODEL_NAME
            })
            .when('/administration', {
                templateUrl: 'app/components/administration/users-administration-view.html',
                controller: 'UsersAdministrationController',
                controllerAs: CONTROLLER_VIEW_MODEL_NAME
                //  resolve: routeResolveChecks.addProject
            })
            .when('/notfound', {
                templateUrl: 'not-found-page/not-found-view.html'
            })
            .otherwise({ redirectTo: '/notfound' });

        //$locationProvider.html5Mode({
        //    enabled: true,
        //    requireBase: false
        //});


        $httpProvider.interceptors.push('httpResponseInterceptor');
    };

    var run = function run($rootScope, $location, auth, notifier) {
        $rootScope.$on('$routeChangeError', function (ev, current, previous, rejection) {
            if (rejection === 'not authorized') {
                notifier.warning('Please log into your account first!');
                $location.path('/');
                angular
                    .element('#open-login-btn')
                    .trigger('click');

                angular.element('#login-modal')
                    .attr('data-previous-route', previous.$$route.originalPath)
                    .attr('data-current-route', current.$$route.originalPath);
            }
        });

        //if (auth.isAuthenticated()) {
        //    auth.getIdentity().then(function (identity) {
        //        notifier.success('Welcome back, ' + identity.userName + '!');
        //    });
        //}
    };

    // angular.module('templates', []) // used for client-side template caching
    angular.module('techSupportApp.data', []);
    angular.module('techSupportApp.services', []);
    angular.module('techSupportApp.controllers', ['techSupportApp.data', 'techSupportApp.services']);
    angular.module('techSupportApp.directives', []);

    angular.module('techSupportApp', ['ngRoute', 'ngResource', 'ngCookies', 'kendo.directives', 'techSupportApp.controllers', 'techSupportApp.directives'])
        .config(['$routeProvider', '$locationProvider', '$httpProvider', 'routeResolversProvider', config])
        .run(['$rootScope', '$location', 'auth', 'notifier', run])
        .value('jQuery', jQuery)
        .value('toastr', toastr)
        .constant('appSettings', {
            serverPath: 'http://localhost:13078/api',
            odataServerPath: 'http://localhost:13078/odata'
        });
}());