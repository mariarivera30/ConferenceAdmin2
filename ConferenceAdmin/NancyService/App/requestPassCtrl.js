(function () {
    'use strict';

    var controllerId = 'requestPassCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', '$window', '$location', requestPassCtrl]);

    function requestPassCtrl($scope, $http, restApi, $window, $location) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'requestPassCtrl';
        //Login fields
        vm.firstname;
        vm.lastname;
        vm.email;
        vm.password;
        vm.token;
        vm.profileButton = false;
        vm.message = "";




        // Functions

        vm.requestPass = _requestPass;
        vm.checkEmail= _checkEmail;

        activate();

        function activate() {
            _getUserTypes()

        }
        function _checkEmail() {
            restApi.checkEmail(vm.email).
                   success(function (data, status, headers, config) {
                       if (data == false) {
                          vm.message ="This email is not registered."
                           vm.emailExist = false;
                          
                       }
                       else {
                           vm.emailExist = true;
                           _requestPass();
                       
                        }

                   }).
                   error(function (data, status, headers, config) {

                   });
        }


        function _requestPass() {
            if (vm.emailExist) {
                restApi.requestPass(vm.email).
                       success(function (data, status, headers, config) {
                           alert("Your temporary password has been sent to " + vm.email);
                           $location.path("/Login/Log");
                       }).
                       error(function (data, status, headers, config) {

                       });
            }

        }



        function _getUserTypes() {
            restApi.getUserTypes().
                   success(function (data, status, headers, config) {
                       // this callback will be called asynchronously
                       // when the response is available
                       vm.userTypeList = data;
                       vm.TYPE = vm.userTypeList[0];
                   }).
                   error(function (data, status, headers, config) {
                       // called asynchronously if an error occurs
                       // or server returns response with an error status.
                       vm.userTypeList = data;
                   });
        }



    }
})();
