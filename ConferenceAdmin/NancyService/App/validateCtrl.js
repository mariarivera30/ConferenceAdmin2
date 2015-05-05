(function () {
    'use strict';

    var controllerId = 'validateCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$rootScope', '$http', 'restApi', '$window', '$location','$state', validateCtrl]);

    function validateCtrl($scope, $rootScope, $http, restApi, $window, $location, $state) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'loginCtrl';
        //Login fields
        vm.firstname;
        vm.lastname;
        vm.email;
        vm.password;
        vm.token;
        vm.profileButton = false;


        // Functions
       
        vm.validate = _validate;
     
        activate();

        function activate() {
            vm.keyConfirmation = $state.params.key;
           
        }


  
    

        function _validate() {
           
            vm.creatingUser = true;
            if (vm.keyConfirmation != undefined) {
                restApi.accountConfirmation(vm.keyConfirmation).
                       success(function (data, status, headers, config) {
                           if (data == "") {
                               vm.message = "Please verify your confirmation Key.";
                               vm.keyConfirmation = "";
                               vm.creatingUser = false;


                           }
                           else if (data == "wasValidated") {
                               vm.message = "This account is already validated.";
                               vm.keyConfirmation = "";
                               vm.creatingUser = false;


                           }
                           else {
                               $rootScope.$emit('popUp', 'confirm');
                               vm.creatingUser = false;

                           }
                       }).
                       error(function (data, status, headers, config) {
                           $rootScope.$emit('popUp', 'error');
                           vm.creatingUser = false;
                       });
            }
        }

       

       

       

    }
})();
