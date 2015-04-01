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
        vm.credentials = {
            password: "",
            newPass: "",
            newPassConfirm: "",
            email: ""
        };

        //Functions
        vm.changePassword= _changePassword;

        function activate() {

        }

        function _changePassword() {
            vm.loadingRemovingComp = true;
            restApi.changePassword(vm.credentials)
            .success(function (data, status, headers, config) {
                if (data != null && data != "") {
                    alert("Your Password was changed!");
                    $location.path("/Login/Log");
                    //_login();
                }

                else {
                    alert("Your Password cannot be changed please verify your email acccount!");
                    //hacer login automatico



                }

            })

            .error(function (data, status, headers, config) {
                alert("Server ERROR: Please try again.");
              

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
