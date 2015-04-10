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
        vm.payList = [];
        vm.copy = [];
        var fontSize = 8, height = 0, doc;

        // Functions
        vm.downloadBillReport = _downloadBillReport;
        vm.getReportList = _getReportList;
        vm.load = _load;
        _getReportList();

        function activate() {
            $('#billTable').dataTable({
                "pagingType": "full_numbers"
            });

            vm.payList.forEach(function (pay, index) {
                vm.copy[index] = {
                    "Transaction ID": pay.transactionID,
                    "Date": pay.paymentDate,
                    "Name": pay.name,
                    "Affiliation": pay.affiliation,
                    "User Type": pay.userType,
                    "Amount Paid": pay.amountPaid,
                    "Payment Method": pay.paymentMethod
                }
            });

            //_generateBillReport(copy);
        }

        function _getReportList() {
            restApi.getBillReport()
            .success(function (data, status, headers, config) {
                vm.payList = data;
                setTimeout(function () {
                    activate();
                    _load();
                }, 1000);
            })
           .error(function (data, status, headers, config) {
               vm.payList = data;
           });
        }

        /*function _generateBillReport(data) {
            doc = new jsPDF('p', 'pt', 'ledger', true);
            doc.setFont("times", "normal");
            doc.setFontSize(fontSize);
            doc.text(30, 20, "Caribbean Celebration of Women in Computing- Bill Report");
            var d = new Date();
            var n = d.toDateString();
            doc.text(425, 20, n);
            height = doc.drawTable(data, {
                xstart: 50,
                ystart: 50,
                tablestart: 50,
                marginright: 40,
                xOffset: 10,
                yOffset: 10
            });
            doc.text(50, height + 20, '');
        }*/

        function _downloadBillReport() {
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
            doc.text(50, height + 20, '');
            doc.save('billreport.pdf');
        }

        function _load() {
            document.getElementById("loading-icon").style.visibility = "hidden";
            document.getElementById("body").style.visibility = "visible";
        }
    }
})();