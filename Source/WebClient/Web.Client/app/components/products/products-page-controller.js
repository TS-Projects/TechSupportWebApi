(function () {
    'use strict';

    var productsPageController = function productsPageController($scope, uiGmapGoogleMapApi) {
        //  var vm = this;
        //display at clicked point
        $scope.map = { center: { latitude: 40.1451, longitude: -99.6680 }, zoom: 4, bounds: {} };
        $scope.polylines = [];
        // get from db
        $scope.route = {
            "type": "LineString",
            "coordinates": [[24.75691795349121, 42.142850639465884], [24.760007858276364, 42.136613490397686], [24.75271224975586, 42.13648619524438], [24.74833488464355, 42.13591336388817], [24.742326736450195, 42.134894984239224]]
        };

        uiGmapGoogleMapApi.then(function () {
            $scope.polylines = [
                {
                    id: 1,
                    path: $scope.route,
                    stroke: {
                        color: '#6060FB',
                        weight: 3
                    },
                    editable: true,
                    draggable: true,
                    geodesic: true,
                    visible: true,
                    icons: [
                        {
                            icon: {
                                path: google.maps.SymbolPath.BACKWARD_OPEN_ARROW
                            },
                            offset: '25px',
                            repeat: '50px'
                        }
                    ]
                }
                //{
                //    id: 2,
                //    path: [
                //        {
                //            latitude: 47,
                //            longitude: -74
                //        },
                //        {
                //            latitude: 32,
                //            longitude: -89
                //        },
                //        {
                //            latitude: 39,
                //            longitude: -122
                //        },
                //        {
                //            latitude: 62,
                //            longitude: -95
                //        }
                //    ],
                //    stroke: {
                //        color: '#6060FB',
                //        weight: 3
                //    },
                //    editable: true,
                //    draggable: true,
                //    geodesic: true,
                //    visible: true,
                //    icons: [
                //        {
                //            icon: {
                //                path: google.maps.SymbolPath.BACKWARD_OPEN_ARROW
                //            },
                //            offset: '25px',
                //            repeat: '50px'
                //        }
                //    ]
                //}
            ];
            $scope.drawingManagerOptions = {
                drawingMode: google.maps.drawing.OverlayType.MARKER,
                drawingControl: true,
                drawingControlOptions: {
                    position: google.maps.ControlPosition.TOP_CENTER,
                    drawingModes: [
                      google.maps.drawing.OverlayType.MARKER,
                      google.maps.drawing.OverlayType.CIRCLE,
                      google.maps.drawing.OverlayType.POLYGON,
                      google.maps.drawing.OverlayType.POLYLINE,
                      google.maps.drawing.OverlayType.RECTANGLE
                    ]
                },
                circleOptions: {
                    fillColor: '#ffff00',
                    fillOpacity: 1,
                    strokeWeight: 5,
                    clickable: false,
                    editable: true,
                    zIndex: 1
                }
            };
        });
        $scope.events = {
            'markercomplete': function (gObject, gData) {
                console.debug(gData);
                console.debug(gObject);
            },
            'polylinecomplete': function (polyline, eventName, model, args) {
                console.log("You fi;nished drawing polyline to [%s]", args[0].getPath().getArray().join(","));
                //send to db
            }
        };

    }
    angular
        .module('techSupportApp.controllers')
        .controller('ProductsPageController', ['$scope', 'uiGmapGoogleMapApi', productsPageController]);
}());