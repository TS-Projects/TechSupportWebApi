(function () {
    'use strict';

    var contactsPageController = function contactsPageController(uiGmapGoogleMapApi) {
        var vm = this;
        vm.map = { center: { latitude: 45, longitude: -73 }, zoom: 8 }

        uiGmapGoogleMapApi.then(function (maps) {
            vm.map = { center: { latitude: 45, longitude: -73 }, zoom: 8 }

        });

    };

    angular
        .module('techSupportApp.controllers')
        .controller('ContactsPageController', ['uiGmapGoogleMapApi', contactsPageController]);
}());