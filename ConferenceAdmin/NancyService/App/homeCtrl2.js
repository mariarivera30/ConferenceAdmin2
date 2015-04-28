(function () {
    'use strict';

    var controllerId = 'homeCtrl2';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', homeCtrl2]);

    function homeCtrl2($scope, $http, restApi) {

        //Variables
        var vm = this;
        vm.activate = activate;
        vm.title = 'homeCtrl2';

        //InterfaceElements
        vm.ihomeMainTitle;
        vm.ihomeParagraph1;
        vm.iimg;

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

        //Functions
        vm.getHome = _getHome;

        _getHome();
        activate();

        function activate() {

        }

        function _getHome() {
            restApi.getHome()
            .success(function (data, status, headers, config) {
                if (data != null && data != "") {
                    vm.ihomeMainTitle = data.homeMainTitle;
                    vm.ihomeParagraph1 = data.homeParagraph1;

                    _getImage();

                    load();
                }
            })

            .error(function (error) {
                load();
                vm.toggleModal('error');
            });
        }

        function _getImage() {
            restApi.getHomeImage()
            .success(function (data, status, headers, config) {
                if (data != null && data != "") {
                    vm.iimg = data.image;
                }
            })

            .error(function (error) {
                vm.toggleModal('error');
            });
        }

        //Avoid flashing when page loads
        var load = function () {
            document.getElementById("loading-icon").style.visibility = "hidden";
            document.getElementById("body").style.visibility = "visible";
        };
    }
})();