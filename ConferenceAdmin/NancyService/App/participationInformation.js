(function () {
    'use strict';

    var controllerId = 'participationCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', participationCtrl]);

    function participationCtrl($scope, $http, restApi) {
        var vm = this;
        vm.activate = activate;
        vm.title = 'participationCtrl';

        //Admin
        vm.temp;
        vm.participationParagraph1;

        vm.loading = false;

        //Interface
        vm.iparticipationParagraph1;

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
        vm.getParticipation = _getParticipation;
        vm.saveParticipation = _saveParticipation;
        vm.reset = _reset;

        _getParticipation();

        function activate() {

        }

        function _reset() {
            vm.participationParagraph1 = vm.temp.participationParagraph1;
        }

        function _getParticipation() {
            restApi.getParticipation()
            .success(function (data, status, headers, config) {
                if (data != null && data!="") {
                    vm.temp = data;
                    vm.iparticipationParagraph1 = data.participationParagraph1;
                    vm.participationParagraph1 = data.participationParagraph1;
                    load();
                }
            })

            .error(function (error) {
                load();
                vm.toggleModal('error');
            });
        }

        function _saveParticipation() {
            vm.loading = true;
            var newParticipation = {
                participationParagraph1: vm.participationParagraph1
            }
            restApi.saveParticipation(newParticipation)
            .success(function (data, status, headers, config) {
                if (data != null && data != "") {
                    vm.temp.participationParagraph1 = newParticipation.participationParagraph1;
                    vm.loading = false;
                    $("#updateConfirm").modal('show');
                }
            })
            .error(function (error) {
                vm.loading = false;
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