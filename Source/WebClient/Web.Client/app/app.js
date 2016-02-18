(function () {
    'use strict'

    var config = function config($routeProvider, $locationProvider, $httpProvider, routeResolversProvider, GoogleMapApi, $translateProvider) {

        var CONTROLLER_VIEW_MODEL_NAME = 'vm';

        $locationProvider.html5Mode(true);

        var routeResolveChecks = routeResolversProvider.$get();


        //GoogleMapApi.configure( {
        //});

        //$translateProvider.translations('en-US', {
        //    'TITLE': 'Hello',
        //    'FOO': 'This is a paragraph'
        //});

        //$translateProvider.translations('de-DE', {
        //    'TITLE': 'Hallo',
        //    'FOO': 'Dies ist ein Paragraph'
        //});

        //$translateProvider.preferredLanguage('en-US');

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
            .when('/login', {
                templateUrl: 'app/Identity/login-page-view.html',
                controller: 'LoginPageController',
                controllerAs: CONTROLLER_VIEW_MODEL_NAME
            })
            .when('/administration/users', {
                templateUrl: 'app/components/administration/users-administration-view.html',
                controller: 'UsersAdministrationController',
                resolve: routeResolveChecks.administration
            })
            .when('/products', {
                templateUrl: 'app/components/products/products-page-view.html',
                controller: 'ProductsPageController',
                controllerAs: CONTROLLER_VIEW_MODEL_NAME,
                resolve: routeResolveChecks.products
            })
            .when('/register', {
                templateUrl: 'app/Identity/register-page-view.html',
                controller: 'RegisterPageController',
                controllerAs: CONTROLLER_VIEW_MODEL_NAME
            })
            .when('/users/:username', {
               templateUrl: 'app/components/user-profile-page/user-profile-view.html',
               controller: 'UserProfileController',
               controllerAs: CONTROLLER_VIEW_MODEL_NAME,
               resolve: routeResolveChecks.profile
            })
            .when('/contacts', {
                templateUrl: 'app/components/contacts/contacts-page-view.html',
                controller: 'ContactsPageController',
                controllerAs: CONTROLLER_VIEW_MODEL_NAME
            })
            .when('/about', {
                templateUrl: 'app/components/about/about-page-view.html',
                controller: 'AboutUsPageController',
                controllerAs: CONTROLLER_VIEW_MODEL_NAME
            })
            .when('/notfound', {
                templateUrl: 'not-found-page/not-found-view.html'
            })
            .otherwise({ redirectTo: '/notfound' });

        $httpProvider.interceptors.push('httpResponseInterceptor');
    };

    var run = function run($rootScope, $http, $location, auth, notifier) {
        $rootScope.$on('$routeChangeError', function (ev, current, previous, rejection) {
            if (rejection === 'not authorized') {
                notifier.warning('Please log into your account first!');
                $location.path('/');
                //angular
                //    .element('#open-login-btn')
                //    .trigger('click');

                //angular.element('#login-modal')
                //    .attr('data-previous-route', previous.$$route.originalPath)
                //    .attr('data-current-route', current.$$route.originalPath);
            }
        });
        if (auth.isAuthenticated()) {
            auth.getIdentity().then(function (identity) {
                notifier.success('Welcome back, ' + identity.userName + '!');
            });
        }
    };


    // angular.module('templates', []) // used for client-side template caching
    angular.module('techSupportApp.data', []);
    angular.module('techSupportApp.services', []);
    angular.module('techSupportApp.controllers', ['techSupportApp.data', 'techSupportApp.services']);
    angular.module('techSupportApp.directives', []);

    angular.module('techSupportApp', ['ngRoute', 'ngResource', 'ngCookies', 'kendo.directives', 'techSupportApp.controllers', 'techSupportApp.directives', 'uiGmapgoogle-maps', 'ui.bootstrap', 'ngMessages', 'ngAnimate', 'pascalprecht.translate'])
        .config(['$routeProvider', '$locationProvider', '$httpProvider', 'routeResolversProvider', 'uiGmapGoogleMapApiProvider', '$translateProvider', config])
        .run(['$rootScope', '$http', '$location', 'auth', 'notifier', run])
        .value('jQuery', jQuery)
        .value('toastr', toastr)
        .value('errorHandler', function (error) { console.warn(error) })
        .constant('appSettings', {
            serverPath: 'http://localhost:13078',
            odataServerPath: 'http://localhost:13078/administration'
        });
}());