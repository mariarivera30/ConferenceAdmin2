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
        vm.submissionDeadline;
        vm.loading = false;

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

        //Functions
        vm.getDeadlines = _getDeadlines;
        vm.saveDeadlines = _saveDeadlines;
        vm.reset = _reset;

        _getDeadlines();

        function activate() {

        }

        function _reset() {
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
            vm.submissionDeadline = new Date(vm.temp.submissionDeadline.split('/')[2], vm.temp.submissionDeadline.split('/')[0] - 1, vm.temp.submissionDeadline.split('/')[1]);
        }

        function _getDeadlines() {
            restApi.getDeadlines()
            .success(function (data, status, headers, config) {
                if (data != null) {
                    vm.temp = data;
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
                    vm.submissionDeadline = new Date(data.submissionDeadline.split('/')[2], data.submissionDeadline.split('/')[0] - 1, data.submissionDeadline.split('/')[1]);

                    load();
                }
            })

            .error(function (error) {

            });
        }

        function _saveDeadlines() {
            vm.loading = true;
            var d1 = ""; var d2 = ""; var d3 = ""; var d4 = ""; var d5 = ""; var d6 = "";

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

            if (vm.submissionDeadline == null || vm.submissionDeadline == "Invalid Date") {
                vm.submissionDeadline = new Date("");
            }
            else {
                d6 = (vm.submissionDeadline.getUTCMonth() + 1) + "/" + vm.submissionDeadline.getUTCDate() + "/" + vm.submissionDeadline.getUTCFullYear();
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
                submissionDeadline: d6
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