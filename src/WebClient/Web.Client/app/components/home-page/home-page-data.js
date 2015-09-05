(function () {
    'use strict';

    var homePageData = function homePageData(data) {
        function getLatestProjects() {
            //return data.get('/projects');
            return null;
        }

        return {
            getLatestProjects: getLatestProjects
        };
    };

    angular.module('techSupportApp.data')
        .factory('homePageData', ['data', homePageData]);
}());