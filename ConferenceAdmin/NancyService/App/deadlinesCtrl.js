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
        vm.deadline4;
        vm.deadlineDate4;
        vm.deadline5;
        vm.deadlineDate5;

        vm.sponsorDeadline;
        vm.extendedPaperDeadline;
        vm.posterDeadline;
        vm.panelDeadline;
        vm.bofDeadline;
        vm.workshopDeadline;

        vm.loading = false;

         //Functions
        vm.getDeadlines = _getDeadlines;
        vm.saveDeadlines = _saveDeadlines;
        vm.reset = _reset;

        _getDeadlines();
        
        function activate() {

        }

        function _reset() {
            if (vm.temp != null && vm.temp != "") {
                vm.deadline1 = vm.temp.deadline1;
                vm.deadlineDate1 = new Date(vm.temp.deadlineDate1.split('/')[2], vm.temp.deadlineDate1.split('/')[0] - 1, vm.temp.deadlineDate1.split('/')[1]);
                vm.deadline2 = vm.temp.deadline2;
                vm.deadlineDate2 = new Date(vm.temp.deadlineDate2.split('/')[2], vm.temp.deadlineDate2.split('/')[0] - 1, vm.temp.deadlineDate2.split('/')[1]);
                vm.deadline3 = vm.temp.deadline3;
                vm.deadlineDate3 = new Date(vm.temp.deadlineDate3.split('/')[2], vm.temp.deadlineDate3.split('/')[0] - 1, vm.temp.deadlineDate3.split('/')[1]);
                vm.deadline4 = vm.temp.deadline4;
                vm.deadlineDate4 = new Date(vm.temp.deadlineDate4.split('/')[2], vm.temp.deadlineDate4.split('/')[0] - 1, vm.temp.deadlineDate4.split('/')[1]);
                vm.deadline5 = vm.temp.deadline5;
                vm.deadlineDate5 = new Date(vm.temp.deadlineDate5.split('/')[2], vm.temp.deadlineDate5.split('/')[0] - 1, vm.temp.deadlineDate5.split('/')[1]);
                vm.sponsorDeadline = new Date(vm.temp.sponsorDeadline.split('/')[2], vm.temp.sponsorDeadline.split('/')[0] - 1, vm.temp.sponsorDeadline.split('/')[1]);

                //Papers Deadlines
                vm.extendedPaperDeadline = new Date(vm.temp.extendedPaperDeadline.split('/')[2], vm.temp.extendedPaperDeadline.split('/')[0] - 1, vm.temp.extendedPaperDeadline.split('/')[1]);
                vm.posterDeadline = new Date(vm.temp.posterDeadline.split('/')[2], vm.temp.posterDeadline.split('/')[0] - 1, vm.temp.posterDeadline.split('/')[1]);
                vm.panelDeadline = new Date(vm.temp.panelDeadline.split('/')[2], vm.temp.panelDeadline.split('/')[0] - 1, vm.temp.panelDeadline.split('/')[1]);
                vm.bofDeadline = new Date(vm.temp.bofDeadline.split('/')[2], vm.temp.bofDeadline.split('/')[0] - 1, vm.temp.bofDeadline.split('/')[1]);
                vm.workshopDeadline = new Date(vm.temp.workshopDeadline.split('/')[2], vm.temp.workshopDeadline.split('/')[0] - 1, vm.temp.workshopDeadline.split('/')[1]);

            }
        }

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
                    vm.deadline4 = data.deadline4;
                    vm.deadlineDate4 = new Date(data.deadlineDate4.split('/')[2], data.deadlineDate4.split('/')[0] - 1, data.deadlineDate4.split('/')[1]);
                    vm.deadline5 = data.deadline5;
                    vm.deadlineDate5 = new Date(data.deadlineDate5.split('/')[2], data.deadlineDate5.split('/')[0] - 1, data.deadlineDate5.split('/')[1]);
                    vm.sponsorDeadline = new Date(data.sponsorDeadline.split('/')[2], data.sponsorDeadline.split('/')[0] - 1, data.sponsorDeadline.split('/')[1]);

                    //Papers Deadlines
                    vm.extendedPaperDeadline = new Date(data.extendedPaperDeadline.split('/')[2], data.extendedPaperDeadline.split('/')[0] - 1, data.extendedPaperDeadline.split('/')[1]);
                    vm.posterDeadline = new Date(data.posterDeadline.split('/')[2], data.posterDeadline.split('/')[0] - 1, data.posterDeadline.split('/')[1]);
                    vm.panelDeadline = new Date(data.panelDeadline.split('/')[2], data.panelDeadline.split('/')[0] - 1, data.panelDeadline.split('/')[1]);
                    vm.bofDeadline = new Date(data.bofDeadline.split('/')[2], data.bofDeadline.split('/')[0] - 1, data.bofDeadline.split('/')[1]);
                    vm.workshopDeadline = new Date(data.workshopDeadline.split('/')[2], data.workshopDeadline.split('/')[0] - 1, data.workshopDeadline.split('/')[1]);

                    load();
                }
            })

            .error(function (error) {

            });
        }

         function _saveDeadlines() {
             vm.loading = true;

            var d1 = "", d2 = "", d3 = "", d4 = "", d5 = "", d6 = "";

            var s1 = "", s2 = "", s3 = "", s4 = "", s5 = "";

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

            if (vm.deadlineDate4 == null || vm.deadlineDate4 == "Invalid Date") {
                vm.deadlineDate4 = new Date("");
            }
            else {
                d4 = (vm.deadlineDate4.getUTCMonth() + 1) + "/" + vm.deadlineDate4.getUTCDate() + "/" + vm.deadlineDate4.getUTCFullYear();
            }

            if (vm.deadlineDate5 == null || vm.deadlineDate5 == "Invalid Date") {
                vm.deadlineDate5 = new Date("");
            }
            else {
                d5 = (vm.deadlineDate5.getUTCMonth() + 1) + "/" + vm.deadlineDate5.getUTCDate() + "/" + vm.deadlineDate5.getUTCFullYear();
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

            if (vm.bofDeadline == null || vm.bofDeadline == "Invalid Date") {
                vm.bofDeadline = new Date("");
            }
            else {
                s4 = (vm.bofDeadline.getUTCMonth() + 1) + "/" + vm.bofDeadline.getUTCDate() + "/" + vm.bofDeadline.getUTCFullYear();
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
                deadline4: vm.deadline4,
                deadlineDate4: d4,
                deadline5: vm.deadline5,
                deadlineDate5: d5,
                sponsorDeadline: d6,
                extendedPaperDeadline: s1,
                posterDeadline:s2,
                panelDeadline:s3,
                bofDeadline:s4,
                workshopDeadline:s5
            }
            //alert(vm.deadlineDate1.toLocaleDateString());
            //alert((vm.deadlineDate1.getUTCMonth()+1) + "/" + vm.deadlineDate1.getUTCDate() + "/" + vm.deadlineDate1.getUTCFullYear());

            restApi.saveDeadlines(newDeadlines)
            .success(function (data, status, headers, config) {
                if (data != null && data != "") {
                    vm.temp = newDeadlines;
                    $("#updateConfirm").modal('show');
                }
                vm.loading = false;
            })
            .error(function (error) {
                vm.loading = false;
                $("#updateError").modal('show');
            });
        }

         //Avoid flashing when page loads
        var load = function () {
            document.getElementById("body").style.visibility = "visible";
        };

    }
})();