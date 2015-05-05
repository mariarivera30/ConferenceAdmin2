//By: Heidi
(function () {
    'use strict';

    var controllerId = 'committeeCtrl2';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', committeeCtrl2]);

    function committeeCtrl2($scope, $http, restApi) {
        var vm = this;
        vm.activate = activate;
        vm.title = 'committeeCtrl2';
        vm.committee;
        vm.temp;
        vm.loading = false;

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

        //Error modal
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
        vm.reset = _reset;
        vm.getCommittee = _getCommittee;
        vm.saveCommittee = _saveCommittee;

        _getCommittee();

        function activate() {

        }

        //Reload original information
        function _reset() {
            vm.committee = vm.temp;
        }

        //get Committee information
        function _getCommittee() {
            restApi.getCommitteeInterface()
           .success(function (data, status, headers, config) {
               if (data != null && data != "") {
                   vm.temp = data.committee;
                   vm.committee = data.committee;
                   //vm.planningCommitteeList = data;
               }
               load();
           })
          .error(function (data, status, headers, config) {
              load();
              vm.toggleModal('error');
          });
        }

        //update Committee Information
        function _saveCommittee() {
            vm.loading = true;
            var info = {
                    committee: vm.committee
                }
                restApi.saveCommitteeInterface(info)
                .success(function (data, status, headers, config) {
                    if (data) {
                    //update temp
                    vm.temp = info.committee;
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
            document.getElementById("loading-icon").style.visibility = "hidden";
            document.getElementById("body").style.visibility = "visible";
        };
    }
})();