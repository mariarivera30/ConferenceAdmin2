(function () {
    'use strict';

    var controllerId = 'signUPCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$rootScope', '$http', 'restApi', '$window', '$location', signUPCtrl]);

    function signUPCtrl($scope, $rootScope, $http, restApi, $window, $location) {
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

       

        // Functions
        vm.login = _login;
        vm.getUserTypes = _getUserTypes;
        vm.createUser = _createUser;
       
        //  vm.logout = _logout;
        vm.signUp = _signUp;
        vm.loginIfEmail = _loginIfEmail;
     


       
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
            vm.uploadingComp = true;
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
                      
                       $location.path('/Profile/GeneralInformation');

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
                       $rootScope.$emit('popUp', 'makeConfirmation');


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
