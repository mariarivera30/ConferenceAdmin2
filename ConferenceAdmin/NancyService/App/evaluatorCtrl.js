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

        //Evaluator List Variables (Paging)
        vm.evaluatorsList = [];
        vm.eindex = 0;  //Page index [Goes from 0 to emaxIndex-1]
        vm.emaxIndex = 0;   //Max page number
        vm.efirstPage = true;

        //Search List Variables (Paging)
        vm.searchList = [];
        vm.searchIndex = 0;  //Page index [Goes from 0 to pmaxIndex-1]
        vm.searchMaxIndex = 0;   //Max page number
        vm.criteria;
        vm.showSearch = false;
        vm.showResults = false;

        //Pending List Variables (Paging)
        vm.pendingList = [];
        vm.pindex = 0;  //Page index [Goes from 0 to pmaxIndex-1]
        vm.pmaxIndex = 0;   //Max page number
        vm.pfirstPage = true;

        //General Variables
        vm.email;
        vm.acceptanceStatus;
        vm.evaluator;
        vm.currentid;
        vm.acceptanceStatusList = ['Accept', 'Reject'];
        vm.searchEvaluator;

        //Functions- Past Evaluators (Paging)
        vm.getEvaluatorListFromIndex = _getEvaluatorListFromIndex;
        vm.previousEvaluator = _previousEvaluator;
        vm.nextEvaluator = _nextEvaluator;
        vm.getFirstEvaluatorPage = _getFirstEvaluatorPage;
        vm.getLastEvaluatorPage = _getLastEvaluatorPage;

        //Functions- Pending Evaluators (Paging)
        vm.getPendingListFromIndex = _getPendingListFromIndex;
        vm.previousPending = _previousPending;
        vm.nextPending = _nextPending;
        vm.getFirstPendingPage = _getFirstPendingPage;
        vm.getLastPendingPage = _getLastPendingPage;

        //Functions- Search (Paging)
        vm.search = _searchEvaluators;
        vm.previousSearch = _previousSearch;
        vm.nextSearch = _nextSearch;
        vm.getFirstSearch = _getFirstSearch;
        vm.getLastSearch = _getLastSearch;
        vm.back = _back;

        //General Functions
        vm.clear = _clear;
        vm.addEvaluator = _addEvaluator;
        vm.updateAcceptanceStatus = _updateAcceptanceStatus;
        vm.next = _next;
        vm.load = _load;

        activate();

        function activate() {
            _getPendingListFromIndex(vm.pindex);
        }

        function _clear() {
            vm.email = "";
        }

        //Pending List Methods
        function _getPendingListFromIndex(index) {
            restApi.getPendingListFromIndex(index)
            .success(function (data, status, headers, config) {
                vm.pmaxIndex = data.maxIndex;
                if (vm.pmaxIndex == 0) {
                    vm.pindex = 0;
                    vm.pendingList = [];
                }
                else if (vm.pindex >= vm.pmaxIndex) {
                    vm.pindex = vm.pmaxIndex - 1;
                    _getPendingListFromIndex(vm.pindex);
                }
                else {
                    vm.pendingList = data.results;
                }

                _getEvaluatorListFromIndex(vm.eindex);

                /*if (vm.pfirstPage) {
                    vm.pfirstPage = false;
                    vm.pmaxIndex = data.maxIndex;
                }*/
            })
           .error(function (data, status, headers, config) {
           });
        }

        function _nextPending() {
            if (vm.pindex < vm.pmaxIndex - 1) {
                vm.pindex += 1;
                _getPendingListFromIndex(vm.pindex);
            }
        }

        function _previousPending() {
            if (vm.pindex > 0) {
                vm.pindex -= 1;
                _getPendingListFromIndex(vm.pindex);
            }
        }

        function _getFirstPendingPage() {
            vm.pindex = 0;
            _getPendingListFromIndex(vm.pindex);
        }

        function _getLastPendingPage() {
            vm.pindex = vm.pmaxIndex - 1;
            _getPendingListFromIndex(vm.pindex);
        }

        //Past Evaluator List Methods
        function _getEvaluatorListFromIndex(index) {
            restApi.getEvaluatorListFromIndex(index)
            .success(function (data, status, headers, config) {
                vm.emaxIndex = data.maxIndex;
                if (vm.emaxIndex == 0) {
                    vm.eindex = 0;
                    vm.evaluatorsList = [];
                }
                else if (vm.eindex >= vm.emaxIndex) {
                    vm.eindex = vm.emaxIndex - 1;
                    _getEvaluatorListFromIndex(vm.eindex);
                }
                else {
                    vm.evaluatorsList = data.results;
                }

                _load();

                /*if (vm.efirstPage) {
                    vm.efirstPage = false;
                    vm.emaxIndex = data.maxIndex;
                }*/
            })
           .error(function (data, status, headers, config) {
           });
        }

        function _nextEvaluator() {
            if (vm.eindex < vm.emaxIndex - 1) {
                vm.eindex += 1;
                _getEvaluatorListFromIndex(vm.eindex);
            }
        }

        function _previousEvaluator() {
            if (vm.eindex > 0) {
                vm.eindex -= 1;
                _getEvaluatorListFromIndex(vm.eindex);
            }
        }

        function _getFirstEvaluatorPage() {
            vm.eindex = 0;
            _getEvaluatorListFromIndex(vm.eindex);
        }

        function _getLastEvaluatorPage() {
            vm.eindex = vm.emaxIndex - 1;
            _getEvaluatorListFromIndex(vm.eindex);
        }

        //General Methods
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
                            //vm.evaluatorsList.push(data);
                            _getEvaluatorListFromIndex(vm.eindex);
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
                                    //vm.evaluatorsList.push(s);
                                    vm.pendingList.splice(index, 1);
                                    _getEvaluatorListFromIndex(vm.eindex);
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

        //Search Methods
        function _searchEvaluators(index) {
            if (vm.criteria != "" && vm.criteria != null) {
                var info = { index: index, criteria: vm.criteria };
                restApi.searchEvaluators(info).
                       success(function (data, status, headers, config) {
                           vm.showSearch = true;
                           vm.searchMaxIndex = data.maxIndex;
                           if (vm.searchMaxIndex == 0) {
                               vm.searchIndex = 0;
                               vm.searchResults = [];
                               vm.showResults = false;
                           }
                           else if (vm.searchIndex >= vm.searchMaxIndex) {
                               vm.searchIndex = vm.searchMaxIndex - 1;
                               _searchEvaluators(vm.searchIndex);
                           }
                           else {
                               vm.showResults = true;
                               vm.searchResults = data.results;
                           }
                       }).
                       error(function (data, status, headers, config) {
                       });
            }
        }

        function _nextSearch() {
            if (vm.searchIndex < vm.searchMaxIndex - 1) {
                vm.searchIndex += 1;
                _searchEvaluators(vm.searchIndex);
            }
        }

        function _previousSearch() {
            if (vm.searchIndex > 0) {
                vm.searchIndex -= 1;
                _searchEvaluators(vm.searchIndex);
            }
        }

        function _getFirstSearch() {
            vm.searchIndex = 0;
            _searchEvaluators(vm.searchIndex);
        }

        function _getLastSearch() {
            vm.searchIndex = vm.searchMaxIndex - 1;
            _searchEvaluators(vm.searchIndex);
        }

        function _back() {
            vm.criteria = "";
            vm.searchIndex = 0;
            vm.searchResults = [];
            vm.showSearch = false;
            vm.showResults = false;
        }

        //Avoid flashing when page loads
        function _load() {
            document.getElementById("loading-icon").style.visibility = "hidden";
            document.getElementById("body").style.visibility = "visible";
        }

    }
})();