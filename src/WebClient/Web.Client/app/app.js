//'use strict';

//angular.module('appRoutes', []).config(function ($routeProvider, $httpProvider, $locationProvider) {

//    $httpProvider.interceptors.push('errorHandlerHttpInterceptor');

//    var routeUserChecks = {
//        authenticated: {
//            authenticate: function (auth) {
//                return auth.isAuthenticated();
//            }
//        }
//    };

//    $routeProvider
//		.when('/', {
//		    templateUrl: 'app/components/main/main.html',
//		    controller: 'MainController'
//		})
//		.when('/administration', {
//		    templateUrl: 'app/components/administration/users.html',
//		    controller: 'AdministrationUsersController'
//		    //resolve: routeUserChecks.authenticated
//		})
//        .when('/customercard', {
//            templateUrl: 'app/components/customercard/customercard.html',
//            controller: 'CustomerCardController'
//            //resolve: routeUserChecks.authenticated
//        		})
//        .when('/register', {
//            templateUrl: 'app/components/login/register.html',
//            controller: 'SignUpController'
//        })
//        .otherwise({ redirectTo: '/' });

//    //$locationProvider.html5Mode({
//    //    enabled: true,
//    //    requireBase: false
//    //});
    

//})
// .run(function ($rootScope, $location) {
//     $rootScope.$on('$routeChangeError', function (ev, current, previous, rejection) {
//         if (rejection === 'not authorized') {
//             $location.path('/unauthorized');
//         }
//     })
// })
//.value('toastr', toastr)
//.constant('baseServiceUrl', 'http://localhost:13078');


(function () {
    'use strict';

    var config = function config($routeProvider, $locationProvider, $httpProvider) {

        var CONTROLLER_VIEW_MODEL_NAME = 'vm';

        //$locationProvider.html5Mode(true);

      //  var routeResolveChecks = routeResolversProvider.$get();

        $routeProvider
            .when('/', {
                templateUrl: 'app/components/home-page/home-page-view.html',
                controller: 'HomePageController',
                controllerAs: CONTROLLER_VIEW_MODEL_NAME,
              //  resolve: routeResolveChecks.home
            })
            //.when('/projects/search', {
            //    templateUrl: 'projects-search-page/projects-search-page-view.html',
            //    controller: 'ProjectsSearchPageController',
            //    controllerAs: CONTROLLER_VIEW_MODEL_NAME,
            //    reloadOnSearch: false
            //})
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

       // $httpProvider.interceptors.push('httpResponseInterceptor');
    };

    var run = function run($rootScope, $location) {
        $rootScope.$on('$routeChangeError', function (ev, current, previous, rejection) {
            if (rejection === 'not authorized') {
                //notifier.warning('Please log into your account first!');
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

    angular.module('techSupportApp', ['ngRoute','ngResource', 'ngCookies','kendo.directives', 'techSupportApp.controllers', 'techSupportApp.directives'])
        //, 'NotifierService', 'IdentityService', 'appRoutes', 'MainCtrl', 'ErrorHandlerService', 'ErrorHandlerHttpInterceptorService', 'AuthorizationService', 'infinite-scroll' , 'ngAnimate' 'angular-loading-bar', 'templates', 'ui.bootstrap', 'hSweetAlert',


        .config(['$routeProvider', '$locationProvider', '$httpProvider', config])
        .run(['$rootScope', '$location', run])
        .value('jQuery', jQuery)
        .value('toastr', toastr)
        .constant('appSettings', {
            serverPath: 'http://localhost:13078/api',
            odataServerPath: 'http://localhost:13078/odata'
        });
}());