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
        vm.temp;
        vm.venueParagraph1;
        vm.venueTitleBox;
        vm.venueParagraphContentBox;
        vm.loading = false;

        //InterfaceElements
        vm.ivenueParagraph1;
        vm.ivenueTitleBox;
        vm.ivenueParagraphContentBox;

        //Functions
        vm.getVenue = _getVenue;
        vm.saveVenue = _saveVenue;
        vm.reset = _reset;

        //For error modal:
        vm.obj = {
            title: "",
            message1: "",
            message2: "",
            label: "",
            okbutton: false,
            okbuttonText: "",
            cancelbutton: false,
            cancelbuttoText: "Cancel",
        };
        vm.okFunc;
        vm.cancelFunc;

        vm.toggleModal = function (action) {

          if (action == "error")
               vm.obj.title = "Server Error",
               vm.obj.message1 = "Please refresh the page and try again.",
               vm.obj.message2 = "",
               vm.obj.label = "",
               vm.obj.okbutton = true,
               vm.obj.okbuttonText = "OK",
               vm.obj.cancelbutton = false,
               vm.obj.cancelbuttoText = "Cancel",
               vm.showConfirmModal = !vm.showConfirmModal;
            };

        _getVenue();

        function activate() {

        }

        function _reset() {
            vm.venueParagraph1 = vm.temp.venueParagraph1;
            vm.venueTitleBox = vm.temp.venueTitleBox;
            vm.venueParagraphContentBox = vm.temp.venueParagraphContentBox;
        }

        function _getVenue() {
            restApi.getVenue()
            .success(function (data, status, headers, config) {
                if (data != null && data != "") {
                    vm.temp = data;
                    vm.ivenueParagraph1 = data.venueParagraph1;
                    vm.ivenueTitleBox = data.venueTitleBox;
                    vm.ivenueParagraphContentBox = data.venueParagraphContentBox;

                    vm.venueParagraph1 = data.venueParagraph1;
                    vm.venueTitleBox = data.venueTitleBox;
                    vm.venueParagraphContentBox = data.venueParagraphContentBox;

                    load();
                }
            })

            .error(function (error) {
                load();
                vm.toggleModal('error');
            });
        }

        function _saveVenue() {
            vm.loading = true;
            var newVenue = {
                venueParagraph1: vm.venueParagraph1,
                venueTitleBox: vm.venueTitleBox,
                venueParagraphContentBox: vm.venueParagraphContentBox
            }
            restApi.saveVenue(newVenue)
            .success(function (data, status, headers, config) {
                if (data) {
                    vm.temp.venueParagraph1 = newVenue.venueParagraph1;
                    vm.temp.venueTitleBox = newVenue.venueTitleBox;
                    vm.temp.venueParagraphContentBox = newVenue.venueParagraphContentBox;
                    $("#updateConfirm").modal('show');
                }
                vm.loading = false;
            })
            .error(function (error) {
                vm.loading = false
                vm.toggleModal('error');
            });
        }

        //Avoid flashing when page loads
        var load = function () {
            if (document.getElementById('loading-icon') != null) {
                document.getElementById("loading-icon").style.visibility = "hidden";
            }
            document.getElementById("body").style.visibility = "visible";
        };

    }
})();