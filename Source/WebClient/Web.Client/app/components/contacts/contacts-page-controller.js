(function () {
    'use strict';

    var contactsPageController = function contactsPageController(uiGmapGoogleMapApi) {
        var vm = this;

        vm.map = { center: { latitude: 42.643343, longitude: 24.8061749 }, zoom: 15 };


    };

    angular
        .module('techSupportApp.controllers')
        .controller('ContactsPageController', ['uiGmapGoogleMapApi', contactsPageController]);
}());