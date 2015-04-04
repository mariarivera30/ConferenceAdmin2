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

        vm.toggleModal = function (action) {

            if (action == "changed") {

                vm.obj.title = "Reset Password",
                vm.obj.message1 = "Your temporary Password was send to your email!",
                vm.obj.message2 = vm.email,
                vm.obj.label = "Email",
                vm.obj.okbutton = true,
                vm.obj.okbuttonText = "OK",
                vm.obj.cancelbutton = false,
                vm.obj.cancelbuttoText = "Cancel",
                vm.showConfirmModal = !vm.showConfirmModal;

            }
       
            else if (action == "error")
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
                       vm.toggleModal('error');
                   });
        }


        function _requestPass() {
            vm.uploadingComp = true;
            if (vm.emailExist) {
                restApi.requestPass(vm.email).
                       success(function (data, status, headers, config) {
                           vm.toggleModal('changed');
                           vm.uploadingComp = false;
                           $location.path("/Login/Log");
                       }).
                       error(function (data, status, headers, config) {
                           vm.uploadingComp = false;
                           vm.message = "An eeror occurr please refresh the page";
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
                       vm.toggleModal('error');
                   });
        }



    }
})();
