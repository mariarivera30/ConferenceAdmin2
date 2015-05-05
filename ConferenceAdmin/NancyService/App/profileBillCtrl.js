(function () {
    'use strict';

    var controllerId = 'profileBillCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi','$window', profileBillCtrl]);

    function profileBillCtrl($scope, $http,restApi, $window) {
        var vm = this;
        vm.message = "No content to display."
        vm.visible = false;
        vm.activate = activate;
        vm.payment;
        vm.loading =false;
        vm.obj={};
        vm.title = 'profileBillCtrl';
        if ($window.sessionStorage.getItem('userID') != null)
            vm.userID = $window.sessionStorage.getItem('userID');
        else {
            $location.path("/Home");
        }

        function activate() {
            
            _getUserPayment();
        }
        activate();

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
        function _getUserPayment() {
            vm.loading = true;
            restApi.getUserPayment(vm.userID)
                   .success(function (data, status, headers, config) {
                       vm.payment = data;
                       vm.loading = false;
                       if (vm.payment == "") {
                           vm.loading = false;
                           vm.visible = false;
                       }
                       else {
                           vm.loading = false;
                           vm.visible = true;
                       
                       }

                   }).
                   error(function (data, status, headers, config) {

                       vm.loading = false;
                       vm.toggleModal('error');
                   });
        }


    }
})();
