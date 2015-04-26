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
        vm.participationTitle1;
        vm.participationTitle2;
        vm.participationTitle3;
        vm.participationTitle4;
        vm.participationTitle5;

        vm.participationParagraph1;
        vm.participationParagraph2;
        vm.participationParagraph3;
        vm.participationParagraph4;
        vm.participationParagraph5;

        vm.loading = false;

        //Interface
        vm.iparticipationTitle1;
        vm.iparticipationTitle2;
        vm.iparticipationTitle3;
        vm.iparticipationTitle4;
        vm.iparticipationTitle5;

        vm.iparticipationParagraph1;
        vm.iparticipationParagraph2;
        vm.iparticipationParagraph3;
        vm.iparticipationParagraph4;
        vm.iparticipationParagraph5;

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
            vm.participationTitle1 = vm.temp.participationTitle1;
            vm.participationTitle2 = vm.temp.participationTitle2;
            vm.participationTitle3 = vm.temp.participationTitle3;
            vm.participationTitle4 = vm.temp.participationTitle4;
            vm.participationTitle5 = vm.temp.participationTitle5;
            vm.participationParagraph1 = vm.temp.participationParagraph1;
            vm.participationParagraph2 = vm.temp.participationParagraph2;
            vm.participationParagraph3 = vm.temp.participationParagraph3;
            vm.participationParagraph4 = vm.temp.participationParagraph4;
            vm.participationParagraph5 = vm.temp.participationParagraph5;
        }

        function _getParticipation() {
            restApi.getParticipation()
            .success(function (data, status, headers, config) {
                if (data != null && data!="") {
                    vm.temp = data;
                    vm.iparticipationTitle1 = data.participationTitle1;
                    vm.iparticipationTitle2 = data.participationTitle2;
                    vm.iparticipationTitle3 = data.participationTitle3;
                    vm.iparticipationTitle4 = data.participationTitle4;
                    vm.iparticipationTitle5 = data.participationTitle5;
                    vm.iparticipationParagraph1 = data.participationParagraph1;
                    vm.iparticipationParagraph2 = data.participationParagraph2;
                    vm.iparticipationParagraph3 = data.participationParagraph3;
                    vm.iparticipationParagraph4 = data.participationParagraph4;
                    vm.iparticipationParagraph5 = data.participationParagraph5;

                    vm.participationTitle1 = data.participationTitle1;
                    vm.participationTitle2 = data.participationTitle2;
                    vm.participationTitle3 = data.participationTitle3;
                    vm.participationTitle4 = data.participationTitle4;
                    vm.participationTitle5 = data.participationTitle5;
                    vm.participationParagraph1 = data.participationParagraph1;
                    vm.participationParagraph2 = data.participationParagraph2;
                    vm.participationParagraph3 = data.participationParagraph3;
                    vm.participationParagraph4 = data.participationParagraph4;
                    vm.participationParagraph5 = data.participationParagraph5;

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
                participationTitle1: vm.participationTitle1,
                participationTitle2: vm.participationTitle2,
                participationTitle3: vm.participationTitle3,
                participationTitle4: vm.participationTitle4,
                participationTitle5: vm.participationTitle5,
                participationParagraph1: vm.participationParagraph1,
                participationParagraph2: vm.participationParagraph2,
                participationParagraph3: vm.participationParagraph3,
                participationParagraph4: vm.participationParagraph4,
                participationParagraph5: vm.participationParagraph5
            }
            restApi.saveParticipation(newParticipation)
            .success(function (data, status, headers, config) {
                if (data != null && data != "") {

                    vm.temp.participationTitle1 = newParticipation.participationTitle1;
                    vm.temp.participationTitle2 = newParticipation.participationTitle2;
                    vm.temp.participationTitle3 = newParticipation.participationTitle3;
                    vm.temp.participationTitle4 = newParticipation.participationTitle4;
                    vm.temp.participationTitle5 = newParticipation.participationTitle5;
                    vm.temp.participationParagraph1 = newParticipation.participationParagraph1;
                    vm.temp.participationParagraph2 = newParticipation.participationParagraph2;
                    vm.temp.participationParagraph3 = newParticipation.participationParagraph3;
                    vm.temp.participationParagraph4 = newParticipation.participationParagraph4;
                    vm.temp.participationParagraph5 = newParticipation.participationParagraph5;

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