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
        vm.conferenceAssistant = {};
        vm.conferenceAccountant = {};

        //Functions
        vm.getCommitteeInterface = _getCommitteeInterface;

        _getCommitteeInterface();

        function activate() {

        }

        function _getCommitteeInterface() {
            restApi.getCommitteeInterface()
           .success(function (data, status, headers, config) {
               vm.conferenceChairList = data.conferenceChairList;
               vm.conferenceCoChairList = data.conferenceCoChairList;
               vm.conferenceCoordinatorList = data.conferenceCoordinatorList;
               vm.conferenceTreasurerList = data.conferenceTreasurerList;
               vm.conferenceAssistant = data.conferenceAssistant;
               vm.conferenceAccountant = data.conferenceAccountant;

               load();
           })
          .error(function (data, status, headers, config) {
          });
        }

        //Avoid flashing when page loads
        var load = function () {
            document.getElementById("body").style.visibility = "visible";
        };
    }
})();