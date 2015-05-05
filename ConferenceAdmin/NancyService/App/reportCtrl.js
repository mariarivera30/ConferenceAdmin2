(function () {
    'use strict';

    var controllerId = 'reportCtrl';
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', reportCtrl]);

    function reportCtrl($scope, $http, restApi) {
        //Variables
        var vm = this;
        vm.activate = activate;
        vm.title = 'reportCtrl'; 

        //Report Variables
        vm.copy = [];
        vm.totalAmount;
        var fontSize = 8, height = 0, doc;
        vm.downloadLoading = false;
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

        //Registration Payments- Variables (Paging)
        vm.registrationList = []; //Results to Display
        vm.rindex = 0;  //Page index [Goes from 0 to rmaxIndex-1]
        vm.rmaxIndex = 0;   //Max page number
        vm.rfirstPage = true;

        //Sponsor Payments- Variables (Paging)
        vm.sponsorList = []; //Results to Display
        vm.sindex = 0;  //Page index [Goes from 0 to smaxIndex-1]
        vm.smaxIndex = 0;   //Max page number
        vm.sfirstPage = true;

        //Search List Variables (Paging)
        vm.searchList = [];
        vm.searchIndex = 0;  //Page index [Goes from 0 to pmaxIndex-1]
        vm.searchMaxIndex = 0;   //Max page number
        vm.criteria;
        vm.showSearch = false;
        vm.showResults = false;
        vm.loadingSearch = false;

        // Functions- General
        vm.downloadBillReport = _downloadBillReport;
        vm.load = _load;

        //Functions- Registration (Paging)
        vm.getRegistrationListFromIndex = _getRegistrationListFromIndex;
        vm.previousRegistration = _previousRegistration;
        vm.nextRegistration = _nextRegistration;
        vm.getFirstRegistrationPage = _getFirstRegistrationPage;
        vm.getLastRegistrationPage = _getLastRegistrationPage;

        //Functions- Sponsors (Paging)
        vm.getSponsorListFromIndex = _getSponsorListFromIndex;
        vm.previousSponsor = _previousSponsor;
        vm.nextSponsor = _nextSponsor;
        vm.getFirstSponsorPage = _getFirstSponsorPage;
        vm.getLastSponsorPage = _getLastSponsorPage;

        //Functions- Search (Paging)
        vm.search = _search;
        vm.previousSearch = _previousSearch;
        vm.nextSearch = _nextSearch;
        vm.getFirstSearch = _getFirstSearch;
        vm.getLastSearch = _getLastSearch;
        vm.back = _back;

        activate();

        function activate() {
            _getRegistrationListFromIndex(vm.rindex);
            _getSponsorListFromIndex(vm.sindex);
            _load();
        }

        //Registration Methods
        function _getRegistrationListFromIndex(index) {
            restApi.getRegistrationPaymentsFromIndex(index)
            .success(function (data, status, headers, config) {
                if (data != null && data != "") {
                    vm.rmaxIndex = data.maxIndex;
                    if (vm.rmaxIndex == 0) {
                        vm.rindex = 0;
                        vm.registrationList = [];
                    }
                    else if (vm.rindex >= vm.rmaxIndex) {
                        vm.rindex = vm.rmaxIndex - 1;
                        _getRegistrationListFromIndex(vm.rindex);
                    }
                    else {
                        vm.registrationList = data.results;
                    }
                }
                
            })
           .error(function (data, status, headers, config) {
               vm.toggleModal('error');
           });
        }

        function _nextRegistration() {
            if (vm.rindex < vm.rmaxIndex-1) {
                vm.rindex += 1;
                _getRegistrationListFromIndex(vm.rindex);
            }
        }

        function _previousRegistration() {
            if (vm.rindex > 0) {
                vm.rindex -= 1;
                _getRegistrationListFromIndex(vm.rindex);
            }
        }

        function _getFirstRegistrationPage() {
            vm.rindex = 0;
            _getRegistrationListFromIndex(vm.rindex);
        }

        function _getLastRegistrationPage() {
            vm.rindex = vm.rmaxIndex - 1;
            _getRegistrationListFromIndex(vm.rindex);
        }

        //Sponsor Methods
        function _getSponsorListFromIndex(index) {
            restApi.getSponsorPaymentsFromIndex(index)
            .success(function (data, status, headers, config) {
                if (data != null && data != "") {
                    vm.smaxIndex = data.maxIndex;
                    if (vm.smaxIndex == 0) {
                        vm.sindex = 0;
                        vm.sponsorList = [];
                    }
                    else if (vm.sindex >= vm.smaxIndex) {
                        vm.sindex = vm.smaxIndex - 1;
                        _getSponsorListFromIndex(vm.sindex);
                    }
                    else {
                        vm.sponsorList = data.results;
                    }
                    /*if (vm.sfirstPage) {
                        vm.sfirstPage = false;
                        vm.smaxIndex = data.maxIndex;
                    }*/
                }
            })
           .error(function (data, status, headers, config) {
               vm.toggleModal('error');
           });
        }

        function _nextSponsor() {
            if (vm.sindex < vm.smaxIndex - 1) {
                vm.sindex += 1;
                _getSponsorListFromIndex(vm.sindex);
            }
        }

        function _previousSponsor() {
            if (vm.sindex > 0) {
                vm.sindex -= 1;
                _getSponsorListFromIndex(vm.sindex);
            }
        }

        function _getFirstSponsorPage() {
            vm.sindex = 0;
            _getSponsorListFromIndex(vm.sindex);
        }

        function _getLastSponsorPage() {
            vm.sindex = vm.smaxIndex - 1;
            _getSponsorListFromIndex(vm.sindex);
        }

        //Report Method
        function _downloadBillReport() {
            vm.loading = true;
            vm.downloadLoading = true;
            restApi.getBillReport()
            .success(function (data, status, headers, config) {
                if (data != null && data != "") {
                    var report = data.results;
                    vm.downloadLoading = false;
                    vm.loading = false;

                    if (report != "" && report != undefined) {
                        var blob = new Blob([report], { type: "text/plain;charset=utf-8" });
                        saveAs(blob, "billreport.csv");
                    }
                }
            })
           .error(function (data, status, headers, config) {
               vm.loading = false;
               vm.downloadLoading = false;
               vm.toggleModal('error');
           });
        }

        //Search Methods
        function _search(index) {
            if (vm.criteria != "" && vm.criteria != null) {
                vm.loadingSearch = true;
                var info = { index: index, criteria: vm.criteria };
                restApi.searchReport(info).
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
                               _search(vm.searchIndex);
                           }
                           else {
                               vm.showResults = true;
                               vm.searchResults = data.results;
                           }
                           vm.loadingSearch = false;
                       }).
                       error(function (data, status, headers, config) {
                           _back;
                           vm.toggleModal('error');
                       });
            }
        }

        function _nextSearch() {
            if (vm.searchIndex < vm.searchMaxIndex - 1) {
                vm.searchIndex += 1;
                _search(vm.searchIndex);
            }
        }

        function _previousSearch() {
            if (vm.searchIndex > 0) {
                vm.searchIndex -= 1;
                _search(vm.searchIndex);
            }
        }

        function _getFirstSearch() {
            vm.searchIndex = 0;
            _search(vm.searchIndex);
        }

        function _getLastSearch() {
            vm.searchIndex = vm.searchMaxIndex - 1;
            _search(vm.searchIndex);
        }

        //Clear search results
        function _back() {
            vm.criteria = "";
            vm.searchIndex = 0;
            vm.searchResults = [];
            vm.showSearch = false;
            vm.showResults = false;
            vm.loadingSearch = false;
        }

        //Load
        function _load() {
            document.getElementById("loading-icon").style.visibility = "hidden";
            document.getElementById("body").style.visibility = "visible";
        }
    }
})();