(function () {
    'use strict';

    var controllerId = 'profileSponsorBillCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', '$window', profileSponsorBillCtrl]);

    function profileSponsorBillCtrl($scope, $http, restApi, $window) {
        var vm = this;
        vm.activate = activate;
        //add sponsor fields
        vm.title = 'profileSponsorBillCtrl';
        vm.sponsor;
        vm.loadingComp;
        vm.payment;
        vm.noPaymentMessage = "No content to display.";
        if ($window.sessionStorage.getItem('userID') != null)
            vm.userID = $window.sessionStorage.getItem('userID');
        else {
            $location.path("/Home");
        }

        vm.addComplementaryObj = { sponsorID: 0, quantity: 0, company: "" };
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

        //Functions
        vm.getSponsorPayments = _getSponsorPayments;

        vm.toggleModal = function (action) {


            if (action == "error") {
                vm.obj.title = "Server Error",
               vm.obj.message1 = "Please refresh the page and try again.",
               vm.obj.message2 = "",
               vm.obj.label = "",
               vm.obj.okbutton = true,
               vm.obj.okbuttonText = "OK",
               vm.obj.cancelbutton = false,
               vm.obj.cancelbuttoText = "Cancel",
               vm.showConfirmModal = !vm.showConfirmModal;
            }
        };



        vm.selectedKey = _selectedKey;
        vm.selectedReceipt = _selectedReceipt;
        vm.close = _close;

        activate();

        // Functions
        function activate() {

        
            _getSponsorPayments();

        }

        function _selectedKey(key) {
            vm.key = key;
            vm.keyPop = key.key;
        }
        function _selectedReceipt(payment) {
            vm.payment = payment;
            vm.show = true;
        }
        function _close(){
            vm.show =false;
        }
        function _getSponsorbyID() {
            restApi.getSponsorbyID(vm.userID).
                   success(function (data, status, headers, config) {
                       vm.sponsor = data;
                       vm.CCWICSponsorID = vm.sponsor.sponsorID;
                       _getSponsorComplimentaryKeysFromIndex(vm.sindex);
                   }).
                   error(function (data, status, headers, config) {
                       vm.toggleModal('error');
                       vm.loadingComp = false;
                   });

        }
      

        function _getSponsorPayments() {
            vm.loadingComp = true;
            restApi.getSponsorPayments(vm.userID).
                   success(function (data, status, headers, config) {
                       vm.bills = data;
                       vm.loadingComp = false;
                       if (vm.bills.length ==0)
                           vm.messageVisible = true;
                       else {
                           vm.payment = vm.bills[0];
                       }
                   }).
                   error(function (data, status, headers, config) {
                       vm.toggleModal('error');
                       vm.loadingComp = false;
                        vm.loadingComp = false;
                   });

        }


    }
})();





