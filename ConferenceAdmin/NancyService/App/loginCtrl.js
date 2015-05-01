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
        vm.conferenceLogo;

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
        //  vm.logout = _logout;
        vm.signUp = _signUp;
        vm.loginIfEmail = _loginIfEmail;
        vm.getGeneralInfo = _getGeneralInfo;
        vm.uploadingComp;

        //alerts Directive
        function _goToLogin() { $location.path("/Login/Log"); }

        vm.toggleModal = function (action) {
            if (action == "changed") {

                vm.obj.title = "Reset Password",
                vm.obj.message1 = "Your temporary Password was send to your email!",
                vm.obj.message2 = "",
                vm.obj.label = "",
                vm.obj.okbutton = true,
                vm.obj.okbuttonText = "OK",
                vm.obj.cancelbutton = false,
                vm.obj.cancelbuttoText = "Cancel",
                vm.obj.okfunc = _goToLogin;
                vm.showConfirmModal = !vm.showConfirmModal;

            }

            if (action === "confirm") {

                vm.obj.title = "Account Confirmation",
                vm.obj.message1 = "Congratulation your account was confirmed!",
                vm.obj.message2 = "",
                vm.obj.label = "",
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
        _getGeneralInfo();
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

    
        $rootScope.$on('ConferenceLogo', function (event, data) {
            vm.conferenceLogo = data;
        });

        function _getGeneralInfo() {
            restApi.getGeneralInfo()
            .success(function (data, status, headers, config) {
                if (data != null && data != "") {
                    vm.conferenceAcronym = data.conferenceAcronym;
                    vm.conferenceName = data.conferenceName;

                    _getImage();
                }
            })
            .error(function (error) {

            });
        }

        function _getImage() {
            restApi.getWebsiteLogo()
            .success(function (data, status, headers, config) {
                if (data != null && data != "") {
                    vm.conferenceLogo = data.logo;
                }
            })

            .error(function (error) {

            });
        }

        function _validate() {
            restApi.accountConfirmation(vm.keyConfirmation).
                   success(function (data, status, headers, config) {
                       if (data == "") {
                           vm.message = "Please verify your confirmation Key.";
                           vm.keyConfirmation = "";


                       }
                       else if (data == "wasValidated") {
                           vm.message = "This account is already validated.";
                           vm.keyConfirmation = "";
                          

                       }
                       else {
                           $rootScope.$emit('popUp', 'requestedPass');
                       //    vm.toggleModal("confirm");
                        //   $location.path("/Login/Log");
                       }
                   }).
                   error(function (data, status, headers, config) {
                       $rootScope.$emit('popUp', 'error');
                   });
        }

        function _loginIfEmail() {
          vm.uploadingComp = true;
            restApi.checkEmail(vm.email).
                  success(function (data, status, headers, config) {
                      if (data == "") {
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
                      vm.uploadingComp = false;
                      $rootScope.$emit('popUp', 'error');
                    
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
                       
                       vm.isSponsor = false;
                       var list = JSON.parse($window.sessionStorage.getItem('claims'));
                       if (list != null) {
                           list.forEach(function (claim) {
                               if (claim.localeCompare('sponsor') == 0) {
                                   vm.isSponsor = true;
                               }

                           });
                       }
                       if (vm.isSponsor) {
                           $location.path('/Profile/sponsorgeneralinformation');
                       }
                       else {
                           $location.path('/Profile/GeneralInformation');
                       }

                  

                       })

                   .error(function (error) {
                       // called asynchronously if an error occurs
                       // or server returns response with an error status.
                       $window.sessionStorage.removeItem('token');
                       vm.message = "Wrong email or password please try again";

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
                       $rootScope.$emit('popUp', 'error');
                      
                   });
        }

        function _signUp() {
            vm.creatingUser = true;
            restApi.checkEmail(vm.user.email).
                   success(function (data, status, headers, config) {
                       if (data != "") {
                           vm.message = "This email is used.";
                           vm.creatingUser = false;
                       }
                       else {

                           _createUser();
                           $rootScope.$emit('popUp', 'requestedPass');

                       }

                   }).
                   error(function (data, status, headers, config) {
                   
                       vm.creatingUser = false;
                       $rootScope.$emit('popUp', 'error');
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
                       
                      // $rootScope.$emit('popUp', 'makeConfirmation');
                     


                   }).
                   error(function (data, status, headers, config) {
                       // called asynchronously if an error occurs
                       // or server returns response with an error status.
                      
                       vm.creatingUser = false;
                       $rootScope.$emit('popUp', 'error');
                   });
        }
    

    }
})();
