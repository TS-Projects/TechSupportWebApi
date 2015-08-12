'use strict';

angular.module('appRoutes', []).config(function ($routeProvider, $httpProvider, $locationProvider) {

    $httpProvider.interceptors.push('errorHandlerHttpInterceptor');

    var routeUserChecks = {
        authenticated: {
            authenticate: function (auth) {
                return auth.isAuthenticated();
            }
        }
    };

    $routeProvider
		.when('/', {
		    templateUrl: 'app/components/main/main.html',
		    controller: 'MainController'
		})
		.when('/administration', {
		    templateUrl: 'app/components/administration/users.html',
		    controller: 'AdministrationUsersController'
		    //resolve: routeUserChecks.authenticated
		})
        .when('/customercard', {
            templateUrl: 'app/components/customercard/customercard.html',
            controller: 'CustomerCardController'
            //resolve: routeUserChecks.authenticated
        		})
        .when('/register', {
            templateUrl: 'app/components/login/register.html',
            controller: 'SignUpController'
        })
        .otherwise({ redirectTo: '/' });

    //$locationProvider.html5Mode({
    //    enabled: true,
    //    requireBase: false
    //});
    

})
 .run(function ($rootScope, $location) {
     $rootScope.$on('$routeChangeError', function (ev, current, previous, rejection) {
         if (rejection === 'not authorized') {
             $location.path('/unauthorized');
         }
     })
 })
.value('toastr', toastr)
.constant('baseServiceUrl', 'http://localhost:13078');