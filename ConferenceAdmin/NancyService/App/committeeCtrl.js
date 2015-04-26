(function () {
    'use strict';

    var controllerId = 'committeeCtrl';

    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', committeeCtrl]);

    function committeeCtrl($scope, $http, restApi) {
        var vm = this;
        vm.activate = activate;
        vm.title = 'committeeCtrl';
        vm.conferenceChairList = {};
        vm.conferenceCoChairList = {};
        vm.conferenceCoordinatorList = {};
        vm.conferenceTreasurerList = {};
        vm.conferenceAssistantList = {};
        vm.conferenceAccountantList = {};

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
        vm.getCommitteeInterface = _getCommitteeInterface;

        _getCommitteeInterface();

        function activate() {

        }

        function _getCommitteeInterface() {
            restApi.getCommitteeInterface()
           .success(function (data, status, headers, config) {
               if (data != null && data != undefined) {
                   vm.conferenceChairList = data.conferenceChairList;
                   vm.conferenceCoChairList = data.conferenceCoChairList;
                   vm.conferenceCoordinatorList = data.conferenceCoordinatorList;
                   vm.conferenceTreasurerList = data.conferenceTreasurerList;
                   vm.conferenceAssistantList = data.conferenceAssistantList;
                   vm.conferenceAccountantList = data.conferenceAccountantList;
               }

               load();
           })
          .error(function (data, status, headers, config) {
              load();
              vm.toggleModal('error');
          });
        }

        //Avoid flashing when page loads
        var load = function () {
            document.getElementById("body").style.visibility = "visible";
        };
    }
})();