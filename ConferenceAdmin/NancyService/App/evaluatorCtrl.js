(function () {
    'use strict';

    var controllerId = 'evaluatorCtrl';
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', evaluatorCtrl]);

    function evaluatorCtrl($scope, $http, restApi) {
        //Variables
        var vm = this;
        vm.activate = activate;
        vm.title = 'evaluatorCtrl';
        vm.evaluatorsList = [];
        vm.pendingList = [];
        vm.email;
        vm.acceptanceStatus;
        vm.evaluator;
        vm.currentid;
        vm.acceptanceStatusList = ['Accept', 'Reject'];
        vm.searchEvaluator;

        // Functions
        vm.clear = _clear;
        vm.getEvaluatorList = _getEvaluatorList;
        vm.getPendingList = _getPendingList;
        vm.addEvaluator = _addEvaluator;
        vm.updateAcceptanceStatus = _updateAcceptanceStatus;
        vm.next = _next;

        //Get list of evaluators
        _getPendingList();
        _getEvaluatorList();

        function activate() {

        }

        function _clear() {
            vm.email = "";
        }

        function _getPendingList() {
            restApi.getPendingList().
                   success(function (data, status, headers, config) {
                       vm.pendingList = data;
                   }).
                   error(function (data, status, headers, config) {
                       vm.pendingList = data;
                   });
        }

        function _getEvaluatorList() {
            restApi.getEvaluatorList().
                   success(function (data, status, headers, config) {
                       vm.evaluatorsList = data;
                       load();
                   }).
                   error(function (data, status, headers, config) {
                       vm.evaluatorsList = data;
                   });
        }

        function _next() {
            if (vm.email != undefined && vm.email != "") {
                restApi.getNewEvaluator(vm.email)
                    .success(function (data, status, headers, config) {
                        if (data) {
                            //Email found. Add evaluator to the list.
                            _addEvaluator();
                        }
                        else {
                            //Email not found. Show modal displaying error.
                            $("#addError").modal('show');
                        }
                    })

                    .error(function (error) {
                        $("#addError3").modal('show');
                    });
            }
        }


        function _addEvaluator() {
            if (vm.email != undefined && vm.email != "") {
                restApi.postNewEvaluator(vm.email)
                    .success(function (data, status, headers, config) {
                        if (data.email != null) {
                            vm.evaluatorsList.push(data);
                            vm.pendingList.forEach(function (s, index) {
                                if (s.userID == data.userID) {
                                    vm.pendingList.splice(index, 1);
                                }
                            });
                            $("#addEvaluator").modal('hide');
                            _clear();
                            $("#addConfirm").modal('show');
                        }
                        else {
                            $("#addEvaluator").modal('hide');
                            _clear();
                            $("#addError2").modal('show');
                        }
                    })

                    .error(function (error) {
                        $("#addError3").modal('show');
                    });
            }
        }

        function _updateAcceptanceStatus(evaluator) {

            vm.evaluator = JSON.parse(JSON.stringify(evaluator));

            if (vm.evaluator.optionStatus == "Accept") {
                vm.acceptanceStatus = "Accepted";
            }
            else if (vm.evaluator.optionStatus == "Reject") {
                vm.acceptanceStatus = "Rejected";
            }

            if (vm.acceptanceStatus != undefined && vm.acceptanceStatus != "") {
                var changeStatus = { userID: vm.evaluator.userID, acceptanceStatus: vm.acceptanceStatus };
                restApi.updateEvaluatorAcceptanceStatus(changeStatus)
                    .success(function (data, status, headers, config) {
                        if (vm.evaluator.acceptanceStatus == "Pending") {
                            vm.pendingList.forEach(function (s, index) {
                                if (s.userID == vm.evaluator.userID) {
                                    s.acceptanceStatus = vm.acceptanceStatus;
                                    vm.evaluatorsList.push(s);
                                    vm.pendingList.splice(index, 1);
                                    $("#confirmationEvaluatorAcceptanceChange").modal('show');
                                }
                            });
                        }
                        else {
                            vm.evaluatorsList.forEach(function (eva, index) {
                                if (eva.userID == vm.evaluator.userID) {
                                    eva.acceptanceStatus = vm.acceptanceStatus;
                                    $("#confirmationEvaluatorAcceptanceChange").modal('show');
                                }
                            });
                        }
                    })

                    .error(function (error) {
                        $("#editError").modal('show');
                    });
            }
        }

        //Avoid flashing when page loads
        var load = function () {
            document.getElementById("loading-icon").style.visibility = "hidden";
            document.getElementById("body").style.visibility = "visible";
        };

    }
})();