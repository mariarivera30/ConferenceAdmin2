(function () {
    'use strict';

    var controllerId = 'venueCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', venueCtrl]);

    function venueCtrl($scope, $http, restApi) {
        var vm = this;
        vm.activate = activate;
        vm.title = 'venueCtrl';

        //From Admin Website
        vm.venueTitle1;
        vm.venueParagraph1;
        vm.venueTitle2;
        vm.venueParagraph2;
        vm.venueTitleBox;
        vm.venueParagraphContentBox;

        //InterfaceElements
        vm.ivenueTitle1;
        vm.ivenueParagraph1;
        vm.ivenueTitle2;
        vm.ivenueParagraph2;
        vm.ivenueTitleBox;
        vm.ivenueParagraphContentBox;

        //Functions
        vm.getVenue = _getVenue;
        vm.saveVenue = _saveVenue;

        _getVenue();

        function activate() {

        }

        function _getVenue() {
            restApi.getVenue()
            .success(function (data, status, headers, config) {
                if (data != null) {
                    vm.ivenueTitle1 = data.venueTitle1;
                    vm.ivenueParagraph1 = data.venueParagraph1;
                    vm.ivenueTitle2 = data.venueTitle2;
                    vm.ivenueParagraph2 = data.venueParagraph2;
                    vm.ivenueTitleBox = data.venueTitleBox;
                    vm.ivenueParagraphContentBox = data.venueParagraphContentBox;

                    vm.venueTitle1 = data.venueTitle1;
                    vm.venueParagraph1 = data.venueParagraph1;
                    vm.venueTitle2 = data.venueTitle2;
                    vm.venueParagraph2 = data.venueParagraph2;
                    vm.venueTitleBox = data.venueTitleBox;
                    vm.venueParagraphContentBox = data.venueParagraphContentBox;

                    load();
                }
            })

            .error(function (error) {

            });
        }

        function _saveVenue() {

            var newVenue = {
                venueTitle1: vm.venueTitle1,
                venueParagraph1: vm.venueParagraph1,
                venueTitle2: vm.venueTitle2,
                venueParagraph2: vm.venueParagraph2,
                venueTitleBox: vm.venueTitleBox,
                venueParagraphContentBox: vm.venueParagraphContentBox
            }
            restApi.saveVenue(newVenue)
            .success(function (data, status, headers, config) {
                if (data != null) {
                    $("#updateConfirm").modal('show');
                }
            })
            .error(function (error) {
                $("#updateError").modal('show');
            });
        }

        //Avoid flashing when page loads
        var load = function () {
            document.getElementById("body").style.visibility = "visible";
        };

    }
})();