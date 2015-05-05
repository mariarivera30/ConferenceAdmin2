(function () {
    'use strict';

    var controllerId = 'deadlinesCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', deadlinesCtrl]);

    function deadlinesCtrl($scope, $http, restApi) {
        var vm = this;
        vm.activate = activate;
        vm.title = 'deadlinesCtrl';

        //Admin
        vm.temp;
        vm.deadline1;
        vm.deadlineDate1;
        vm.deadline2;
        vm.deadlineDate2;
        vm.deadline3;
        vm.deadlineDate3;

        vm.registrationDeadline;
        vm.lateRegistrationDeadline;

        vm.deadlineParagraph1;

        vm.sponsorDeadline;
        vm.extendedPaperDeadline;
        vm.posterDeadline;
        vm.panelDeadline;
        vm.othersDeadline;
        vm.workshopDeadline;

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

        //Error Modal
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
        vm.getDeadlines = _getDeadlines;
        vm.saveDeadlines = _saveDeadlines;
        vm.reset = _reset;

        _getDeadlines();
        
        function activate() {

        }

        //Reload original information
        function _reset() {
            if (vm.temp != null && vm.temp != "") {
                vm.deadline1 = vm.temp.deadline1;
                vm.deadlineDate1 = new Date(vm.temp.deadlineDate1.split('/')[2], vm.temp.deadlineDate1.split('/')[0] - 1, vm.temp.deadlineDate1.split('/')[1]);
                vm.deadline2 = vm.temp.deadline2;
                vm.deadlineDate2 = new Date(vm.temp.deadlineDate2.split('/')[2], vm.temp.deadlineDate2.split('/')[0] - 1, vm.temp.deadlineDate2.split('/')[1]);
                vm.deadline3 = vm.temp.deadline3;
                vm.deadlineDate3 = new Date(vm.temp.deadlineDate3.split('/')[2], vm.temp.deadlineDate3.split('/')[0] - 1, vm.temp.deadlineDate3.split('/')[1]);

                vm.registrationDeadline = new Date(vm.temp.registrationDeadline.split('/')[2], vm.temp.registrationDeadline.split('/')[0] - 1, vm.temp.registrationDeadline.split('/')[1]);
                vm.lateRegistrationDeadline = new Date(vm.temp.lateRegistrationDeadline.split('/')[2], vm.temp.lateRegistrationDeadline.split('/')[0] - 1, vm.temp.lateRegistrationDeadline.split('/')[1]);

                vm.sponsorDeadline = new Date(vm.temp.sponsorDeadline.split('/')[2], vm.temp.sponsorDeadline.split('/')[0] - 1, vm.temp.sponsorDeadline.split('/')[1]);


                vm.deadlineParagraph1 = vm.temp.paragraph;

                //Submission Deadlines
                vm.extendedPaperDeadline = new Date(vm.temp.extendedPaperDeadline.split('/')[2], vm.temp.extendedPaperDeadline.split('/')[0] - 1, vm.temp.extendedPaperDeadline.split('/')[1]);
                vm.posterDeadline = new Date(vm.temp.posterDeadline.split('/')[2], vm.temp.posterDeadline.split('/')[0] - 1, vm.temp.posterDeadline.split('/')[1]);
                vm.panelDeadline = new Date(vm.temp.panelDeadline.split('/')[2], vm.temp.panelDeadline.split('/')[0] - 1, vm.temp.panelDeadline.split('/')[1]);
                vm.othersDeadline = new Date(vm.temp.othersDeadline.split('/')[2], vm.temp.othersDeadline.split('/')[0] -1, vm.temp.othersDeadline.split('/')[1]);
                vm.workshopDeadline = new Date(vm.temp.workshopDeadline.split('/')[2], vm.temp.workshopDeadline.split('/')[0] - 1, vm.temp.workshopDeadline.split('/')[1]);

            }
        }

        //get Conference deadlines, Registration and Submission deadlines
        function _getDeadlines() {
            restApi.getDeadlines()
            .success(function (data, status, headers, config) {
                if (data != null && data != "") {
                    vm.temp = data;

                    vm.deadline1 = data.deadline1;
                    vm.deadlineDate1 = new Date(data.deadlineDate1.split('/')[2], data.deadlineDate1.split('/')[0] - 1, data.deadlineDate1.split('/')[1]); //Date(yyyy,mm-1,dd)
                    vm.deadline2 = data.deadline2;
                    vm.deadlineDate2 = new Date(data.deadlineDate2.split('/')[2], data.deadlineDate2.split('/')[0] - 1, data.deadlineDate2.split('/')[1]);
                    vm.deadline3 = data.deadline3;
                    vm.deadlineDate3 = new Date(data.deadlineDate3.split('/')[2], data.deadlineDate3.split('/')[0] - 1, data.deadlineDate3.split('/')[1]);

                    vm.registrationDeadline = new Date(data.registrationDeadline.split('/')[2], data.registrationDeadline.split('/')[0] - 1, data.registrationDeadline.split('/')[1]);
                    vm.lateRegistrationDeadline = new Date(data.lateRegistrationDeadline.split('/')[2], data.lateRegistrationDeadline.split('/')[0] - 1, data.lateRegistrationDeadline.split('/')[1]);

                    vm.sponsorDeadline = new Date(data.sponsorDeadline.split('/')[2], data.sponsorDeadline.split('/')[0] - 1, data.sponsorDeadline.split('/')[1]);

                    vm.deadlineParagraph1 = data.paragraph;

                    //Papers Deadlines
                    vm.extendedPaperDeadline = new Date(data.extendedPaperDeadline.split('/')[2], data.extendedPaperDeadline.split('/')[0] - 1, data.extendedPaperDeadline.split('/')[1]);
                    vm.posterDeadline = new Date(data.posterDeadline.split('/')[2], data.posterDeadline.split('/')[0] - 1, data.posterDeadline.split('/')[1]);
                    vm.panelDeadline = new Date(data.panelDeadline.split('/')[2], data.panelDeadline.split('/')[0] - 1, data.panelDeadline.split('/')[1]);
                    vm.othersDeadline = new Date(data.othersDeadline.split('/')[2], data.othersDeadline.split('/')[0] - 1, data.othersDeadline.split('/')[1]);
                    vm.workshopDeadline = new Date(data.workshopDeadline.split('/')[2], data.workshopDeadline.split('/')[0] - 1, data.workshopDeadline.split('/')[1]);

                    load();
                }
            })

            .error(function (error) {
                load();
                vm.toggleModal('error');
            });
        }

        //update conference deadlines
        function _saveDeadlines() {

            vm.loading = true;

            var d1 = "", d2 = "", d3 = "", d4 = "", d5 = "", d6 = "";

            var s1 = "", s2 = "", s3 = "", s4 = "", s5 = "";

            //Format dates to acceptable string format

            if (vm.deadlineDate1 == null || vm.deadlineDate1 == "Invalid Date") {
                vm.deadlineDate1 = new Date("");
            }
            else {
                d1 = (vm.deadlineDate1.getUTCMonth() + 1) + "/" + vm.deadlineDate1.getUTCDate() + "/" + vm.deadlineDate1.getUTCFullYear();
            }

            if (vm.deadlineDate2 == null || vm.deadlineDate2 == "Invalid Date") {
                vm.deadlineDate2 = new Date("");
            }
            else {
                d2 = (vm.deadlineDate2.getUTCMonth() + 1) + "/" + vm.deadlineDate2.getUTCDate() + "/" + vm.deadlineDate2.getUTCFullYear();
            }

            if (vm.deadlineDate3 == null || vm.deadlineDate3 == "Invalid Date") {
                vm.deadlineDate3 = new Date("");
            }
            else {
                d3 = (vm.deadlineDate3.getUTCMonth() + 1) + "/" + vm.deadlineDate3.getUTCDate() + "/" + vm.deadlineDate3.getUTCFullYear();
            }

            if (vm.registrationDeadline == null || vm.registrationDeadline == "Invalid Date") {
                vm.registrationDeadline = new Date("");
            }
            else {
                d4 = (vm.registrationDeadline.getUTCMonth() + 1) + "/" + vm.registrationDeadline.getUTCDate() + "/" + vm.registrationDeadline.getUTCFullYear();
            }

            if (vm.lateRegistrationDeadline == null || vm.lateRegistrationDeadline == "Invalid Date") {
                vm.lateRegistrationDeadline = new Date("");
            }
            else {
                d5 = (vm.lateRegistrationDeadline.getUTCMonth() + 1) + "/" + vm.lateRegistrationDeadline.getUTCDate() + "/" + vm.lateRegistrationDeadline.getUTCFullYear();
            }

            if (vm.sponsorDeadline == null || vm.sponsorDeadline == "Invalid Date") {
                vm.sponsorDeadline = new Date("");
            }
            else {
                d6 = (vm.sponsorDeadline.getUTCMonth() + 1) + "/" + vm.sponsorDeadline.getUTCDate() + "/" + vm.sponsorDeadline.getUTCFullYear();
            }

             //Submission deadlines
            if (vm.extendedPaperDeadline == null || vm.extendedPaperDeadline == "Invalid Date") {
                vm.extendedPaperDeadline = new Date("");
            }
            else {
                s1 = (vm.extendedPaperDeadline.getUTCMonth() + 1) + "/" + vm.extendedPaperDeadline.getUTCDate() + "/" + vm.extendedPaperDeadline.getUTCFullYear();
            }

            if (vm.posterDeadline == null || vm.posterDeadline == "Invalid Date") {
                vm.posterDeadline = new Date("");
            }
            else {
                s2 = (vm.posterDeadline.getUTCMonth() + 1) + "/" + vm.posterDeadline.getUTCDate() + "/" + vm.posterDeadline.getUTCFullYear();
            }

            if (vm.panelDeadline == null || vm.panelDeadline == "Invalid Date") {
                vm.panelDeadline = new Date("");
            }
            else {
                s3 = (vm.panelDeadline.getUTCMonth() + 1) + "/" + vm.panelDeadline.getUTCDate() + "/" + vm.panelDeadline.getUTCFullYear();
            }

            if (vm.othersDeadline == null || vm.othersDeadline == "Invalid Date") {
                vm.othersDeadline = new Date("");
            }
            else {
                s4 = (vm.othersDeadline.getUTCMonth() + 1) + "/" + vm.othersDeadline.getUTCDate() + "/" + vm.othersDeadline.getUTCFullYear();
            }

            if (vm.workshopDeadline == null || vm.workshopDeadline == "Invalid Date") {
                vm.workshopDeadline = new Date("");
            }
            else {
                s5 = (vm.workshopDeadline.getUTCMonth() + 1) + "/" + vm.workshopDeadline.getUTCDate() + "/" + vm.workshopDeadline.getUTCFullYear();
            }

            var newDeadlines = {
                deadline1: vm.deadline1,
                deadlineDate1: d1,
                deadline2: vm.deadline2,
                deadlineDate2: d2,
                deadline3: vm.deadline3,
                deadlineDate3: d3,
                registrationDeadline: d4,
                lateRegistrationDeadline: d5,
                sponsorDeadline: d6,
                extendedPaperDeadline: s1,
                posterDeadline:s2,
                panelDeadline:s3,
                othersDeadline: s4,
                workshopDeadline: s5,
                paragraph: vm.deadlineParagraph1
            }
            //alert(vm.deadlineDate1.toLocaleDateString());
            //alert((vm.deadlineDate1.getUTCMonth()+1) + "/" + vm.deadlineDate1.getUTCDate() + "/" + vm.deadlineDate1.getUTCFullYear());

            restApi.saveDeadlines(newDeadlines)
            .success(function (data, status, headers, config) {
                if (data) {
                    vm.temp = newDeadlines;
                    $("#updateConfirm").modal('show');
                }
                vm.loading = false;
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