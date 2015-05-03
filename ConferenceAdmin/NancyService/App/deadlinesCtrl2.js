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

        vm.registrationDeadline;
        vm.lateRegistrationDeadline;

        vm.idateFrom;
        vm.idateTo;
        vm.ideadlineTitle1;
        vm.ideadlineParagraph1;

        vm.isponsorDeadline;
        vm.iextendedPaperDeadline;
        vm.iposterDeadline;
        vm.ipanelDeadline;
        vm.iothersDeadline;
        vm.iworkshopDeadline;

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
                    vm.registrationDeadline = data.registrationDeadline;
                    vm.lateRegistrationDeadline = data.lateRegistrationDeadline;
                    vm.ideadlineTitle1 = data.title;
                    vm.ideadlineParagraph1 = data.paragraph;

                    vm.isponsorDeadline = data.sponsorDeadline;
                    vm.iextendedPaperDeadline = data.extendedPaperDeadline;
                    vm.iposterDeadline = data.posterDeadline;
                    vm.ipanelDeadline = data.panelDeadline;
                    vm.iothersDeadline = data.othersDeadline;
                    vm.iworkshopDeadline = data.workshopDeadline;

                    load();
                }
            })

            .error(function (error) {
                load();
                vm.toggleModal('error');
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
                       vm.toggleModal('error');
                   });
        }

        //Avoid flashing when page loads
        var load = function () {
            document.getElementById("body").style.visibility = "visible";
        };

    }
})();