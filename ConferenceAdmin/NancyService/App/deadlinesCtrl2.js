(function () {
    'use strict';

    var controllerId = 'deadlinesCtrl2';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', deadlinesCtrl2]);

    function deadlinesCtrl2($scope, $http, restApi) {
        var vm = this;
        vm.activate = activate;
        vm.title = 'deadlinesCtrl2';

        //Interface
        vm.ideadline1;
        vm.ideadlineDate1;
        vm.ideadline2;
        vm.ideadlineDate2;
        vm.ideadline3;
        vm.ideadlineDate3;
        vm.ideadline4;
        vm.ideadlineDate4;
        vm.ideadline5;
        vm.ideadlineDate5;
        vm.idateFrom;
        vm.idateTo;

        //Functions
        _getInterfaceDeadlines();
        _getDates();

        function activate() {

        }

        function _getInterfaceDeadlines() {
            restApi.getInterfaceDeadlines()
            .success(function (data, status, headers, config) {
                if (data != null && data != "") {
                    vm.ideadline1 = data.deadline1;
                    vm.ideadlineDate1 = data.deadlineDate1;
                    vm.ideadline2 = data.deadline2;
                    vm.ideadlineDate2 = data.deadlineDate2;
                    vm.ideadline3 = data.deadline3;
                    vm.ideadlineDate3 = data.deadlineDate3;
                    vm.ideadline4 = data.deadline4;
                    vm.ideadlineDate4 = data.deadlineDate4;
                    vm.ideadline5 = data.deadline5;
                    vm.ideadlineDate5 = data.deadlineDate5;

                    load();
                }
            })

            .error(function (error) {

            });
        }

        function _getDates() {
            restApi.getDates().
                   success(function (data, status, headers, config) {
                       if (data != null && data != "") {
                           if (data.length > 0) {
                               vm.idateFrom = data[0];
                           }
                           if (data.length > 1) {
                               vm.idateTo = data[1];
                           }
                           if (data.length > 2) {
                               vm.idateTo = data[2];
                           }
                       }
                   }).
                   error(function (data, status, headers, config) {
                   });
        }

        //Avoid flashing when page loads
        var load = function () {
            document.getElementById("body").style.visibility = "visible";
        };

    }
})();