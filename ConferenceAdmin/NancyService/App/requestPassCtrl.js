(function () {
    'use strict';

    var controllerId = 'requestPassCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', '$window', '$location','$rootScope', requestPassCtrl]);

    function requestPassCtrl($scope, $http, restApi, $window, $location, $rootScope) {
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
                       if (data == "") {
                           vm.message = "This email is not registered."
                           vm.emailExist = false;
                       }
                       else if (data == "confirmed") {
                           vm.emailExist = true;
                           _requestPass();
                       }
                       else if (data == "notconfirmed") {
                           vm.emailExist = true;
                           vm.message = "You account need to be confirmed."
                          
                       }
                       

                   }).
                   error(function (data, status, headers, config) {
                       $rootScope.$emit('popUp', 'error');
                   });
        }


        function _requestPass() {
            vm.uploadingComp = true;
            if (vm.emailExist) {
                restApi.requestPass(vm.email).
                       success(function (data, status, headers, config) {
                         //  vm.toggleModal('changed');
                           vm.uploadingComp = false;
                           $rootScope.$emit('popUp', 'requestedPass');

                       }).
                       error(function (data, status, headers, config) {
                           vm.uploadingComp = false;
                           $rootScope.$emit('popUp', 'error');
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
                       $rootScope.$emit('popUp', 'error');
                   });
        }



    }
})();
