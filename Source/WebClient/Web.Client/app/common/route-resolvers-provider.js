﻿(function () {
    'use strict';

    var routeResolversProvider = function routeResolversProvider() {
        var routeResolvers = {
            authenticated: ['$q', 'auth', function ($q, auth) {
                if (!auth.isAuthenticated()) {
                    return $q.reject('not authorized');
                }
                return $q.when(true);
            }],
            latestProjects: ['homePageData', function (homePageData) {
                return homePageData.getLatestProjects();
            }],
            allProducts: ['productsPageData', function(productsPageData){
                return productsPageData.getAllProducts();
            }],
            allUsers: ['$injector', '$q', 'usersAdministrationPageData', function ($injector, $q, usersAdministrationPageData) {
                var authPromise = $injector.invoke(routeResolvers.authenticated);
                return authPromise.then(function () {
                    return usersAdministrationPageData.getAllRegistersUsers();
                });
            }],
            allCustomerCards: ['$injector', '$q', 'customerCardsAdministrationPageData', function ($injector, $q, customerCardsAdministrationPageData) {
                var authPromise = $injector.invoke(routeResolvers.authenticated);
                return authPromise.then(function () {
                    return customerCardsAdministrationPageData.getAllRegistersUsers();
                });
            }],
            

            //userProfile: ['$injector', '$q', 'userProfileData', function ($injector, $q, userProfileData) {
            //    var authPromise = $injector.invoke(routeResolvers.authenticated);
            //    return authPromise.then(function () {
            //        return userProfileData.getProfile();
            //    });
            //}],
            //popularProjects: ['homePageData', function (homePageData) {
            //    return homePageData.getMostPopularProjects();
            //}],
            //statistics: ['statisticsData', function (statisticsData) {
            //    return statisticsData.getMainStatistics();
            //}],
            //seasonTags: ['$injector', '$q', 'addProjectData', function ($injector, $q, addProjectData) {
            //    var authPromise = $injector.invoke(routeResolvers.authenticated);
            //    return authPromise.then(function () {
            //        return addProjectData.getSeasonTags();
            //    });
            //}],
            //languageAndTechnologyTags: ['$injector', '$q', 'addProjectData', function ($injector, $q, addProjectData) {
            //    var authPromise = $injector.invoke(routeResolvers.authenticated);
            //    return authPromise.then(function () {
            //        return addProjectData.getLanguageAndTechnologyTags();
            //    });
            //}],
            //detailedStatistics: ['$q', 'statisticsData', function ($q, statisticsData) {
            //    return $q.all([
            //        statisticsData.getMainStatistics(),
            //        statisticsData.getProjectsForLastSixMonths(),
            //        statisticsData.getProjectsCountTag(),
            //        statisticsData.getMostLikedProjects(),
            //        statisticsData.getTopUsers()
            //    ]).then(function (results) {
            //        return {
            //            mainStatistics: results[0],
            //            projectsLastSixMonths: results[1],
            //            projectsCountByTag: results[2],
            //            mostLikedProjects: results[3],
            //            topUsers: results[4],
            //        };
            //    });
            //}],
            //project: ['$route', 'projectDetailsData', function ($route, projectDetailsData) {
            //    var routeParams = $route.current.params;
            //    return projectDetailsData.getProject(routeParams.id, routeParams.title);
            //}],
            user: ['$route', 'userProfileData', function ($route, userProfileData) {
                var routeParams = $route.current.params;
                return userProfileData.getProfile(routeParams.username.toLowerCase());
            }]
        };

        var routeResolveChecks = {
            home: {
                latestProjects: routeResolvers.latestProjects
                //popularProjects: routeResolvers.popularProjects,
                //statistics: routeResolvers.statistics
            },
            administration: {
                allUsers: routeResolvers.allUsers,
                allCustomerCards: routeResolvers.allCustomerCards
            },
            products: {
                allProducts: routeResolvers.allProducts
            },
            //addProject: {
            //    seasonTags: routeResolvers.seasonTags,
            //    languageAndTechnologyTags: routeResolvers.languageAndTechnologyTags
            //},
            //statistics: {
            //    detailedStatistics: routeResolvers.detailedStatistics
            //},
            //projectDetails: {
            //    project: routeResolvers.project
            //},
            profile: {
                user: routeResolvers.user
                //userProfile: routeResolvers.userProfile
            }
        };

        return {
            $get: function () {
                return routeResolveChecks;
            }
        };
    };

    angular
        .module('techSupportApp')
        .provider('routeResolvers', routeResolversProvider);
}());