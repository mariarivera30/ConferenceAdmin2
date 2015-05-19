(function () {
    'use strict';

    var controllerId = 'paymentReceiptCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', '$rootScope', "$stateParams","$state", paymentReceiptCtrl]);

    function paymentReceiptCtrl($scope, $http,restApi, $rootScope, $stateParams, $state) {
        var vm = this;
        vm.paymentID;
        vm.activate = activate;
        vm.obj = {};
        vm.message;
        vm.title = 'paymentReceiptCtrl';


        //functions 
        vm.getPayment = _getPayment;
    

        activate();
        function activate() {
      
        }
        
       _getPayment();
          
          

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
     

       function _getPayment() {

           vm.paymentid = $state.params.paymentId;
           if (vm.paymentid != undefined) {
               vm.loading = true;
               restApi.getPayment(vm.paymentid)
                      .success(function (data, status, headers, config) {
                          if (data != null) {
                              if (data.paymentBillID != -1) {

                                  vm.payment = data;
                                  vm.loading = false;
                              }

                              else {
                                  vm.message = true;
                                  vm.loading = false;
                              }
                          }
                         

                      }).
                      error(function (data, status, headers, config) {

                          vm.loading = false;
                          vm.toggleModal('error');
                      });
           }
       }
        


    }
})();
