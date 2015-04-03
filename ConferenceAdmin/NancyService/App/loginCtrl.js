(function () {
    'use strict';

    var controllerId = 'loginCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$rootScope', '$http', 'restApi', '$window', '$location', loginCtrl]);

    function loginCtrl($scope, $rootScope, $http, restApi, $window, $location) {
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




        // Functions
        vm.login = _login;
        vm.getUserTypes = _getUserTypes;
        vm.createUser = _createUser;
        vm.validate = _validate;
        vm.logout = _logout;
        vm.signUp = _signUp;
        vm.loginIfEmail = _loginIfEmail;

        //alerts Directive
 

        vm.toggleModal = function (action) {
            if (action ==="confirm") {
              
                vm.obj.title ="Account Confirmation",
                vm.obj.message1 = "Congratulation your account was confirmed!",
                vm.obj.message2 = "",
                vm.obj.label = "",
                vm.obj.okbutton = true,
                vm.obj.okbuttonText = "OK",
                vm.obj.cancelbutton = false,
                vm.obj.cancelbuttoText = "Cancel",
                vm.showConfirmModal = !vm.showConfirmModal;

            }
            if (action === "makeConfirmation") {

                vm.obj.title = "Next Step Account Confirmation",
                vm.obj.message1 = "Please check you email to confirm your account!",
                vm.obj.message2 = vm.user.email,
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

        activate();

        function activate() {
            _getUserTypes()
            if ($window.sessionStorage.length == 0) {
                vm.loged = false;
            }
            else {
                vm.loged = true;
                vm.messageLogOut = $window.sessionStorage.getItem('email').substring(1, $window.sessionStorage.getItem('email').length - 1);

            }
        }
        function _logout() {
            vm.isAdmin = false;
            $rootScope.$emit('Login', event, vm.isAdmin);
           
            
            $window.sessionStorage.clear();
            vm.loged = false;
            $location.path('/Home');
            vm.showAdminsitrator = false;
        }
        function _validate() {
            restApi.accountConfirmation(vm.keyConfirmation).
                   success(function (data, status, headers, config) {
                       if (data == "") {
                           vm.message = "Please verify your confirmation Key.";
                           vm.keyConfirmation = "";
                     
                          
                       }
                       else {
                           vm.toggleModal("confirm");
                           $location.path("/Login/Log");
                       }
                   }).
                   error(function (data, status, headers, config) {
                       vm.toggleModal("error");
                   });
        }
        function _loginIfEmail() {
            vm.uploadingComp = true;
            restApi.checkEmail(vm.email).
                  success(function (data, status, headers, config) {
                      if (data =="") {
                          vm.message = "This email is not registered.";
                          vm.uploadingComp = false;
                          return;
                      }
                      else if (data == "notconfirmed") {
                          vm.message = "Please verify your email to confirm your account before login.";
                          vm.uploadingComp = false;
                          return;
                      }
                      else {

                          _login();


                      }

                  }).
                  error(function (data, status, headers, config) {
                      vm.toggleModal("error");
                      vm.uploadingComp = false;
                  });

        }


        function _login() {
          
            restApi.login(vm)
                   .success(function (data, status, headers, config) {

                       // emit the new hideAlias value

                       $http.defaults.headers.common.Authorization = 'Token ' + data.token;
                       //localStorageService.set('token', data.token);
                       $window.sessionStorage.setItem('token', data.token);
                       $window.sessionStorage.setItem('claims', JSON.stringify(data.userClaims));
                       $window.sessionStorage.setItem('userID', JSON.stringify(data.userID));
                       $window.sessionStorage.setItem('email', JSON.stringify(data.email));

                     
                       vm.uploadingComp = false;
                       data.userClaims.forEach(function (claim) {
                           if (claim.localeCompare('adminFinance') == 0 || claim.localeCompare('adminCommittee') == 0) {
                               if (claim.localeCompare('adminCommittee') == 0)
                                   $location.path('/Administrator/ManageSubmissions');
                               else {
                                   $location.path('/Administrator/GeneralInformation');
                               }
                               vm.isAdmin = true;
                               $rootScope.$emit('Login', event, vm.isAdmin);
                           }

                           else if (claim.localeCompare('minor') == 0 || claim.localeCompare('companion') == 0 || claim.localeCompare('participant') == 0 || claim.localeCompare('evaluator') == 0) {
                               $location.path('/Profile/GeneralInformation');
                               vm.isAdmin = false;
                               $rootScope.$emit('Login', event, vm.isAdmin);
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
                       vm.uploadingComp = false;
                   });

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

        function _signUp() {
            vm.creatingUser = true;
            restApi.checkEmail(vm.user.email).
                   success(function (data, status, headers, config) {
                       if (data != "") {
                           vm.message = "This email is used.";
                       }
                       else {

                           _createUser();


                       }

                   }).
                   error(function (data, status, headers, config) {
                       vm.toggleModal();
                       vm.toggleModal("error");
                       vm.creatingUser = false
                   });
        }

        function _createUser() {
            vm.user.userTypeID = vm.TYPE.userTypeID;
            if (vm.evaluator)
                vm.user.evaluatorStatus = "Pending";
            restApi.createUser(vm.user).
                   success(function (data, status, headers, config) {
                       // this callback will be called asynchronously
                       // when the response is available
                       vm.creatingUser = false;
                       vm.toggleModal("makeConfirmation");
                       $location.path('/Login/Log');


                   }).
                   error(function (data, status, headers, config) {
                       // called asynchronously if an error occurs
                       // or server returns response with an error status.
                       vm.toggleModal("error");

                   });
        }

    }
})();
