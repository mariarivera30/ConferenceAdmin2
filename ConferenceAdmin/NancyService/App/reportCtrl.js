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
                vm.registrationList = data.results;
                if (vm.rfirstPage) {
                    vm.rfirstPage = false;
                    vm.rmaxIndex = data.maxIndex;
                }
            })
           .error(function (data, status, headers, config) {
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
                vm.sponsorList = data.results;
                if (vm.sfirstPage) {
                    vm.sfirstPage = false;
                    vm.smaxIndex = data.maxIndex;
                }
            })
           .error(function (data, status, headers, config) {
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

            restApi.getBillReport()
            .success(function (data, status, headers, config) {

                vm.totalAmount = data.totalAmount;
                data.report.forEach(function (pay, index) {
                    vm.copy[index] = {
                        "Transaction ID": pay.transactionID,
                        "Date": pay.paymentDate,
                        "Name": pay.name,
                        "Affiliation": pay.affiliation,
                        "User Type": pay.userType,
                        "Amount Paid ($)": pay.amountPaid,
                        "Payment Method": pay.paymentMethod
                    }
                });

                doc = new jsPDF('p', 'pt', 'ledger', true);
                doc.setFont("times", "normal");
                doc.setFontSize(fontSize);
                doc.text(30, 20, "Caribbean Celebration of Women in Computing- Bill Report");
                var d = new Date();
                var n = d.toDateString();
                doc.text(425, 20, n);
                height = doc.drawTable(vm.copy, {
                    xstart: 50,
                    ystart: 50,
                    tablestart: 50,
                    marginright: 40,
                    xOffset: 10,
                    yOffset: 10
                });
                doc.text(50, height + 20, "Total Amount: $" + vm.totalAmount);
                doc.save('billreport.pdf');
            })
           .error(function (data, status, headers, config) {
           });
        }

        //Load
        function _load() {
            document.getElementById("loading-icon").style.visibility = "hidden";
            document.getElementById("body").style.visibility = "visible";
        }
    }
})();