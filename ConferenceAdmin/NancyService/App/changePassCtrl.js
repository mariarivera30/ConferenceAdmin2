(function () {
    'use strict';

    var controllerId = 'changePassCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi','$window','$location', changePassCtrl]);

    function changePassCtrl($scope, $http, restApi, $window, $location) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'changePassCtrl';
        vm.loged;
        vm.credentials = {
            password: "",
            newPass: "",
            newPassConfirm: "",
            email: ""
        };

        vm.goTo = _goTo;
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
        function _goTo()
        {
            if (vm.actionPopUp == "changed" && (vm.loged ==false) )
                $location.path('/Login/Log');
            else if (vm.actionPopUp == "changedLoged" && (vm.loged == true))
                $location.path('/profile/generalinformation');
            else {
                vm.credentials = {};
            }
        }

        vm.toggleModal = function (action) {
          
            if (action == "changed") {
                vm.actionPopUp = "changed";
                vm.obj.title = "Password Changed";
                vm.obj.message1 = "Your Password was changed. Please Login.",
                vm.obj.message2 = vm.credentials.email,
                vm.obj.label = "Email",
                vm.obj.okbutton = true,
                vm.obj.okbuttonText = "OK",
                vm.obj.cancelbutton = false,
                vm.obj.cancelbuttoText = "Cancel",
                vm.showConfirmModal = !vm.showConfirmModal;
                
            }
            if (action == "changedLoged") {
                vm.actionPopUp = "changedLoged";
                vm.obj.title = "Password Changed";
                vm.obj.message1 = "Your Password was changed!",
                vm.obj.message2 = vm.credentials.email,
                vm.obj.label = "Email",
                vm.obj.okbutton = true,
                vm.obj.okbuttonText = "OK",
                vm.obj.cancelbutton = false,
                vm.obj.cancelbuttoText = "Cancel",
                vm.showConfirmModal = !vm.showConfirmModal;

            }
            if (action == "notchanged") {
                
                vm.obj.title = "Your Password cannot be changed!",
                vm.obj.message1 = "Verify your credentials and try again",
                vm.obj.message2 = vm.credentials.email,
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
        //Functions
        vm.changePassword= _changePassword;
        vm.setView = _setView;

        activate();
        function activate() {
            _setView()
        }
        function _setView() {
            if ($window.sessionStorage.length == 0) {
                vm.loged = false;
                vm.placeholder = "Temporary Password";
            }
            else {
                vm.loged = true;
                vm.credentials.email = JSON.parse($window.sessionStorage.getItem('email'));
                vm.placeholder = "Previous Password";

            }

            
        }
        function _changePassword() {
            vm.loadingUploading = true;
            restApi.changePassword(vm.credentials)
            .success(function (data, status, headers, config) {
                if (data != null && data != "") {
                    vm.loadingUploading = false;

                   
                    if(vm.loged)
                        vm.toggleModal('changedLoged');
                    else {
                        vm.toggleModal('changed');
                    }
                }

                else {
                    vm.loadingUploading = false;
                    vm.toggleModal('notchanged');
                  }

            })

            .error(function (data, status, headers, config) {
                vm.loadingUploading = false;
                vm.toggleModal('error');
              

            });
        }

        function _login() {
            restApi.login(vm.credentials)
                   .success(function (data, status, headers, config) {
                       $http.defaults.headers.common.Authorization = 'Token ' + data.token;
                       //localStorageService.set('token', data.token);
                       $window.sessionStorage.setItem('token', data.token);
                       $window.sessionStorage.setItem('claims', JSON.stringify(data.userClaims));
                       $window.sessionStorage.setItem('userID', JSON.stringify(data.userID));
                       $window.sessionStorage.setItem('email', JSON.stringify(data.email));
                    
                       data.userClaims.forEach(function (claim) {
                           if (claim.localeCompare('adminFinance') == 0 || claim.localeCompare('adminCommittee') == 0) {
                               if (claim.localeCompare('adminCommittee') == 0)
                                   $location.path('/Administrator/ManageSubmissions');
                               else {
                                   $location.path('/Administrator/GeneralInformation');
                               }

                           }

                           else if (claim.localeCompare('minor') == 0 || claim.localeCompare('companion') == 0 || claim.localeCompare('participant') == 0 || claim.localeCompare('evaluator') == 0) {
                               $location.path('/Profile/GeneralInformation');
                               return;
                           }


                       });


                   })

                   .error(function (error) {
                       // called asynchronously if an error occurs
                       // or server returns response with an error status.
                       $window.sessionStorage.removeItem('token');
                       vm.message = "Wrong email or password please try again";
                       vm.email = "";
                       vm.password = "";
                   });

        }


    }
})();
